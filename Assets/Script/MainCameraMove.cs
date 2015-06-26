using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCameraMove : MonoBehaviour {

	private GameObject m_startObject;

	public GameObject m_dice;
	public GameObject m_button;

	private DragCamera m_dragCamera;

	float m_moveSpeed = 1f;

	// Use this for initialization
	void Start () { 
		m_dragCamera = gameObject.GetComponent<DragCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Set camera to Start Position
	public IEnumerator FindStartPos(){

		// Set to hide dice and button roll
		m_dice.SetActive (false);
		m_button.SetActive (false);

		yield return new WaitForSeconds (1f);

		m_startObject = GameObject.FindWithTag ("Start");
		Vector3 pos = m_startObject.transform.position;

		pos.z = -10;
		pos.y += 1.47f;

		transform.position = pos;
	}

	// Move camera along path at start game
	public IEnumerator MoveCameraFollowPath(List<GameObject> path){

		// Set to hide dice and button roll
		m_dice.SetActive (false);
		m_button.SetActive (false);

		Vector3 currPos;

	//	yield return new WaitForSeconds (1f);
		
		foreach(GameObject pos in path){
		//	Debug.Log (pos);

			currPos = pos.transform.position;
			currPos.z = -10;
			currPos.y += 1.47f;

			// Move camera to position
			iTween.MoveTo(gameObject, currPos, m_moveSpeed + 2);

			yield return new WaitForSeconds(.2f);
		}

		yield break;	
	}

	// Move camera to player position when player move
	public IEnumerator SetPosition(Vector3 pos){

		// Set can't drag between move camera
		m_dragCamera.SetIsDrag(false);
		//m_dice.SetActive (false);

		// Hide button
		m_button.SetActive (false);

		Debug.Log ("SET CAMERA : " + pos);

		pos.z = -10;
	//	pos.y += 0.47f;
	//	pos.x += 0.47f;

		// Move to position

		iTween.MoveTo(gameObject, pos, m_moveSpeed);

		// Delay wait camera move

		yield return new WaitForSeconds (0.5f);

		Debug.Log ("SET POSITION");

		m_dragCamera.SetIsDrag(true);

		yield break;
	}

	// Show dice and button
	public void ShowDiceButton(){

	///	yield return new WaitForSeconds (0.1f);

		Debug.Log ("SHOW DICE AND BUTTON");

		m_dice.SetActive (true);
		m_button.SetActive (true);
	}
}
