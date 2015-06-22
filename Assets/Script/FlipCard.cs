using UnityEngine;
using System.Collections;

public class FlipCard : MonoBehaviour {
	
	private Animator m_animator;
	private bool m_isFlip;
	public Sprite[] m_spriteFront;
	private SpriteRenderer m_spriteRend;
	private int m_numCard;
	private int m_eventCard;
	private Sprite m_spriteBack;


	void Start(){

		m_numCard = m_spriteFront.Length;
		//m_deck = GameObject.FindWithTag ("Deck");
		m_animator = gameObject.GetComponent<Animator> ();
		m_spriteRend = gameObject.GetComponent<SpriteRenderer> ();

		m_spriteBack = m_spriteRend.sprite;

		m_isFlip = false;
	}

	// Use this for initialization
	void OnMouseDown () {
		Debug.Log ("CLICK CARD");
		bool doingFlip = CardControl.Getsingleton().GetDoingFlip (); //m_deck.GetComponent<CardControl> ().GetDoingFlip ();

		if(!m_isFlip && !doingFlip)
			StartCoroutine (Flip ());
	}

	private IEnumerator Flip(){
		float delay;
		int random;

		m_isFlip = true;

		random = (int)Random.Range (0f, m_numCard - 0.1f);

		CardControl.Getsingleton().SetDoingFlip (true);

		m_eventCard = random;

		m_isFlip = true;
		//m_animator.SetTrigger ("flipCard");
		delay = 0.1f;

		Debug.Log ("DELAY : " + delay);

		m_animator.SetTrigger ("flipCard");
		
		yield return new WaitForSeconds(0.002f);

		delay = m_animator.GetCurrentAnimatorStateInfo (0).length;

		yield return new WaitForSeconds(delay);

		m_spriteRend.sprite = m_spriteFront[1];

		Debug.Log ("NEW SPRITE");

		delay = m_animator.GetCurrentAnimatorStateInfo (0).length;

		yield return new WaitForSeconds(delay);

		// Delay Show Card

		yield return new WaitForSeconds(2f);

		CardControl.Getsingleton().SetfinishFlip (true);

		CardControl.Getsingleton().Card (1);

		m_spriteRend.sprite = m_spriteBack;

		m_isFlip = false;

		yield break;

	}

	public int GetEventCard(){
		return m_eventCard;
	}

	public void SetDrawCard(){
		m_isFlip = false;
	}

}
