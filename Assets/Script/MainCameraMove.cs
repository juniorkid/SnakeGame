using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCameraMove : MonoBehaviour {

	private GameObject m_startObject;

	public GameObject m_dice;
	public GameObject m_button;

	float m_moveSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public IEnumerator FindStartPos(){
		m_dice.SetActive (false);
		m_button.SetActive (false);

		yield return new WaitForSeconds (1f);

		m_startObject = GameObject.FindWithTag ("Start");
		Vector3 pos = m_startObject.transform.position;

		pos.z = -10;
		pos.y += 1.47f;

		transform.position = pos;
	}

	public IEnumerator MoveCameraFollowPath(List<Vector3> path){
		m_dice.SetActive (false);
		m_button.SetActive (false);

		Vector3 currPos;

	//	yield return new WaitForSeconds (1f);
		
		foreach(Vector3 pos in path){
		//	Debug.Log (pos);

			currPos = pos;
			currPos.z = -10;
			currPos.y += 1.47f;

			iTween.MoveTo(gameObject, currPos, m_moveSpeed + 2);

			yield return new WaitForSeconds(.2f);
		}

		yield break;	
	}

	public IEnumerator SetPosition(Vector3 pos){

		//m_dice.SetActive (false);
		m_button.SetActive (false);

		Debug.Log ("SET CAMERA : " + pos);

		pos.z = -10;
		pos.y += 0.47f;
		pos.x += 0.47f;

		iTween.MoveTo(gameObject, pos, m_moveSpeed);

		yield return new WaitForSeconds (0.5f);

		Debug.Log ("SET POSITION");

		yield break;
	}

	public void ShowDiceButton(){

	///	yield return new WaitForSeconds (0.1f);

		Debug.Log ("SHOW DICE AND BUTTON");

		m_dice.SetActive (true);
		m_button.SetActive (true);
	}
}
