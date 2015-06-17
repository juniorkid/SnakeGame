using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEvent : MonoBehaviour {

	public int m_maxEventStop;
	public int m_maxEventFB;

	private int[] m_event;

	public GameObject[] m_stop;
	public GameObject[] m_ForwardDown;

	public int m_lastPos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateAllEvent(List<Vector3> path, int[] eventAll){
		m_event = eventAll;
		int maxNode = path.Count;
		Debug.Log (maxNode);
		int posEvent;
		Vector3 pos;

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

		for(int i = 0 ; i < m_maxEventFB ;i++) {
			Debug.Log ("Create EVENT FB : " + i.ToString());
			posEvent = (int)Random.Range(1f, (float)(maxNode - 2));
			
			if(m_event[posEvent] == 0){
				int ranStop = (int)Random.Range (1f, 11.9f);
				if(posEvent - ranStop + 5 >= 0 && posEvent + ranStop <= maxNode){
					switch(ranStop){
					//UP
					case 0:	m_event[posEvent] = posEvent + 1; break;
					case 1: m_event[posEvent] = posEvent + 2; break;	
					case 2: m_event[posEvent] = posEvent + 3; break;
					case 3:	m_event[posEvent] = posEvent + 4; break;
					case 4: m_event[posEvent] = posEvent + 5; break;	
					case 5: m_event[posEvent] = posEvent + 6; break;

					//DOWN
					case 6:	m_event[posEvent] = posEvent - 1; break;
					case 7: m_event[posEvent] = posEvent - 2; break;	
					case 8: m_event[posEvent] = posEvent - 3; break;
					case 9:	m_event[posEvent] = posEvent - 4; break;
					case 10: m_event[posEvent] = posEvent - 5; break;	
					case 11: m_event[posEvent] = posEvent - 6; break;
					}

					pos = path[posEvent];
					pos.z = 0;
					Instantiate (m_ForwardDown[ranStop], pos, Quaternion.identity);
				}
				else{
					i--;
				}
			}
			else {
				i--;
			}
		}

	}

	public int[] GetEvent(){
		return m_event;
	}
}
