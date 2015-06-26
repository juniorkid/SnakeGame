using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum FloorState
{
	CLeft,
	CRight,
	Normal,
	End,
	No,
}

public class Createfloor : MonoBehaviour {

	private List<GameObject> m_path;

	private FloorState m_floorStateID;

	public GameObject m_winFloor;
	public GameObject m_startFloor;

	public GameObject[] m_normalFloor;
	public GameObject[] m_circleLeft;
	public GameObject[] m_circleRight;

	public bool m_isLeft;

	public Vector3 m_lastFloorPos;

	private int m_numFloor;
	public int m_maxFloor;

	private int m_numCircle;
	private int m_maxCircle;

	private int[] m_eventStop;
	
	private string[] m_upDownPos;

	public GameObject[] m_stop;
	
	public TextMesh m_textNumFloor;

	public float m_leftX = 0 ;
	public float m_rightX = 0 ;

	public bool m_isCreateEnd;

	// Use this for initialization
	void Start () {

		GameObject floor;

		// Initialize List for keep path
		m_path = new List<GameObject> ();
		m_isCreateEnd = false;
		m_numFloor = 0;

		// Set initial direction that create floor

		m_isLeft = true;

		// Create Start floor

		floor = Instantiate (m_startFloor, new Vector3 (0.95f, -3.38f, 2), Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;
		m_path.Add (floor);

		// Set right X and left X
		m_rightX = 0.95f;
		m_leftX = 0.95f;

		// initial state that use to create each floor

		m_floorStateID = FloorState.Normal;

		// initial number of floor
		m_numFloor = 1;

		// Set maximum circle to limit floor
		m_maxCircle = m_maxFloor / 12;
	}
	// Update is called once per frame
	void Update () {

		// Check for create each floor
		if (m_numFloor < m_maxFloor) {

			switch (m_floorStateID) {
			case FloorState.Normal: 
				CreateNormalFloor ();
				break;

			case FloorState.CLeft:
				CreateCircleLeftFloor ();

				break;

			case FloorState.CRight:
				CreateCircleRightFloor ();
					
				break;

			case FloorState.End:
				CreateEndFloor ();

				break;

			case FloorState.No:
					
				break;
			}
		} else if (m_numFloor == m_maxFloor && !m_isCreateEnd) {
			Debug.Log("WILL CREATE END");
			CreateEndFloor ();
		}
	}

	private void CreateNormalFloor(){
		int lengthNFloor;
		float space;
		float posNum;
		GameObject floor;
		Vector3 thisFloorPos;

		// Random length floor
		lengthNFloor = (int)Random.Range (3f, 8f);

		// Change state to create left side
		if(m_isLeft){
			space = -3.05f;
			posNum = -1.3f;
			m_floorStateID = FloorState.CLeft;
			m_isLeft = false;
		}

		// Change state to create right side
		else {
			space = 3.05f;
			posNum = +1.7f;
			m_floorStateID = FloorState.CRight;
			m_isLeft = true;
		}
		
		for(int i = 0 ; i < lengthNFloor && m_numFloor < m_maxFloor ; i++){

			// Set space between floor
			m_lastFloorPos.x += space ;

			// Create floor
			floor = Instantiate (m_normalFloor[0], m_lastFloorPos, Quaternion.identity) as GameObject;
			// Set number floor
			m_textNumFloor.text = m_numFloor.ToString();
			// Set floor name
			floor.name = m_numFloor.ToString();

			// Set floor position
			thisFloorPos = m_lastFloorPos;

			thisFloorPos.z = 0;
			thisFloorPos.x += 0.6f;
			thisFloorPos.y += posNum;

			// Create normal floor
			Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);

			// Set last floor position
			m_lastFloorPos = floor.transform.position;		

			// Add floor in path
			m_path.Add(floor);
			m_numFloor++;

			// Find x in right and left
			FindX(floor.transform.position.x);
		}

		// Check for stop create floor 
		if(m_numCircle == m_maxCircle){
			m_floorStateID = FloorState.End;
		}
	}

	private void CreateCircleLeftFloor(){
		Vector3 thisFloorPos;
		GameObject floor;

		m_numCircle ++;

		// Create left circle 1
		m_lastFloorPos.x -= 2.8f ;
		m_lastFloorPos.y += 0.19f ;
		floor = Instantiate (m_circleLeft[0], m_lastFloorPos, Quaternion.identity) as GameObject;

		// Set floor name
		floor.name = m_numFloor.ToString();
		// Set last floor position
		m_lastFloorPos = floor.transform.position;		

		// Set floor number
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x += 0.4f;
		thisFloorPos.y -= 1.3f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);

		// Add floor in path
		m_path.Add(floor);
		m_numFloor++;

		// Find x in right and left
		FindX(floor.transform.position.x);

		// Create left circle 2
		m_lastFloorPos.x -= 2.11f ;
		m_lastFloorPos.y += 1.33f ;
		floor = Instantiate (m_circleLeft[1], m_lastFloorPos, Quaternion.identity) as GameObject;
		// Set floor name
		floor.name = m_numFloor.ToString();
		// Set last floor position
		m_lastFloorPos = floor.transform.position;		

		// Set floor number
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.1f;
		thisFloorPos.y -= 1.3f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);

		// Add floor in path
		m_path.Add(floor);
		m_numFloor++;

		// Find x in right and left
		FindX(floor.transform.position.x);

		// Create left circle 2
		m_lastFloorPos.x -= 0.97f ;
		m_lastFloorPos.y += 1.62f ;
		floor = Instantiate (m_circleLeft[2], m_lastFloorPos, Quaternion.identity) as GameObject;
		// Set floor name
		floor.name = m_numFloor.ToString();
		// Set last floor position
		m_lastFloorPos = floor.transform.position;	

		// Set floor number
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.4f;
		thisFloorPos.y -= 0.4f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);

		// Add floor in path
		m_path.Add(floor);
		m_numFloor++;

		// Find x in right and left
		FindX(floor.transform.position.x);

		// Create left circle 3
		m_lastFloorPos.x += 0.11f ;
		m_lastFloorPos.y += 1.54f ;
		floor = Instantiate (m_circleLeft[3], m_lastFloorPos, Quaternion.identity) as GameObject;
		// Set floor name
		floor.name = m_numFloor.ToString();
		// Set last floor position
		m_lastFloorPos = floor.transform.position;	

		// Set floor number
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.55f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
	
		// Add floor in path
		m_path.Add(floor);
		m_numFloor++;

		// Find x in right and left
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x += 0.62f ;
		m_lastFloorPos.y += 1.79f ;
		floor = Instantiate (m_circleLeft[4], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.3f;
		thisFloorPos.y += 1.3f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x += 2.23f ;
		m_lastFloorPos.y += 1.29f ;
		floor = Instantiate (m_circleLeft[5], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.0f;
		thisFloorPos.y += 1.8f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.y += 0.31f;
		m_lastFloorPos.x -= 0.24f;

		// Set state to create normal floor
		m_floorStateID = FloorState.Normal;
	}

	// Create right floor
	private void CreateCircleRightFloor(){
		Vector3 thisFloorPos;
		GameObject floor;
		m_numCircle ++;
		
		m_lastFloorPos.x += 2.85f ;
		m_lastFloorPos.y += 0.17f ;
		floor = Instantiate (m_circleRight[0], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 0.6f;
		thisFloorPos.y += 1.5f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x += 2.15f ;
		m_lastFloorPos.y += 1.36f ;
		floor = Instantiate (m_circleRight[1], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.2f;
		thisFloorPos.y += 1f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x += 0.98f ;
		m_lastFloorPos.y += 1.65f ;
		floor = Instantiate (m_circleRight[2], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.6f;
		thisFloorPos.y += 0.6f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x -= 0.15f ;
		m_lastFloorPos.y += 1.51f ;
		floor = Instantiate (m_circleRight[3], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.4f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x -= 0.64f ;
		m_lastFloorPos.y += 1.82f ;
		floor = Instantiate (m_circleRight[4], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 1.6f;
		thisFloorPos.y -= 0.6f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		
		m_lastFloorPos.x -= 2.34f ;
		m_lastFloorPos.y += 1.22f ;
		floor = Instantiate (m_circleRight[5], m_lastFloorPos, Quaternion.identity) as GameObject;
		floor.name = m_numFloor.ToString();
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.text = m_numFloor.ToString();
		thisFloorPos = m_lastFloorPos;
		thisFloorPos.z = 0;
		thisFloorPos.x -= 0.4f;
		thisFloorPos.y -= 1.2f;
		Instantiate (m_textNumFloor, thisFloorPos, Quaternion.identity);
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);

		m_lastFloorPos.y += 0.25f;
		m_lastFloorPos.x -= -0.2f;

		// Set state to create normal floor
		m_floorStateID = FloorState.Normal;
	}

	// Create end floor
	private void CreateEndFloor(){
		Debug.Log ("Create End Floor");
		Debug.Log ("NUM FLOOR : " + m_numFloor);
		// Change state to not create floor
		m_floorStateID = FloorState.No;

		GameObject floor;
		float space;

		if(m_isLeft){
			space = 4.32f ;
		//	m_floorStateID = FloorState.CLeft;
		//	m_isLeft =true;
		}
		else {
			space = -4.32f ;
		//	m_floorStateID = FloorState.CRight;
		//	m_isLeft =false;
		}

		m_lastFloorPos.x += space ;
		m_lastFloorPos.y += 0.15f;
		floor = Instantiate (m_winFloor, m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_path.Add(floor);
		m_numFloor++;
		FindX(floor.transform.position.x);
		m_isCreateEnd = true;
	}

	// Get all path
	public List<GameObject> GetPath(){
		return m_path;
	}
	
	// Find maximum X
	private void FindX(float x){
		if(m_leftX > x)
			m_leftX = x;
		else if(m_rightX < x)
			m_rightX = x;
	}

	// Return X right side
	public float GetRightX(){
		return m_rightX;
	}

	// Return X left side
	public float GetLeftX(){
		return m_leftX;
	}

}
