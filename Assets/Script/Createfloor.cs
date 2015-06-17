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

	private List<Vector3> m_path;

	private FloorState m_floorStateID;

	public GameObject m_winFloor;
	public GameObject m_startFloor;

	public GameObject[] m_normalFloor;
	public GameObject[] m_circleLeft;
	public GameObject[] m_circleRight;

	public bool m_hasLeft;
	public bool m_hasRight;

	public Vector3 m_lastFloorPos;

	public int m_numFloor;
	public int m_maxFloor;

	public int m_numCircle;
	public int m_maxCircle;

	public int m_lengthNormal;

	private int[] m_eventStop;
	
	private string[] m_upDownPos;

	public GameObject[] m_stop;

	public GameObject m_textNumFloor;

	// Use this for initialization
	void Start () {
		GameObject floor;
		m_path = new List<Vector3> ();

		m_numFloor = 0;

		m_hasLeft = false;
		m_hasRight = false;

		floor = Instantiate (m_startFloor, new Vector3 (0.95f, -3.38f, 2), Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;
		m_path.Add (m_lastFloorPos);

		m_floorStateID = FloorState.Normal;

		m_numFloor = 1;
		m_maxFloor = 100;

		m_maxCircle = (int)Random.Range (4f, 8f);
	}
	// Update is called once per frame
	void Update () {

		if (m_numFloor < m_maxFloor) {

			switch(m_floorStateID){
				case FloorState.Normal : 
					CreateNormalFloor();
					break;

				case FloorState.CLeft :
					CreateCircleLeftFloor();

					break;

				case FloorState.CRight :
					CreateCircleRightFloor();
					
					break;

				case FloorState.End :
					CreateEndFloor();

					break;

				case FloorState.No :
					
					break;
			}
		}
	}

	private void CreateNormalFloor(){
		int rangeNFloor;
		float space;
		float posNum;
		GameObject floor;
		Vector3 numFloorPos;

		rangeNFloor = (int)Random.Range (4f, 6f);
		if(!m_hasLeft){
			m_lengthNormal = rangeNFloor;
			space = -2.95f;
			posNum = -1.3f;
			m_floorStateID = FloorState.CLeft;
			m_hasLeft =true;
		}
		else {
			if(m_lengthNormal < rangeNFloor)
				rangeNFloor = m_lengthNormal;
			space = 2.95f;
			posNum = +1.7f;
			m_floorStateID = FloorState.CRight;
			m_hasLeft =false;
		}
		
		for(int i = 0 ; i < rangeNFloor ; i++){
			m_lastFloorPos.x += space ;

			floor = Instantiate (m_normalFloor[0], m_lastFloorPos, Quaternion.identity) as GameObject;
			m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();

			numFloorPos = m_lastFloorPos;

			numFloorPos.z = 0;
			numFloorPos.x += 0.6f;
			numFloorPos.y += posNum;

			Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);

			m_lastFloorPos = floor.transform.position;		
			m_path.Add(m_lastFloorPos);
			m_numFloor++;
		}
		
		if(m_numCircle == m_maxCircle){
			m_floorStateID = FloorState.End;
		}
	}

	private void CreateCircleLeftFloor(){
		Vector3 numFloorPos;
		GameObject floor;

		m_numCircle ++;
		
		m_lastFloorPos.x -= 2.7f ;
		m_lastFloorPos.y += 0.19f ;
		floor = Instantiate (m_circleLeft[0], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x += 0.4f;
		numFloorPos.y -= 1.3f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x -= 2.11f ;
		m_lastFloorPos.y += 1.33f ;
		floor = Instantiate (m_circleLeft[1], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.1f;
		numFloorPos.y -= 1.3f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x -= 0.97f ;
		m_lastFloorPos.y += 1.62f ;
		floor = Instantiate (m_circleLeft[2], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.4f;
		numFloorPos.y -= 0.4f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x += 0.11f ;
		m_lastFloorPos.y += 1.54f ;
		floor = Instantiate (m_circleLeft[3], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.55f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x += 0.62f ;
		m_lastFloorPos.y += 1.79f ;
		floor = Instantiate (m_circleLeft[4], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.3f;
		numFloorPos.y += 1.3f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x += 2.23f ;
		m_lastFloorPos.y += 1.29f ;
		floor = Instantiate (m_circleLeft[5], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.0f;
		numFloorPos.y += 1.8f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.y += 0.31f;
		m_lastFloorPos.x -= 0.24f;

		m_floorStateID = FloorState.Normal;
	}

	private void CreateCircleRightFloor(){
		Vector3 numFloorPos;
		GameObject floor;
		m_numCircle ++;
		
		m_lastFloorPos.x += 2.75f ;
		m_lastFloorPos.y += 0.17f ;
		floor = Instantiate (m_circleRight[0], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 0.6f;
		numFloorPos.y += 1.5f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x += 2.15f ;
		m_lastFloorPos.y += 1.36f ;
		floor = Instantiate (m_circleRight[1], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.2f;
		numFloorPos.y += 1f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x += 0.98f ;
		m_lastFloorPos.y += 1.65f ;
		floor = Instantiate (m_circleRight[2], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.6f;
		numFloorPos.y += 0.6f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x -= 0.15f ;
		m_lastFloorPos.y += 1.51f ;
		floor = Instantiate (m_circleRight[3], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;	
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.4f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x -= 0.64f ;
		m_lastFloorPos.y += 1.82f ;
		floor = Instantiate (m_circleRight[4], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 1.6f;
		numFloorPos.y -= 0.6f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_lastFloorPos.x -= 2.34f ;
		m_lastFloorPos.y += 1.22f ;
		floor = Instantiate (m_circleRight[5], m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_textNumFloor.GetComponent<TextMesh>().text = m_numFloor.ToString();
		numFloorPos = m_lastFloorPos;
		numFloorPos.z = 0;
		numFloorPos.x -= 0.4f;
		numFloorPos.y -= 1.2f;
		Instantiate (m_textNumFloor, numFloorPos, Quaternion.identity);
		m_path.Add(m_lastFloorPos);
		m_numFloor++;

		m_lastFloorPos.y += 0.25f;
		m_lastFloorPos.x -= -0.2f;

		m_floorStateID = FloorState.Normal;
	}

	private void CreateEndFloor(){
		GameObject floor;
		float space;

		if(!m_hasLeft){
			space = 4.32f ;
			m_floorStateID = FloorState.CLeft;
			m_hasLeft =true;
		}
		else {
			space = -4.32f ;
			m_floorStateID = FloorState.CRight;
			m_hasLeft =false;
		}

		m_lastFloorPos.x += space ;
		m_lastFloorPos.y += 0.15f;
		floor = Instantiate (m_winFloor, m_lastFloorPos, Quaternion.identity) as GameObject;
		m_lastFloorPos = floor.transform.position;		
		m_path.Add(m_lastFloorPos);
		m_numFloor++;
		
		m_maxFloor = m_numFloor;
	}

	public List<Vector3> GetPath(){
		return m_path;
	}

	private void CreateNumFloor(Vector3 pos){

	}
	
}
