using UnityEngine;
using System.Collections;

public class Gamecontroller : MonoBehaviour {

	private float[] m_posPlayerX;
	private float[] m_posPlayerY;
	private bool[] m_posPlayer;
	private int[] m_event;

	private int m_eventStop;

	private string[] m_upDownPos;

	private int m_currentPos;
	private int m_pointDice;

	public GameObject m_player;

	private string m_state;

	public GameObject m_dice;

	public GameObject m_buttonRoll;

	// Use this for initialization
	void Start () {
		m_eventStop = 0;

		m_state = "WaitRoll";
		m_currentPos = 0;
		m_posPlayerX = new float[40];
		m_posPlayerY = new float[40];
		m_posPlayer = new bool[40];
		m_event = new int[40];
		m_upDownPos = new string[40];

		m_upDownPos [6] = "UP1";
		m_upDownPos [11] = "UP2";
		m_upDownPos [23] = "UP3";
		m_upDownPos [28] = "DOWN1";
		m_upDownPos [32] = "DOWN2";
		m_upDownPos [38] = "START";

		m_event [6] = 24;
		m_event [11] = 18;
		m_event [23] = 35;
		m_event [28] = 1;
		m_event [32] = 25;
		m_event [38] = 0;

		Debug.Log ("EVENT : " + m_upDownPos [6]);

		SetDefaultPos ();

		m_player.transform.position = new Vector3 ( m_posPlayerX[0], m_posPlayerY[0], -2);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_state == "Move")
			StartCoroutine (GoNextPos ());
		if (m_state == "WaitRoll")
			StartCoroutine (Roll ());
	}

	private IEnumerator Event(){
		Debug.Log ("EVENT CHECK START");

		m_state = "DoingEvent";
		if (m_upDownPos[m_currentPos] != null) {
			yield return StartCoroutine(UpDownEvent());
		} else if(m_event [m_currentPos] < 0){
			m_eventStop = Mathf.Abs( m_event[m_currentPos]);
		}
		yield break;
	}

	private IEnumerator UpDownEvent(){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos);
		Debug.Log ("EVENT : " + m_upDownPos [m_currentPos]);

		iTween.MoveTo (m_player, iTween.Hash("path", iTweenPath.GetPath(m_upDownPos[m_currentPos]), "time", 5));
		m_currentPos = m_event [m_currentPos];

		yield break;
	}

	private IEnumerator Roll(){
		bool hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick();
		float m_timeRandom = Random.Range(4F, 7F);

		m_state = "Rolling";

		while(!hasClick){
			hasClick = m_buttonRoll.GetComponent<Roll> ().GetClick();
			yield return null;
		}

		m_dice.GetComponent<Dice> ().StartRoll ();



		yield return new WaitForSeconds (m_timeRandom);
		m_dice.GetComponent<Dice> ().StopRoll ();

		Debug.Log ("BEFORE : " + m_pointDice);

		m_pointDice = m_dice.GetComponent<Dice> ().GetPointDice () + 1;

	//	m_currentPos = 31;
	//	m_pointDice = 1;

		Debug.Log ("After : " + m_pointDice);

		m_buttonRoll.GetComponent<Roll> ().SetClickDefualt();
		m_state = "Move";

		yield break;
	}

	private IEnumerator GoNextPos(){
		m_state = "Moving";
		Debug.Log (m_state);
		for (int i = 1; i <= m_pointDice; i++) {
			if(m_currentPos < 39)
				m_currentPos ++;
			Vector3 nextPos = new Vector3 (m_posPlayerX [m_currentPos], m_posPlayerY [m_currentPos], -2f);
			Vector3 currPos = m_player.transform.position;



	//		Debug.Log (m_currentPos);
	//		Debug.Log ("Curr Pos : " + currPos);
	//		Debug.Log ("Next Pos : " + nextPos);


			while (!Equal( currPos.x, nextPos.x) || !Equal( currPos.y, nextPos.y)) {
				if (!Equal (currPos.x, nextPos.x) && !Equal (currPos.y, nextPos.y)) {
					if (currPos.x > nextPos.x && currPos.y > nextPos.y) {
						currPos.x -= 0.01f;
						currPos.y -= 0.01f;
					} else if (currPos.x < nextPos.x && currPos.y < nextPos.y) {
						currPos.x += 0.01f;
						currPos.y += 0.01f;
					} else if (currPos.x > nextPos.x && currPos.y < nextPos.y) {
						currPos.x -= 0.01f;
						currPos.y += 0.01f;
					} else if (currPos.x < nextPos.x && currPos.y > nextPos.y) {
						currPos.x += 0.01f;
						currPos.y -= 0.01f;
					}
			
				} else if (!Equal (currPos.x, nextPos.x)) {
					if (currPos.x > nextPos.x) {
						currPos.x -= 0.01f;
					} else {
						currPos.x += 0.01f;
					}
				} else if (!Equal (currPos.y, nextPos.y)) {
					if (currPos.y > nextPos.y) {
						currPos.y -= 0.01f;
					} else {
						currPos.y += 0.01f;
					}
				}
			
				m_player.transform.position = currPos;
				yield return null;
			}
		}

		yield return StartCoroutine(Event ());

		Debug.Log ("WAIT FOR GO NEXT POS");

		yield return new WaitForSeconds(0.5f);


		m_state = "WaitRoll";

		yield break;
	}

	private bool Equal(float x , float y){
		if (x > y - 0.1 && x < y + 0.1) {
			return true;
		} else
			return false;
	}

	private void SetDefaultPos(){
		m_posPlayerX[0] = 5.76f;
		m_posPlayerY[0] = -2.92f;
		m_posPlayer[0] = true;
		
		m_posPlayerX[1] = 4.85f;
		m_posPlayerY[1] = -2.92f;
		m_posPlayer[1] = false;
		
		m_posPlayerX[2] = 4.13f;
		m_posPlayerY[2] = -2.92f;
		m_posPlayer[2] = false;
		
		m_posPlayerX[3] = 3.39f;
		m_posPlayerY[3] = -2.92f;
		m_posPlayer[3] = false;
		
		m_posPlayerX[4] = 2.69f;
		m_posPlayerY[4] = -2.92f;
		m_posPlayer[4] = false;
		
		m_posPlayerX[5] = 2.03f;
		m_posPlayerY[5] = -2.92f;
		m_posPlayer[5] = false;
		
		m_posPlayerX[6] = 1.31f;
		m_posPlayerY[6] = -2.92f;
		m_posPlayer[6] = false;
		
		m_posPlayerX[7] = 0.67f;
		m_posPlayerY[7] = -2.92f;
		m_posPlayer[7] = false;
		
		m_posPlayerX[8] = -0.19f;
		m_posPlayerY[8] = -2.92f;
		m_posPlayer[8] = false;
		
		m_posPlayerX[9] = -0.93f;
		m_posPlayerY[9] = -2.92f;
		m_posPlayer[9] = false;
		
		m_posPlayerX[10] = -1.65f;
		m_posPlayerY[10] = -2.92f;
		m_posPlayer[10] = false;
		
		m_posPlayerX[11] = -2.41f;
		m_posPlayerY[11] = -2.92f;
		m_posPlayer[11] = false;
		
		m_posPlayerX[12] = -2.99f;
		m_posPlayerY[12] = -2.92f;
		m_posPlayer[12] = false;
		
		m_posPlayerX[13] = -3.75f;
		m_posPlayerY[13] = -2.92f;
		m_posPlayer[13] = false;
		
		m_posPlayerX[14] = -4.63f;
		m_posPlayerY[14] = -2.67f;
		m_posPlayer[14] = false;
		
		m_posPlayerX[15] = -4.63f;
		m_posPlayerY[15] = -1.83f;
		m_posPlayer[15] = false;
		
		m_posPlayerX[16] = -4.12f;
		m_posPlayerY[16] = -1.15f;
		m_posPlayer[16] = false;
		
		m_posPlayerX[17] = -3.59f;
		m_posPlayerY[17] = -0.97f;
		m_posPlayer[17] = false;
		
		m_posPlayerX[18] = -3.03f;
		m_posPlayerY[18] =  -0.97f;
		m_posPlayer[18] = false;
		
		m_posPlayerX[19] = -2.41f;
		m_posPlayerY[19] =  -0.97f;
		m_posPlayer[19] = false;
		
		m_posPlayerX[20] = -1.77f;
		m_posPlayerY[20] =  -0.97f;
		m_posPlayer[20] = false;
		
		m_posPlayerX[21] = -1.26f;
		m_posPlayerY[21] =  -0.97f;
		m_posPlayer[21] = false;
		
		m_posPlayerX[22] = -0.52f;
		m_posPlayerY[22] =  -0.97f;
		m_posPlayer[22] = false;
		
		m_posPlayerX[23] = 0.2f;
		m_posPlayerY[23] =  -0.97f;
		m_posPlayer[23] = false;
		
		m_posPlayerX[24] = 0.96f;
		m_posPlayerY[24] = -0.6f;
		m_posPlayer[24] = false;
		
		m_posPlayerX[25] = 1.66f;
		m_posPlayerY[25] = -0.6f;
		m_posPlayer[25] = false;
		
		m_posPlayerX[26] = 2.36f;
		m_posPlayerY[26] = -0.35f;
		m_posPlayer[26] = false;
		
		m_posPlayerX[27] = 3.02f;
		m_posPlayerY[27] = -0.35f;
		m_posPlayer[27] = false;
		
		m_posPlayerX[28] = 3.78f;
		m_posPlayerY[28] = 0.1f;
		m_posPlayer[28] = false;
		
		m_posPlayerX[29] = 4.12f;
		m_posPlayerY[29] = 0.84f;
		m_posPlayer[29] = false;
		
		m_posPlayerX[30] = 4.12f;
		m_posPlayerY[30] = 1.68f;
		m_posPlayer[30] = false;
		
		m_posPlayerX[31] = 3.32f;
		m_posPlayerY[31] = 2.26f;
		m_posPlayer[31] = false;
		
		m_posPlayerX[32] = 2.5f;
		m_posPlayerY[32] = 2.67f;
		m_posPlayer[32] = false;
		
		m_posPlayerX[33] = 1.37f;
		m_posPlayerY[33] = 2.67f;
		m_posPlayer[33] = false;
		
		m_posPlayerX[34] = 0.51f;
		m_posPlayerY[34] = 2.67f;
		m_posPlayer[34] = false;
		
		m_posPlayerX[35] = -0.46f;
		m_posPlayerY[35] = 2.67f;
		m_posPlayer[35] = false;
		
		m_posPlayerX[36] = -1.51f;
		m_posPlayerY[36] = 2.67f;
		m_posPlayer[36] = false;
		
		m_posPlayerX[37] = -2.37f;
		m_posPlayerY[37] = 2.67f;
		m_posPlayer[37] = false;
		
		m_posPlayerX[38] = -3.34f;
		m_posPlayerY[38] = 2.67f;
		m_posPlayer[38] = false;
		
		m_posPlayerX[39] = -4.41f;
		m_posPlayerY[39] = 2.67f;
		m_posPlayer[39] = false;
	}
}
