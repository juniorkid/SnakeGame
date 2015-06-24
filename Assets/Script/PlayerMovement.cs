using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private int m_currentPos;
	public Camera m_mainCamera;
	float m_moveSpeed = 1f;

	// Use to go next position by pointDice
	public IEnumerator GoNextPos(int pointDice, List<Vector3> path, int maxNode){

		bool backward = false;
		Vector3 nextPos;

		for (int i = 1; i <= pointDice; i++) {
			
			Debug.Log("CURRENT POSITION : " + m_currentPos);

			// Check if walk over win floor 

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

			// Move to next position

			iTween.MoveTo(gameObject, nextPos, m_moveSpeed);

			// Delay for wait player move
		//	yield return new WaitForSeconds(0.1f);	

			// Move camera follow player
			yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (nextPos));

			// Delay for wait camera
			yield return new WaitForSeconds(0.5f);	
		}
		
		yield break;
	}

	// Go any posion 
	public IEnumerator GoAnyPos(int pos, List<Vector3> path){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos);

		Vector3 goPos;
		int upDown;

		// Check that posiotion before or after current position
		if (pos < m_currentPos)
			upDown = -1;
		else
			upDown = 1;

		for (int i = m_currentPos + upDown; i != pos + upDown; i+=upDown) {

			Debug.Log("Pos : " + i);

			goPos = path[i];
			goPos.z = -2;

			// Move to next position

			iTween.MoveTo (gameObject, goPos, m_moveSpeed);

			// Move camera follow player

			StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (goPos));

			yield return new WaitForSeconds(0.1f);
		}

		m_currentPos = pos ;
		
		yield return new WaitForSeconds (0.5f);
		
		yield break;
	}

	// Get current position

	public int GetCurrentPos(){
		return m_currentPos;
	}
}
