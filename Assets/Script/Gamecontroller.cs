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
	
	private List<GameObject> m_path;

	private int m_pointDice;
	
	public Player[] m_player;
	
	private string m_state;
	
	public Dice m_dice;
	
	public Roll m_buttonRoll;
	private GameStateID m_stateID;
	
	public Createfloor m_createFloor;
	public CreateEvent m_createEvent;
	
	private DragCamera m_dragCamera;
	
	public int m_numPlayer;
	
	private int m_currID;
	
	private int m_maxNode;
	
	private MainCameraMove m_mainCameraMove;
	
	// Use this for initialization
	void Start () {
		
		Time.timeScale = 0.9f;
		
		// Set player number
		m_numPlayer = PlayerPrefs.GetInt ("numPlayer");
		
		// Set player number
		m_stateID = GameStateID.GetPath;
		
		// Set player id current
		m_currID = 0;
		
		// Initial value for event Stop and enable player
		Debug.Log (m_numPlayer);

		for (int i = 0; i < m_numPlayer; i++) {
			m_player[i].gameObject.SetActive(true);
		}
		
		m_mainCameraMove = m_mainCamera.GetComponent<MainCameraMove> ();
		m_dragCamera = m_mainCamera.GetComponent<DragCamera> ();
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
	private IEnumerator MoveCamera(List<GameObject> path){
		yield return StartCoroutine (m_mainCameraMove.FindStartPos());
		
		yield return StartCoroutine (m_mainCameraMove.MoveCameraFollowPath(path));
	}
	
	// Create path and Get path
	private IEnumerator GetPathFromCreate(){
		
		m_stateID = GameStateID.Getting;
		Debug.Log ("GetPath");
		
		//Delay for wait create path
		
		yield return new WaitForSeconds (1f);
		
		// Get path
		m_path = m_createFloor.GetPath ();
		
		m_maxNode = m_path.Count;
		
		// Create array for get all event
		m_createEvent.CreateAllEvent (m_path);
		
		// Set player Path and start position
		for (int i = 0; i < m_numPlayer; i++) {
			m_player[i].SetPath(m_path);
			m_player[i].SetStartPos();
			m_player[i].MoveToCheckPoint();
		}
		
		// Delay for wait set player
		yield return new WaitForSeconds (0.1f);
		
		Debug.Log ("GET DONE");
		
		// Move camera at start
		yield return StartCoroutine (MoveCamera (m_path));
		
		// Set limit drag camera
		m_dragCamera.SetLastPos (m_path[m_path.Count - 1].transform.position);
		float rX = m_createFloor.GetRightX ();
		float lX = m_createFloor.GetLeftX ();
		m_dragCamera.SetX (rX,lX);
		
		// Set player can drag
		m_dragCamera.SetIsDrag (true);
		
		
		//	StartCoroutine( m_mainCameraMove.ShowDiceButton ());
		
		// Change state to wait player roll
		m_stateID = GameStateID.WaitRoll;
	}
	
	// Event State
	private IEnumerator Event(){

		// Can't drag
		m_dragCamera.SetIsDrag (false);

		int lastCurrPos = 0;
		bool isEvent = true;
		
		while (isEvent) {
			int currPos ;

			currPos = m_player [m_currID].GetCurrentPos ();
			
			Debug.Log ("CURR POS : " + currPos);
			
			EventClass eventObj = m_path [currPos].GetComponent<FloorProperties> ().GetEvent ();
			//eventObj = m_path [currPos].GetComponent<FloorProperties> ().GetEvent ();
			if(eventObj == null || currPos == lastCurrPos){
				break;
			}
			else {
				lastCurrPos = currPos;

				// Start event 
				yield return eventObj.StartCoroutine(eventObj.DoEvent(m_player[m_currID]));
			}
		}	

		// Can drag
		m_dragCamera.SetIsDrag (true);

		yield break;
	}
	
	// Roll State
	private IEnumerator Roll(){
		Debug.Log ("ROLL");

		// Can't drag
		m_dragCamera.SetIsDrag (false);

		int normalMode = 0;
		
		m_stateID = GameStateID.Rolling;
		
		// Set button can click
		m_buttonRoll.SetClick (false);
		
		// Delay wait for set button
		yield return new WaitForSeconds (0.5f);

		// Change player windows
		m_player [m_currID].ChangePlayerWindow ();

		// Move player to center of floor
		m_player[m_currID].MoveToCenter ();

		// Move camera to player
		yield return StartCoroutine( m_mainCameraMove.SetPosition (m_player[m_currID].transform.position));
		
		// Delay wait camera move
		yield return new WaitForSeconds (0.5f);

		// Check time bomb
		bool isBomb = m_player[m_currID].IsBomb();
		if (isBomb) {
			Debug.Log("DECREASE TIME : " + m_player [m_currID].GetTimeBomb ());
			m_player [m_currID].DecreaseTimeBomb ();
		}

		// Wait for bomb event
		yield return new WaitForSeconds (0.01f);

		// Check event stop turn that player have
		if (m_player[m_currID].GetEventStop() == normalMode) {

			if(m_player[m_currID].IsUFO()){
				m_player[m_currID].SetIsUFO(false);
				yield return new WaitForSeconds(5f);
			}
			// Show button and dice
			m_mainCameraMove.ShowDiceButton ();
			
			Debug.Log ("WAIT Player : " + (m_currID + 1).ToString() + " ROLL");
			
			// Set can drag
			m_dragCamera.SetIsDrag (true);

			// Set dice defualt
			m_dice.m_isSetPoint = false;

			// Get value button
			bool isClick = m_buttonRoll.GetClick();
			
			//			Debug.Log (hasClick);
			
			// Random time to roll dice
			float m_timeRandom = Random.Range(1F, 3F);
			
			// Wait unitl player click button
			while (!isClick) {
				isClick = m_buttonRoll.GetClick ();
				yield return null;
			}
			
			// Move camera to player and wait until this done
			yield return StartCoroutine( m_mainCameraMove.SetPosition (m_player[m_currID].transform.position));
			
			// Start roll dice
			m_dice.SetTimeRandom(m_timeRandom);
			m_dice.StartRoll ();
			
			// Set can't drag
			m_dragCamera.SetIsDrag (false);
			
			// Wait for roll dice
			while(!m_dice.isStopRoll())
				yield return null;
			
			//	m_dragCamera.SetDrag (true);
			
			//	Debug.Log ("BEFORE : " + m_pointDice);
			
			// Get poice dice
			m_pointDice = m_dice.GetPointDice () + 1;
			
			// Change State
			m_stateID = GameStateID.Move;
			//m_state = "Move";
			
		}
		// Change player turn when have stop
		else {
			Debug.Log("Player : " + (m_currID + 1).ToString() + " STOP!! " ) ;
			m_player[m_currID].DecreaseEventStop();
			ChangePlayerTurn ();
			
			m_stateID = GameStateID.WaitRoll;
			//m_state = "WaitRoll";
		}
		
		yield break;
	}
	
	// Move State
	private IEnumerator PlayerMove(){
		
		m_stateID = GameStateID.Moving;
		//	m_dragCamera.SetDrag (false);
		//m_state = "Moving";
		Debug.Log (m_stateID);

		m_dragCamera.SetIsDrag (false);

		// Wait player move
		yield return StartCoroutine(m_player[m_currID].GoNextPos( m_pointDice, m_maxNode-1));
		
		// Wait check event
		yield return StartCoroutine(Event ());
		
		Debug.Log ("END TURN WAIT OTHER PLAYER");
		
		// Delay for wait check
		yield return new WaitForSeconds(0.5f);

		// Move to check point
		if(!m_player[m_currID].IsUFO())
			m_player[m_currID].MoveToCheckPoint ();
		yield return new WaitForSeconds (0.5f);

		// Change player
		ChangePlayerTurn ();

		m_dragCamera.SetIsDrag (true);

		// Change State
		m_stateID = GameStateID.WaitRoll;
		
		//m_dragCamera.SetDrag (true);
		yield break;
	}
	
	// Change player turn
	private void ChangePlayerTurn(){
		m_currID ++;
		if (m_currID == m_numPlayer)
			m_currID = 0;
	}

	public Player GetCurrentPlayer(){
		return m_player[m_currID];
	}
	
}
