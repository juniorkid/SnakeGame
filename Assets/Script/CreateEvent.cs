using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEvent : MonoBehaviour {

	public int m_maxEventStop;

	private int[] m_event;

	public GameObject[] m_stop;

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
			posEvent = (int)Random.Range(1f, (float)(maxNode - 1));

			if(m_event[posEvent] >= 0){
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
	}

	public int[] GetEvent(){
		return m_event;
	}
}
