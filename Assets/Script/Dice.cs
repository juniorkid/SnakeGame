using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

	private float m_timeRandom;

	private float m_speedRoll;

	public bool m_startRoll;
	public bool m_stopRoll;

	public Sprite[] m_diceSprite;

	public int m_pointDice;

	private SpriteRenderer m_spriteRend;

	void Start(){
		m_startRoll = false;
		m_speedRoll = 0.01f;
		m_stopRoll = true;
		m_spriteRend = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		// Check to Start roll dice
		if (m_startRoll) {
			StartCoroutine (Rolling ());
		}
	}

	// Use to set state for roll
	public void StartRoll(){
		m_startRoll = true;
		m_stopRoll = false;
	}

	// Use to set state for stop roll
	public void StopRoll(){
		m_stopRoll = true;
	}

	// Rolling dice
	private IEnumerator Rolling(){
		m_startRoll = false;
		m_speedRoll = 0.01f;

		// Roll dice until m_stopRoll equal true
		while (!m_stopRoll) {
	//		Debug.Log ("Roll : " + m_pointDice);
			m_pointDice ++;
			if(m_pointDice > 5){
				m_pointDice = 0;
			}

			// Change sprite follow point dice
			m_spriteRend.sprite = m_diceSprite[m_pointDice];
			yield return new WaitForSeconds(m_speedRoll);

			// Decrease speed random roll
			m_speedRoll += 0.04f;
		}

	//	Debug.Log ("GET POINT DONE : " + m_pointDice);
		yield break;
	}

	// Get point dice
	public int GetPointDice(){
		return m_pointDice;
	}
}
