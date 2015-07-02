using UnityEngine;
using System.Collections;

public class UFOControl : MonoBehaviour {

	public Animator m_UFOamimator;
	public Animator m_playerAnimator;

	public Player m_player;

	private bool m_isUFO = false;

	public IEnumerator SetAnimationUFO(){

		m_isUFO = m_UFOamimator.GetBool ("IsGo");

		m_isUFO = !m_isUFO;

		m_UFOamimator.SetBool ("IsGo", m_isUFO);

		yield return new WaitForSeconds (0.01f);

		float delay;
		delay = m_UFOamimator.GetCurrentAnimatorStateInfo (0).length;

		Debug.Log ("DELAY : " + delay);

		yield return new WaitForSeconds(delay);

		yield break;
	}

	public void runPlayer(){
		bool appear = m_playerAnimator.GetBool ("Appear");
	
		appear = !appear;
		m_playerAnimator.SetBool ("Appear", appear);
	}

	void Update(){
		if (!m_player.IsUFO() && m_isUFO) {
			StartCoroutine(SetAnimationUFO());
		}
	}
}
