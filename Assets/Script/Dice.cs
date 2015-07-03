using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dice : MonoBehaviour {

	private float m_timeRandom;

	private float m_speedRoll;

	public bool m_isStartRoll;
	public bool m_isStopRoll;

	public bool m_isSetPoint;

	public Sprite[] m_diceSprite;

	public int m_pointDice;

	private SpriteRenderer m_spriteRend;

	void Start(){
		m_isStartRoll = false;
		m_speedRoll = 0.01f;
		m_isStopRoll = true;
		m_spriteRend = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		// Check to Start roll dice
		if (m_isStartRoll) {
			StartCoroutine (Rolling ());
		}
	}

	// Use to set state for roll
	public void StartRoll(){
		m_isStartRoll = true;
		m_isStopRoll = false;
	}

	// Rolling dice
	private IEnumerator Rolling(){
		m_isStartRoll = false;
		m_speedRoll = 0.01f;
		
		if (!m_isSetPoint) {
			StartCoroutine (TimeStopDice ());

			// Roll dice until m_stopRoll equal true
			while (!m_isStopRoll) {
				//		Debug.Log ("Roll : " + m_pointDice);
				m_pointDice ++;
				if (m_pointDice > 5) {
					m_pointDice = 0;
				}

				// Change sprite follow point dice
				m_spriteRend.sprite = m_diceSprite [m_pointDice];
				yield return new WaitForSeconds (m_speedRoll);

				// Decrease speed random roll
				m_speedRoll += 0.04f;
			}
		} else {
			m_isStopRoll = true;
		}

	//	Debug.Log ("GET POINT DONE : " + m_pointDice);
		yield break;
	}
	
	public bool isStopRoll(){
		return m_isStopRoll;
	}

	// Get point dice
	public int GetPointDice(){
		return m_pointDice;
	}

	public void SetTimeRandom(float timeRandom){
		m_timeRandom = timeRandom;
	}

	public IEnumerator TimeStopDice(){
		yield return new WaitForSeconds(m_timeRandom);
		m_isStopRoll = true;
	}
}
