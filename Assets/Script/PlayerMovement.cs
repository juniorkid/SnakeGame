using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private int m_currentPos;

	public Camera m_mainCamera;

	// Use this for initialization
	public IEnumerator GoNextPos(int pointDice, List<Vector3> path){
		float moveSpeed = 1f;
		bool backward = false;
		int maxNode = 39;
		Vector3 nextPos;

		for (int i = 1; i <= pointDice; i++) {
			
			Debug.Log("CURRENT POSITION : " + m_currentPos);
			
			if(m_currentPos >= maxNode)
				backward = true;
			
			if(backward){
				m_currentPos --;
			}
			else{
				m_currentPos ++;
			}

			nextPos = path[m_currentPos];

			//		Debug.Log (m_currentPos);
			//		Debug.Log ("Curr Pos : " + currPos);
			//		Debug.Log ("Next Pos : " + nextPos);

			nextPos.z = -2;

			iTween.MoveTo(gameObject, nextPos, moveSpeed);
			yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosiotion (nextPos));
	//		yield return new WaitForSeconds(0.5f);
			
		}
		
		yield break;
	}

	public IEnumerator DoUpDownEvent(int posEvent, Vector3 goPos){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos);

		goPos.z = -2;

		iTween.MoveTo (gameObject, goPos, 5);

		gameObject.transform.position = goPos;

		m_currentPos = posEvent ;
		
		yield return new WaitForSeconds (2f);
		
		yield break;
	}

	public int GetCurrentPos(){
		return m_currentPos;
	}
}
