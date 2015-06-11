using UnityEngine;
using System.Collections;

public enum GameStateID
{
	WaitRoll,
	Rolling,
	Move,
	Moving,
	DoingEvent,
}

public enum PlayerID
{
	Player1 = 0,
	Player2,
}
public class Gamecontroller : MonoBehaviour {

	private Vector3[] m_path1;
	private Vector3[] m_path2;

//	private float[] m_posPlayerX;
//	private float[] m_posPlayerY;
//	private bool[] m_posPlayer;
	private int[] m_event;

	private int[] m_eventStop;

	private string[] m_upDownPos;

	private int[] m_currentPos;
	private int m_pointDice;

	public GameObject[] m_player;

	private string m_state;

	public GameObject m_dice;

	public GameObject m_buttonRoll;
	private GameStateID m_stateID;
	private PlayerID m_playerID;

	private int m_currID;
	// Use this for initialization
	void Start () {


		m_stateID = GameStateID.WaitRoll;
		//m_state = "WaitRoll";

		m_currentPos = new int[2];
		m_eventStop = new int[2];
		m_event = new int[40];
		m_upDownPos = new string[40];

		m_currID = (int)m_playerID;

		m_currentPos[0] = 0;
		m_currentPos[1] = 0;

	/*	m_eventStop[0] = 0;
		m_eventStop[1] = 0;
		
		m_upDownPos [6] = "UP1";
		m_upDownPos [11] = "UP2";
		m_upDownPos [23] = "UP3";
		m_upDownPos [28] = "DOWN1";
		m_upDownPos [32] = "DOWN2";
		m_upDownPos [38] = "START";

		m_event [6] = 24;
		m_event [9] = -1;
		m_event [11] = 18;
		m_event [21] = -3;
		m_event [23] = 35;
		m_event [28] = 1;
		m_event [32] = 25;
		m_event [33] = -2;
		m_event [38] = 0;
*/
//		SetDefaultPos ();

		m_path1 = iTweenPath.GetPath("PATH1");
		m_path2 = iTweenPath.GetPath("PATH2");

		m_player [0].transform.position = m_path1[0];
		m_player [1].transform.position = m_path2[0];
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_stateID) {
		case GameStateID.WaitRoll: 
			StartCoroutine (Roll ()); break;
		case GameStateID.Move: 
			StartCoroutine (GoNextPos ()); break;
		}
	}

	private IEnumerator Event(){
		int normalMode = 0;

		Debug.Log ("EVENT CHECK START");
		m_stateID = GameStateID.DoingEvent;
	//	m_state = "DoingEvent";
		if (m_upDownPos[m_currentPos[m_currID]] != null) {
			yield return StartCoroutine(UpDownEvent());
		} else if(m_event [m_currentPos[m_currID]] < normalMode){
			Debug.Log ("STOP TURN");
			m_eventStop[m_currID] = Mathf.Abs( m_event[m_currentPos[m_currID]]);
		}

		Debug.Log ("EVENT CHECK STOP");
		yield break;
	}

	private IEnumerator UpDownEvent(){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos[m_currID]);
		Debug.Log ("EVENT : " + m_upDownPos [m_currentPos[m_currID]]);

		iTween.MoveTo (m_player[m_currID], iTween.Hash("path", iTweenPath.GetPath(m_upDownPos[m_currentPos[m_currID]]), "time", 5));
		m_currentPos[m_currID] = m_event [m_currentPos[m_currID]];

		yield return new WaitForSeconds (2f);

		yield break;
	}

	private IEnumerator Roll(){
		int normalMode = 0;

		Debug.Log ("WAIT Player : " + (m_currID + 1).ToString() + " ROLL");

		bool hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick();
		float m_timeRandom = Random.Range(3F, 8F);

		m_stateID = GameStateID.Rolling;
	//	m_state = "Rolling";

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

			m_buttonRoll.GetComponent<Roll> ().SetClickDefualt ();

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

	private IEnumerator GoNextPos(){
		float moveSpeed = 0.04f;
		bool backward = false;
		int maxNode = 39;
		Vector3 nextPos;

		m_stateID = GameStateID.Moving;
		//m_state = "Moving";
		Debug.Log (m_stateID);
		for (int i = 1; i <= m_pointDice; i++) {

			Debug.Log("CURRENT POSITION : " + m_currentPos[m_currID]);

			if(m_currentPos[m_currID] >= maxNode)
				backward = true;

			if(backward){
				m_currentPos[m_currID] --;
			}
			else{
				m_currentPos[m_currID] ++;
			}

			if(m_playerID == PlayerID.Player1){
				nextPos = m_path1[m_currentPos[m_currID]];
			}
			else {
				nextPos = m_path2[m_currentPos[m_currID]];
			}
	

			Vector3 currPos = m_player[m_currID].transform.position;

	//		Debug.Log (m_currentPos);
	//		Debug.Log ("Curr Pos : " + currPos);
	//		Debug.Log ("Next Pos : " + nextPos);

			iTween.MoveTo(m_player[m_currID], nextPos, 4f);
			yield return new WaitForSeconds(1f);
			/*
			while (!Equal( (float)currPos.x, (float)nextPos.x) || !Equal( (float)currPos.y, (float)nextPos.y)) {
				if (!Equal ((float)currPos.x, (float)nextPos.x) && !Equal ((float)currPos.y, (float)nextPos.y)) {
					if (currPos.x > nextPos.x && currPos.y > nextPos.y) {
						currPos.x -= moveSpeed;
						currPos.y -= moveSpeed;
					} else if (currPos.x < nextPos.x && currPos.y < nextPos.y) {
						currPos.x += moveSpeed;
						currPos.y += moveSpeed;
					} else if (currPos.x > nextPos.x && currPos.y < nextPos.y) {
						currPos.x -= moveSpeed;
						currPos.y += moveSpeed;
					} else if (currPos.x < nextPos.x && currPos.y > nextPos.y) {
						currPos.x += moveSpeed;
						currPos.y -= moveSpeed;
					}
			
				} else if (!Equal ((float)currPos.x, (float)nextPos.x)) {
					if (currPos.x > nextPos.x) {
						currPos.x -= moveSpeed;
					} else {
						currPos.x += moveSpeed;
					}
				} else if (!Equal ((float)currPos.y, (float)nextPos.y)) {
					if (currPos.y > nextPos.y) {
						currPos.y -= moveSpeed;
					} else {
						currPos.y += moveSpeed;
					}
				}
			
				m_player[m_currID].transform.position = currPos;
				yield return null;
			}*/
		}

		yield return StartCoroutine(Event ());

		Debug.Log ("END TURN WAIT OTHER PLAYER");

		yield return new WaitForSeconds(0.5f);

		ChangePlayerTurn ();

		m_stateID = GameStateID.WaitRoll;
		//m_state = "WaitRoll";

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

	/*private void SetDefaultPos(){
		m_posPlayerX[0] =  0.97f;
		m_posPlayerY[0] = -3.1f;

		m_posPlayerX[1] = -0.04f;
		m_posPlayerY[1] = -3.1f;
		
		m_posPlayerX[2] = -0.795f;
		m_posPlayerY[2] = -3.1f;
		
		m_posPlayerX[3] = -1.57f;
		m_posPlayerY[3] = -3.1f;
		
		m_posPlayerX[4] = -2.34f;
		m_posPlayerY[4] = -3.1f;
		
		m_posPlayerX[5] = -3.11f;
		m_posPlayerY[5] = -3.1f;
		
		m_posPlayerX[6] = -3.88f;
		m_posPlayerY[6] = -3.1f;
		
		m_posPlayerX[7] = -4.59f;
		m_posPlayerY[7] = -3.1f;
		
		m_posPlayerX[8] = -5.13f;
		m_posPlayerY[8] = -2.95f;

		m_posPlayerX[9] = -5.15f;
		m_posPlayerY[9] = -2.494f;
		
		m_posPlayerX[10] = -5.15f;
		m_posPlayerY[10] = -2.163f;
		
		m_posPlayerX[11] = -4.99f;
		m_posPlayerY[11] = -1.79f;
	
		m_posPlayerX[12] = -4.60f;
		m_posPlayerY[12] = -1.41f;
		
		m_posPlayerX[13] = -3.92f;
		m_posPlayerY[13] = -1.41f;
		
		m_posPlayerX[14] = -3.16f;
		m_posPlayerY[14] = -1.41f;
		
		m_posPlayerX[15] = -2.4f;
		m_posPlayerY[15] = -1.41f;
		
		m_posPlayerX[16] = -1.63f;
		m_posPlayerY[16] = -1.41f;
		
		m_posPlayerX[17] = -0.78f;
		m_posPlayerY[17] = -1.41f;
		
		m_posPlayerX[18] = -0.06f;
		m_posPlayerY[18] = -1.05f;
		
		m_posPlayerX[19] = 0.2f;
		m_posPlayerY[19] = -0.48f;

		m_posPlayerX[20] = 0.2f;
		m_posPlayerY[20] =  0.1f;
		
		m_posPlayerX[21] = -0.09f;
		m_posPlayerY[21] =  0.65f;
		
		m_posPlayerX[22] = -0.73f;
		m_posPlayerY[22] = 1.17f;
		
		m_posPlayerX[23] = -1.60f;
		m_posPlayerY[23] =  1.17f;
		
		m_posPlayerX[24] = -2.38f;
		m_posPlayerY[24] = 1.17f;
		
		m_posPlayerX[25] = -3.13f;
		m_posPlayerY[25] = 1.17f;
		
		m_posPlayerX[26] = -3.89f;
		m_posPlayerY[26] = 1.17f;
		
		m_posPlayerX[27] = -4.56f;
		m_posPlayerY[27] = 1.17f;
		
		m_posPlayerX[28] = -4.92f;
		m_posPlayerY[28] = 1.47f;
		
		m_posPlayerX[29] = -5.03f;
		m_posPlayerY[29] = 1.81f;
		
		m_posPlayerX[30] = -5.03f;
		m_posPlayerY[30] = 2.13f;
		
		m_posPlayerX[31] = -4.93f;
		m_posPlayerY[31] = 2.49f;
		
		m_posPlayerX[32] = -4.48f;
		m_posPlayerY[32] = 2.67f;
		
		m_posPlayerX[33] = -3.88f;
		m_posPlayerY[33] = 2.67f;
		
		m_posPlayerX[34] = -3.10f;
		m_posPlayerY[34] = 2.67f;
		
		m_posPlayerX[35] = -2.34f;
		m_posPlayerY[35] = 2.67f;
		
		m_posPlayerX[36] = -1.56f;
		m_posPlayerY[36] = 2.67f;
		
		m_posPlayerX[37] = -0.77f;
		m_posPlayerY[37] = 2.67f;
		
		m_posPlayerX[38] = -0.01f;
		m_posPlayerY[38] = 2.67f;
		
		m_posPlayerX[39] = 1.06f;
		m_posPlayerY[39] = 2.67f;
	}*/
}
