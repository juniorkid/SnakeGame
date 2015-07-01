using UnityEngine;
using System.Collections;

public class UFOControl : MonoBehaviour {

	public Animator m_UFOamimator;
	public Animator m_playerAnimator;

	private float m_speed;

	public IEnumerator SetAnimationUFO(float speed){
		m_speed = speed;

		m_UFOamimator.SetFloat ("Speed", m_speed);

		yield return new WaitForSeconds (0.01f);

		float delay;
		delay = m_UFOamimator.GetCurrentAnimatorStateInfo (0).length;

		Debug.Log ("DELAY : " + delay);

		yield return new WaitForSeconds(delay);

		yield break;
	}

	public void runPlayer(){
		bool appear = m_playerAnimator.GetBool ("Appear");

		if (m_speed == 1 || m_speed == -1) {
			appear = !appear;
			m_playerAnimator.SetBool ("Appear", appear);
			m_speed = 0;
		}
	}
}
