using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

	//public GameObject m_buttonRoll;

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
		if (m_startRoll) {
			StartCoroutine (Rolling ());
		}
	}

	public void StartRoll(){
		m_startRoll = true;
		m_stopRoll = false;
	}

	public void StopRoll(){
		m_stopRoll = true;
	}

	private IEnumerator Rolling(){
		m_startRoll = false;
		m_speedRoll = 0.01f;
		while (!m_stopRoll) {
			Debug.Log ("Roll : " + m_pointDice);
			m_pointDice ++;
			if(m_pointDice > 5){
				m_pointDice = 0;
			}

			m_spriteRend.sprite = m_diceSprite[m_pointDice];
			yield return new WaitForSeconds(m_speedRoll);
			m_speedRoll += 0.05f;
		}

		Debug.Log ("GET POINT DONE : " + m_pointDice);
		yield break;
	}

	public int GetPointDice(){
		return m_pointDice;
	}
}
