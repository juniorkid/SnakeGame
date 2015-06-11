using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private int m_currentPos;

	// Use this for initialization
	public IEnumerator GoNextPos(int pointDice, Vector3[] path){
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
			
			iTween.MoveTo(gameObject, nextPos, moveSpeed);
			yield return new WaitForSeconds(1f);
			
		}
		
		yield break;
	}

	public IEnumerator DoUpDownEvent(int posEvent, Vector3 goPos){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos);
		
		iTween.MoveTo (gameObject, goPos, 5);
		m_currentPos = posEvent ;
		
		yield return new WaitForSeconds (2f);
		
		yield break;
	}

	public int GetCurrentPos(){
		return m_currentPos;
	}
}
