using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private int m_currentPos;
	public Camera m_mainCamera;
	float m_moveSpeed = 1f;
	// Use this for initialization
	public IEnumerator GoNextPos(int pointDice, List<Vector3> path, int maxNode){

		bool backward = false;
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

			iTween.MoveTo(gameObject, nextPos, m_moveSpeed);
			yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (nextPos));
			yield return new WaitForSeconds(0.5f);
			
		}
		
		yield break;
	}

	public IEnumerator DoUpDownEvent(int posEvent, List<Vector3> path){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos);

		Vector3 goPos;
		int upDown;

		if (posEvent < m_currentPos)
			upDown = -1;
		else
			upDown = 1;

		for (int i = m_currentPos + upDown; i != posEvent + upDown; i+=upDown) {

			Debug.Log("Pos : " + i);

			goPos = path[i];
			goPos.z = -2;

			iTween.MoveTo (gameObject, goPos, m_moveSpeed);

//		gameObject.transform.position = goPos;

			StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (goPos));

			yield return new WaitForSeconds(0.1f);
		}

		m_currentPos = posEvent ;
		
		yield return new WaitForSeconds (0.5f);
		
		yield break;
	}

	public int GetCurrentPos(){
		return m_currentPos;
	}
}
