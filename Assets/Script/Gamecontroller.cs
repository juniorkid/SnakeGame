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

	private int[] m_eventStop;	// Keep turn that each player will stop
	
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

		// Set player number
		m_numPlayer = PlayerPrefs.GetInt ("numPlayer");

		// Set player number
		m_stateID = GameStateID.GetPath;

		// Create array for keep player event stop
		m_eventStop = new int[m_numPlayer];

		// Set player id current
		m_currID = 0;

		// Initial value for event Stop and enable player
		for (int i = 0; i < m_numPlayer; i++) {
			m_player[i].SetActive(true);
			m_eventStop [i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// State in Game
		switch (m_stateID) {
		case GameStateID.GetPath :
			StartCoroutine (GetPathFromCreate ()); break;
		case GameStateID.WaitRoll: 
			StartCoroutine (Roll ()); break;
		case GameStateID.Move: 
			StartCoroutine (PlayerMove ()); break;
		}
	}

	// Move camera along path at start game
	private IEnumerator MoveCamera(List<Vector3> path){
		yield return StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().FindStartPos());

		yield return StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().MoveCameraFollowPath(path));
	}

	// Create path and Get path
	private IEnumerator GetPathFromCreate(){

		m_stateID = GameStateID.Getting;
		Debug.Log ("GetPath");

		//Delay for wait create path

		yield return new WaitForSeconds (1f);

		// Get path
		m_path = m_createFloor.GetComponent<Createfloor> ().GetPath ();

		m_maxNode = m_path.Count;

		// Create array for get all event
		m_event = new int[m_path.Count];
		m_eventCard = new bool[m_path.Count];	
		m_createFloor.GetComponent<CreateEvent> ().CreateAllEvent (m_path, m_event, m_eventCard);

		// Set event card
		m_deckCard.GetComponent<CardControl> ().SetAllEvent (m_event, m_eventCard);

		// Set player to start position
		Vector3 startPos = m_path [0];
		startPos.z = -2;
		for (int i = 0; i < m_numPlayer; i++) {
			m_player [i].transform.position = startPos;
		}

		// Delay for wait set player
		yield return new WaitForSeconds (0.1f);

		Debug.Log ("GET DONE");

		// Move camera at start
		yield return StartCoroutine (MoveCamera (m_path));

		// Set limit drag camera
		m_mainCamera.GetComponent<DragCamera> ().SetLastPos (m_path[m_path.Count - 1]);
		float rX = m_createFloor.GetComponent<Createfloor> ().GetRightX ();
		float lX = m_createFloor.GetComponent<Createfloor> ().GetLeftX ();
		m_mainCamera.GetComponent<DragCamera> ().SetX (rX,lX);

		// Set player can drag
		m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);


	//	StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().ShowDiceButton ());

		// Change state to wait player roll
		m_stateID = GameStateID.WaitRoll;
	}

	// Event State
	private IEnumerator Event(){
		int normalMode = 0;
		int currPos;
		int goPos;

		// initial value event check
		bool checkEvent = true;

		Debug.Log ("EVENT CHECK START");
		m_stateID = GameStateID.DoingEvent;

		// Do event until that position don't have event
		while(checkEvent){
			// Set current position and next position
			currPos = m_player [m_currID].GetComponent<PlayerMovement> ().GetCurrentPos ();
			goPos = m_event [currPos];
				
			// Check for do UP DOWN And Trap restart EVENT
			if(goPos != currPos && goPos > 0){

				// Do restart trap
				if(goPos == m_maxNode){
					GameObject trapRestart;

					// Change Restart position to player position and show trap
					Vector3 pos = m_path[currPos];
					pos.z = -3;

					trapRestart = GameObject.FindWithTag("RestartItem");
					trapRestart.transform.position = pos;

					// Waite for find and change restart trap position
					yield return new WaitForSeconds(1f);

					// Destroy trap at current position
					m_event[currPos] = 0;

					pos.z = 3;

					// Hide trap
					trapRestart.transform.position = pos;

					goPos = 0;
				}

				// Move player to position
				yield return StartCoroutine(m_player[m_currID].GetComponent<PlayerMovement>().GoAnyPos(goPos, m_path));
			}
			// Do stop event 
			else if(m_event [currPos] < normalMode){
				Debug.Log ("STOP TURN");
				Debug.Log ("CURRENT POSITION WHEN CHECK EVENT : " + currPos.ToString());
				// Set turn that player will stop
				m_eventStop[m_currID] = Mathf.Abs( m_event[currPos]);
				Debug.Log("EVENT STOP : " + m_eventStop[m_currID].ToString());
				checkEvent = false;
			}
			// Do card event
			else if(m_eventCard[currPos]){
				m_deckCard.GetComponent<CardControl> ().SetfinishFlip (false);
				m_deckCard.GetComponent<CardControl> ().SetfinishTrap (false);
				// Call card control
				yield return StartCoroutine(m_deckCard.GetComponent<CardControl>().ControlCard());
				m_mainCamera.GetComponent<DragCamera>().SetDrag(true);
				// Call card event
				yield return StartCoroutine(m_deckCard.GetComponent<CardControl>().CardEvent(m_maxNode, m_path));

				Debug.Log ("TEST : " + m_event[12]);

				checkEvent = false;
			}
			else
				checkEvent = false;
		}

		Debug.Log ("EVENT CHECK STOP");
		yield break;
	}

	// Roll State
	private IEnumerator Roll(){
		int normalMode = 0;
		
		m_stateID = GameStateID.Rolling;

		// Set button can click
		m_buttonRoll.GetComponent<Roll> ().SetClickDefualt ();

		// Delay wait for set button
		yield return new WaitForSeconds (0.5f);

		// Move camera to player
		yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (m_player[m_currID].transform.position));

		// Check event stop turn that player have
		if (m_eventStop [m_currID] == normalMode) {

			// Show button and dice
			m_mainCamera.GetComponent<MainCameraMove> ().ShowDiceButton ();
			
			Debug.Log ("WAIT Player : " + (m_currID + 1).ToString() + " ROLL");

			// Set can drag
			m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);

			// Get value button
			bool hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick();

//			Debug.Log (hasClick);

			// Random time to roll dice
			float m_timeRandom = Random.Range(3F, 8F);

			// Wait unitl player click button
			while (!hasClick) {
				hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick ();
				yield return null;
			}

			// Move camera to player and wait until this done
			yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (m_player[m_currID].transform.position));

			// Start roll dice
			m_dice.GetComponent<Dice> ().StartRoll ();

			// Set can't drag
			m_mainCamera.GetComponent<DragCamera> ().SetDrag (false);

			// Wait for roll dice
			yield return new WaitForSeconds (m_timeRandom);
			m_dice.GetComponent<Dice> ().StopRoll ();
			
		//	m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);

		//	Debug.Log ("BEFORE : " + m_pointDice);

			// Get poice dice
			m_pointDice = m_dice.GetComponent<Dice> ().GetPointDice () + 1;

			//	m_currentPos = 31;
			//	m_pointDice = 1;

		//	Debug.Log ("After : " + m_pointDice);
		
			// Change State
			m_stateID = GameStateID.Move;
			//m_state = "Move";

		}
		// Change player turn when have stop
		else {
			Debug.Log("Player : " + (m_currID + 1).ToString() + " STOP!! " ) ;
			m_eventStop [m_currID] --;
			ChangePlayerTurn ();

			m_stateID = GameStateID.WaitRoll;
			//m_state = "WaitRoll";
		}

		yield break;
	}

	// Move State
	private IEnumerator PlayerMove(){

		m_stateID = GameStateID.Moving;
	//	m_mainCamera.GetComponent<DragCamera> ().SetDrag (false);
		//m_state = "Moving";
		Debug.Log (m_stateID);

		// Wait player move
		yield return StartCoroutine(m_player[m_currID].GetComponent<PlayerMovement>().GoNextPos( m_pointDice, m_path, m_maxNode));

		// Wait check event
		yield return StartCoroutine(Event ());

		Debug.Log ("END TURN WAIT OTHER PLAYER");

		// Delay for wait check
		yield return new WaitForSeconds(0.1f);

		// Change player
		ChangePlayerTurn ();

		// Change State
		m_stateID = GameStateID.WaitRoll;

		//m_mainCamera.GetComponent<DragCamera> ().SetDrag (true);
		yield break;
	}

	// Change player turn
	private void ChangePlayerTurn(){
		m_currID ++;
		if (m_currID == m_numPlayer)
			m_currID = 0;
	}

}
