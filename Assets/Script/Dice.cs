using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

	public GameObject m_buttonRoll;

	private float m_timeRandom;

	private float m_speedRoll;

	public bool m_stopRoll;
	public bool m_getPoint;

	public Sprite[] m_diceSprite;

	public int m_pointDice;

	private SpriteRenderer m_spriteRend;

	void Start(){
		m_getPoint = false;
		m_speedRoll = 0.01f;
		m_stopRoll = true;
		m_spriteRend = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (m_buttonRoll.GetComponent<Roll> ().m_hasRoll && m_stopRoll) {
			StartCoroutine (StartRoll ());
		}
	}

	private IEnumerator StartRoll(){

		m_stopRoll = false;
		m_speedRoll = 0.01f;

		Debug.Log ("START ROLL");
		StartCoroutine (GetPoint ());
		m_timeRandom = Random.Range(4F, 6F);
		yield return new WaitForSeconds (m_timeRandom);

		m_stopRoll = true;
		m_buttonRoll.GetComponent<Roll> ().m_hasRoll = false;
		Debug.Log ("Stop ROLL : " + m_stopRoll);
		yield break;

	}

	private IEnumerator GetPoint(){

		while (!m_stopRoll) {
			m_pointDice ++;
			if(m_pointDice > 5){
				m_pointDice = 0;
			}
			 //Debug.Log(m_pointDice);

			m_spriteRend.sprite = m_diceSprite[m_pointDice];
			yield return new WaitForSeconds(m_speedRoll);
			m_speedRoll += 0.05f;
		}

		m_getPoint = true;
		Debug.Log ("GET POINT DONE");
		yield break;
	}
}
