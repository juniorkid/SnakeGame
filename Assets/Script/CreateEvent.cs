using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEvent : MonoBehaviour {

	public int m_maxEventStop;
	public int m_maxEventCard;
	public int m_maxEventFB;

	public GameObject[] m_stop;
	public GameObject[] m_upDown;
	public GameObject m_deck;

	public int m_noEvent = 0;

	public int m_lastPos;


	// Use to create all Event
	public void CreateAllEvent(List<GameObject> path){
		int maxNode = path.Count;

		Debug.Log (maxNode);
		int posEvent;
		Vector3 pos;

		EventClass eventObj = null;

		//Create Event Stop
		for(int i = 0 ; i < m_maxEventStop ;i++) {

			Debug.Log ("Create EVENT STOP : " + i.ToString());

			Debug.Log ("max node : " + maxNode);

			// Random position to create event
			posEvent = (int)Random.Range(1f, (float)(maxNode - 1));

			// Check that position don't have event
			if(path[posEvent].GetComponent<FloorProperties>().GetEvent() == null){

				// Random stop event
				int ranStop = (int)Random.Range (0f, 2.9f);

				// Set position to create stop event
				pos = path[posEvent].transform.position;
				pos.z = 0;

				// Create object stop event
				GameObject eventGameObj = (GameObject)Instantiate (m_stop[ranStop], pos, Quaternion.identity);
				eventObj = eventGameObj.GetComponent<EventClass>();
				path[posEvent].GetComponent<FloorProperties>().SetEvent(eventObj);
			}
			else {
				i--;
			}
		}

		// Create event UP DOWN

		// Set start position of UP DOWN event 
		posEvent = (int)(maxNode/6f);

		for(int i = 0 ; i < m_maxEventFB && posEvent < maxNode-1;i++) {
			Debug.Log ("Create EVENT FB : " + i.ToString());

			// Check that position don't have event
			int dest = 0;
			if(path[posEvent].GetComponent<FloorProperties>().GetEvent() == null){
				int ranUpDown = (int)Random.Range (1f, 11.9f);
				//Check for don't create if go back start and go over win

				if(posEvent - ranUpDown + 5 >= 0 && (posEvent + ranUpDown <= maxNode || ranUpDown > 5)){

					Debug.Log("Create Pos : " + posEvent);

					switch(ranUpDown){
					//UP
						case 0:	dest = posEvent + 1; break;
						case 1: dest = posEvent + 2; break;	
						case 2: dest = posEvent + 3; break;
						case 3:	dest = posEvent + 4; break;
						case 4: dest = posEvent + 5; break;	
						case 5: dest = posEvent + 6; break;

						//DOWN
						case 6:	dest = posEvent - 1; break;
						case 7: dest = posEvent - 2; break;	
						case 8: dest = posEvent - 3; break;
						case 9:	dest = posEvent - 4; break;
						case 10: dest = posEvent - 5; break;	
						case 11: dest = posEvent - 6; break;
					}

					// Set position and create event
					pos = path[posEvent].transform.position;
					pos.z = 0;
					GameObject eventGameObj = (GameObject)Instantiate (m_upDown[ranUpDown], pos, Quaternion.identity);
					eventObj = eventGameObj.GetComponent<EventClass>();
					eventObj.GetComponent<UpDownEvent>().SetPosUpDown(dest);
					path[posEvent].GetComponent<FloorProperties>().SetEvent(eventObj);

					// Set Distance between Event
					posEvent += (int)Random.Range((maxNode/6f),(maxNode/5f));

					Debug.Log("POS UP DOWN : " + posEvent);
				}
				else{
					i--;
				}
			}
			else {
				posEvent ++;
				i--;
			}
		}

		// Create Event Card

		for(int i = 0 ; i < m_maxEventCard ;i++) {

			// Random position to create event

			posEvent = (int)Random.Range(1f, (float)(maxNode - 1));

			//Debug.Log("EVENT CARD : " + m_isEventCard[posEvent]);

			// Check that position doesn't have event

			if(path[posEvent].GetComponent<FloorProperties>().GetEvent() == null){

				Debug.Log("PATH : " + path[posEvent]);

				// Set position and create object
				pos = path[posEvent].transform.position;
				pos.z = 0;
				GameObject eventGameObj = (GameObject)Instantiate (m_deck, pos, Quaternion.identity);
				eventObj = eventGameObj.GetComponent<EventClass>();
				path[posEvent].GetComponent<FloorProperties>().SetEvent(eventObj);
			}
			else {
				i--;
			}
		}

	}
}
