using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameStateID
{
	MoveCamera,
	WaitRoll,
	Rolling,
	Move,
	Moving,
	DoingEvent,
	GetPath,
	Getting,
}

public enum PlayerID
{
	Player1 = 0,
	Player2,
}
public class Gamecontroller : MonoBehaviour {
	public Camera m_mainCamera;

	private List<Vector3> m_path;

	private int[] m_event;

	private int[] m_eventStop;

	private string[] m_upDownPos;
	
	private int m_pointDice;

	public GameObject[] m_player;

	private string m_state;

	public GameObject m_dice;

	public GameObject m_buttonRoll;
	private GameStateID m_stateID;
	private PlayerID m_playerID;

	public GameObject m_createFloor;

	public int m_numPlayer;

	private int m_currID;
	// Use this for initialization
	void Start () {

		m_numPlayer = 2;

		m_stateID = GameStateID.GetPath;

		m_eventStop = new int[m_numPlayer];

		m_upDownPos = new string[40];

		m_currID = (int)m_playerID;

		for (int i = 0; i < m_numPlayer; i++) {
			m_eventStop [i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_stateID) {
		case GameStateID.GetPath :
			StartCoroutine (GetPathFromCreate ()); break;
		case GameStateID.WaitRoll: 
			StartCoroutine (Roll ()); break;
		case GameStateID.Move: 
			StartCoroutine (PlayerMove ()); break;
		}
	}

	private IEnumerator MoveCamera(List<Vector3> path){
		yield return StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().FindStartPos());

		yield return StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().MoveCameraFollowPath(path));
	}

	private IEnumerator GetPathFromCreate(){
		m_stateID = GameStateID.Getting;
		Debug.Log ("GetPath");

		yield return new WaitForSeconds (1f);

		m_path = m_createFloor.GetComponent<Createfloor> ().GetPath ();

		m_event = new int[m_path.Count];
		m_upDownPos = new string[m_path.Count];

		m_createFloor.GetComponent<CreateEvent> ().CreateAllEvent (m_path, m_event);

		m_event = m_createFloor.GetComponent<CreateEvent> ().GetEvent ();

		Vector3 startPos = m_path [0];

		startPos.z = -2;

		for (int i = 0; i < m_numPlayer; i++) {
			m_player [i].transform.position = startPos;
		}

		yield return new WaitForSeconds (1f);

	//	foreach(int e in m_event){
	//		Debug.Log(e);
	//	}
		Debug.Log ("GET DONE");

		yield return StartCoroutine (MoveCamera (m_path));

		m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);

		m_mainCamera.GetComponent<DragCamera> ().SetLastPos (m_path[m_path.Count - 1]);

		m_stateID = GameStateID.WaitRoll;
	}

	private IEnumerator Event(){
		int normalMode = 0;
		int currPos;
		int goPos;


		currPos = m_player [m_currID].GetComponent<PlayerMovement> ().GetCurrentPos ();

		goPos = m_event [currPos];

		Debug.Log ("EVENT CHECK START");
		m_stateID = GameStateID.DoingEvent;
	//	m_state = "DoingEvent";
		if(m_upDownPos[currPos] != null){
			yield return StartCoroutine(m_player[m_currID].GetComponent<PlayerMovement>().DoUpDownEvent(goPos, m_path[goPos]));
		}
		else if(m_event [currPos] < normalMode){
			Debug.Log ("STOP TURN");
			Debug.Log ("CURRENT POSITION WHEN CHECK EVENT : " + currPos.ToString());
			m_eventStop[m_currID] = Mathf.Abs( m_event[currPos]);
			Debug.Log("EVENT STOP : " + m_eventStop[m_currID].ToString());
		}

		Debug.Log ("EVENT CHECK STOP");
		yield break;
	}

	private IEnumerator Roll(){
		int normalMode = 0;
		
		m_stateID = GameStateID.Rolling;

		yield return new WaitForSeconds (0.5f);

		yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosiotion (m_player[m_currID].transform.position));

		m_mainCamera.GetComponent<MainCameraMove> ().ShowDiceButton ();

		Debug.Log ("WAIT Player : " + (m_currID + 1).ToString() + " ROLL");

		m_buttonRoll.GetComponent<Roll> ().SetClickDefualt ();



		bool hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick();
		float m_timeRandom = Random.Range(3F, 8F);

		if (m_eventStop [m_currID] == normalMode) {
			while (!hasClick) {
				hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick ();
				yield return null;
			}

			m_dice.GetComponent<Dice> ().StartRoll ();



			yield return new WaitForSeconds (m_timeRandom);
			m_dice.GetComponent<Dice> ().StopRoll ();

		//	Debug.Log ("BEFORE : " + m_pointDice);

			m_pointDice = m_dice.GetComponent<Dice> ().GetPointDice () + 1;

			//	m_currentPos = 31;
			//	m_pointDice = 1;

		//	Debug.Log ("After : " + m_pointDice);
		
			m_stateID = GameStateID.Move;
			//m_state = "Move";

		} else {
			Debug.Log("Player : " + (m_currID + 1).ToString() + " STOP!! " ) ;
			m_eventStop [m_currID] --;
			ChangePlayerTurn ();

			m_stateID = GameStateID.WaitRoll;
			//m_state = "WaitRoll";
		}
		yield break;
	}

	private IEnumerator PlayerMove(){

		m_stateID = GameStateID.Moving;
		m_mainCamera.GetComponent<DragCamera> ().SetDrag (false);
		//m_state = "Moving";
		Debug.Log (m_stateID);

		yield return StartCoroutine(m_player[m_currID].GetComponent<PlayerMovement>().GoNextPos( m_pointDice, m_path));
		
		yield return StartCoroutine(Event ());

		Debug.Log ("END TURN WAIT OTHER PLAYER");

		yield return new WaitForSeconds(0.5f);

		ChangePlayerTurn ();

		m_stateID = GameStateID.WaitRoll;
		//m_state = "WaitRoll";
		m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);
		yield break;
	}

	private bool Equal(float x , float y){
		float error = 0.05f;

		if (x > y - error && x < y + error) {
			return true;
		} else
			return false;
	}

	private void ChangePlayerTurn(){
		switch(m_playerID){
			case PlayerID.Player1: m_playerID = PlayerID.Player2; Debug.Log("CHANGE To 2"); break;
			case PlayerID.Player2: m_playerID = PlayerID.Player1; Debug.Log("CHANGE To 1");break;
		}
		m_currID = (int)m_playerID;
	}
	
}
