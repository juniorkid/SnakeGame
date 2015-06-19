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

public class Gamecontroller : MonoBehaviour {
	public Camera m_mainCamera;

	private List<Vector3> m_path;

	private int[] m_event; // Tell what player will do

	private bool[] m_eventCard;

	private int[] m_eventStop;

	private string[] m_upDownPos;
	
	private int m_pointDice;

	public GameObject[] m_player;

	private string m_state;

	public GameObject m_dice;

	public GameObject m_buttonRoll;
	private GameStateID m_stateID;

	public GameObject m_createFloor;

	public int m_numPlayer;

	private int m_currID;

	private int m_maxNode;

	public GameObject m_deckCard;

	// Use this for initialization
	void Start () {

		Time.timeScale = 0.9f;

	//	m_numPlayer = 2;
		m_numPlayer = PlayerPrefs.GetInt ("numPlayer");

		m_stateID = GameStateID.GetPath;

		m_eventStop = new int[m_numPlayer];

		m_currID = 0;



		for (int i = 0; i < m_numPlayer; i++) {
			m_player[i].SetActive(true);
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

		m_maxNode = m_path.Count;

		m_event = new int[m_path.Count];
		m_eventCard = new bool[m_path.Count];
		m_upDownPos = new string[m_path.Count];

		m_createFloor.GetComponent<CreateEvent> ().CreateAllEvent (m_path, m_event, m_eventCard);

//		m_event = m_createFloor.GetComponent<CreateEvent> ().GetEvent ();

		m_eventCard = m_createFloor.GetComponent<CreateEvent> ().GetPosEventCard();

		Vector3 startPos = m_path [0];

		startPos.z = -2;

		for (int i = 0; i < m_numPlayer; i++) {
			m_player [i].transform.position = startPos;
		}

		yield return new WaitForSeconds (0.1f);

	//	foreach(int e in m_event){
	//		Debug.Log(e);
	//	}
		Debug.Log ("GET DONE");

		yield return StartCoroutine (MoveCamera (m_path));

		m_mainCamera.GetComponent<DragCamera> ().SetLastPos (m_path[m_path.Count - 1]);

		float rX = m_createFloor.GetComponent<Createfloor> ().GetRightX ();
		float lX = m_createFloor.GetComponent<Createfloor> ().GetLeftX ();

		m_mainCamera.GetComponent<DragCamera> ().SetX (rX,lX);

		m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);


	//	StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().ShowDiceButton ());

		m_stateID = GameStateID.WaitRoll;
	}

	private IEnumerator Event(){
		int normalMode = 0;
		int currPos;
		int goPos;

		bool checkEvent = true;

		Debug.Log ("EVENT CHECK START");
		m_stateID = GameStateID.DoingEvent;
	//	m_state = "DoingEvent";
		while(checkEvent){
			currPos = m_player [m_currID].GetComponent<PlayerMovement> ().GetCurrentPos ();
			goPos = m_event [currPos];
				
			if(m_event[currPos] != currPos && goPos > 0){

				//goPos = m_maxNode;

				if(goPos == m_maxNode){
					Vector3 pos = m_path[currPos];
					pos.z = -3;

					GameObject.FindWithTag("RestartItem").transform.position = pos;

					yield return new WaitForSeconds(1f);

					m_event[currPos] = 0;

					pos.z = 3;

					GameObject.FindWithTag("RestartItem").transform.position = pos;

					goPos = 0;
				}

				yield return StartCoroutine(m_player[m_currID].GetComponent<PlayerMovement>().DoUpDownEvent(goPos, m_path));
			}
			else if(m_event [currPos] < normalMode){
				Debug.Log ("STOP TURN");
				Debug.Log ("CURRENT POSITION WHEN CHECK EVENT : " + currPos.ToString());
				m_eventStop[m_currID] = Mathf.Abs( m_event[currPos]);
				Debug.Log("EVENT STOP : " + m_eventStop[m_currID].ToString());
				checkEvent = false;
			}
			else if(m_eventCard[currPos]){
				m_deckCard.GetComponent<CardControl> ().SetfinishFlip (false);
				m_deckCard.GetComponent<CardControl> ().SetfinishTrap (false);
				yield return StartCoroutine(m_deckCard.GetComponent<CardControl>().ControlCard());
				m_mainCamera.GetComponent<DragCamera>().SetDrag(true);
				yield return StartCoroutine(m_deckCard.GetComponent<CardControl>().CardEvent(m_event, m_eventCard, m_maxNode, m_path));

				Debug.Log ("TEST : " + m_event[12]);

				checkEvent = false;
}
			else
				checkEvent = false;
		}
		Debug.Log ("EVENT CHECK STOP");
		yield break;
	}
	
		private IEnumerator Roll(){
		int normalMode = 0;
		
		m_stateID = GameStateID.Rolling;

		m_buttonRoll.GetComponent<Roll> ().SetClickDefualt ();

		yield return new WaitForSeconds (0.5f);

		yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (m_player[m_currID].transform.position));

		if (m_eventStop [m_currID] == normalMode) {

			m_mainCamera.GetComponent<MainCameraMove> ().ShowDiceButton ();
			
			Debug.Log ("WAIT Player : " + (m_currID + 1).ToString() + " ROLL");

			m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);

			bool hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick();

//			Debug.Log (hasClick);

			float m_timeRandom = Random.Range(3F, 8F);

			while (!hasClick) {
				hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick ();
				yield return null;
			}

			yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (m_player[m_currID].transform.position));

			m_dice.GetComponent<Dice> ().StartRoll ();
			
			m_mainCamera.GetComponent<DragCamera> ().SetDrag (false);

			yield return new WaitForSeconds (m_timeRandom);
			m_dice.GetComponent<Dice> ().StopRoll ();
			
		//	m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);

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
	//	m_mainCamera.GetComponent<DragCamera> ().SetDrag (false);
		//m_state = "Moving";
		Debug.Log (m_stateID);

		yield return StartCoroutine(m_player[m_currID].GetComponent<PlayerMovement>().GoNextPos( m_pointDice, m_path, m_maxNode));
		
		yield return StartCoroutine(Event ());

		Debug.Log ("END TURN WAIT OTHER PLAYER");

		yield return new WaitForSeconds(0.1f);

		ChangePlayerTurn ();

		m_stateID = GameStateID.WaitRoll;
		//m_state = "WaitRoll";

		//m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);
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
		m_currID ++;
		if (m_currID == m_numPlayer)
			m_currID = 0;
	}

}
