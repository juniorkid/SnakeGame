using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEvent : MonoBehaviour {

	public int m_maxEventStop;
	public int m_maxEventCard;
	public int m_maxEventFB;

	private int[] m_event;

	private bool[] m_eventCard;

	public GameObject[] m_stop;
	public GameObject[] m_ForwardDown;
	public GameObject m_deck;

	public int m_lastPos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateAllEvent(List<Vector3> path, int[] eventAll, bool[] eventCard){
		m_event = eventAll;
		m_eventCard = eventCard;
		int maxNode = path.Count;

		Debug.Log (maxNode);
		int posEvent;
		Vector3 pos;

		//Create Event Stop
		for(int i = 0 ; i < m_maxEventStop ;i++) {
			Debug.Log ("Create EVENT STOP : " + i.ToString());

			Debug.Log ("max node : " + maxNode);

			posEvent = (int)Random.Range(1f, (float)(maxNode - 1));

			if(m_event[posEvent] == 0){
				int ranStop = (int)Random.Range (0f, 2.9f);
				switch(ranStop){
					case 0:	m_event[posEvent] = -1; break;
					case 1: m_event[posEvent] = -2; break;	
					case 2: m_event[posEvent] = -3; break;
				}
				pos = path[posEvent];
				pos.z = 0;
				Instantiate (m_stop[ranStop], pos, Quaternion.identity);
			}
			else {
				i--;
			}
		}

		// Create event UP DOWN

		int posUpDown = (int)(maxNode/6f);
		posEvent = 0;

		for(int i = 0 ; i < m_maxEventFB ;i++) {
			Debug.Log ("Create EVENT FB : " + i.ToString());
			
			if(m_event[posUpDown] == 0){
				int ranStop = (int)Random.Range (1f, 11.9f);

				//Check for don't go back start and go over win

				if(posUpDown - ranStop + 5 >= 0 && (posEvent + ranStop <= maxNode || ranStop > 5)){

					Debug.Log("Create Pos");

					switch(ranStop){
					//UP
					case 0:	m_event[posUpDown] = posUpDown + 1; break;
					case 1: m_event[posUpDown] = posUpDown + 2; break;	
					case 2: m_event[posUpDown] = posUpDown + 3; break;
					case 3:	m_event[posUpDown] = posUpDown + 4; break;
					case 4: m_event[posUpDown] = posUpDown + 5; break;	
					case 5: m_event[posUpDown] = posUpDown + 6; break;

					//DOWN
					case 6:	m_event[posUpDown] = posUpDown - 1; break;
					case 7: m_event[posUpDown] = posUpDown - 2; break;	
					case 8: m_event[posUpDown] = posUpDown - 3; break;
					case 9:	m_event[posUpDown] = posUpDown - 4; break;
					case 10: m_event[posUpDown] = posUpDown - 5; break;	
					case 11: m_event[posUpDown] = posUpDown - 6; break;
					}

					pos = path[posUpDown];
					pos.z = 0;
					Instantiate (m_ForwardDown[ranStop], pos, Quaternion.identity);

					// Distance between Event
					posUpDown += (int)Random.Range((maxNode/6f),(maxNode/5f));
				}
				else{
					i--;
				}
			}
			else {
				i--;
			}
		}

		// Create Event Card

		for(int i = 0 ; i < m_maxEventCard ;i++) {
			
			Debug.Log ("max node : " + maxNode);
			
			posEvent = (int)Random.Range(1f, (float)(maxNode - 1));

			Debug.Log("EVENT CARD : " + m_eventCard[posEvent]);

			if(m_eventCard[posEvent] == false && m_event[posEvent] == 0){

				Debug.Log ("Create EVENT Card : " + i.ToString());

				m_eventCard[posEvent] = true;

				pos = path[posEvent];
				pos.z = 0;
				Instantiate (m_deck, pos, Quaternion.identity);
			}
			else {
				i--;
			}
		}

	}

	public int[] GetEvent(){
		return m_event;
	}

	public bool[] GetPosEventCard(){
		return m_eventCard;
	}
}
