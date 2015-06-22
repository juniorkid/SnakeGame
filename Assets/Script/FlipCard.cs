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

		// Get number of card sprite
		m_numCard = m_spriteFront.Length;

		// Get its animator component
		m_animator = gameObject.GetComponent<Animator> ();
		// Get its sprite component
		m_spriteRend = gameObject.GetComponent<SpriteRenderer> ();

		// Set Back card sprite
		m_spriteBack = m_spriteRend.sprite;

		// Set State for flip card
		m_isFlip = false;
	}

	// Use this for initialization
	void OnMouseDown () {

//		Debug.Log ("CLICK CARD");
		// Get boolean to check for flip
		bool canFlip = CardControl.Getsingleton().GetCanFlip (); //m_deck.GetComponent<CardControl> ().GetDoingFlip ();

		// Check for flip 
		if(!m_isFlip && canFlip)
			StartCoroutine (Flip ());
	}

	// Flip Card
	private IEnumerator Flip(){
		float delay;
		int random;
		// Set for other card can't flip when one card flip
		m_isFlip = true;

		// Random front card
		random = (int)Random.Range (0f, m_numCard - 0.1f);

		// Set other card can't flip
		CardControl.Getsingleton().SetCanFlip (false);

		// Set type event card
		m_eventCard = random;

		// Set other card can't flip
		m_isFlip = true;

		m_animator.SetTrigger ("flipCard");

		// Set delay for make animation smooth
		yield return new WaitForSeconds(0.002f);

		// Wait animation done
		delay = m_animator.GetCurrentAnimatorStateInfo (0).length;
		yield return new WaitForSeconds(delay);

		// Change front sprite follow event card
		m_spriteRend.sprite = m_spriteFront[1];

		Debug.Log ("NEW SPRITE");

		// Wait animation done
		delay = m_animator.GetCurrentAnimatorStateInfo (0).length;
		yield return new WaitForSeconds(delay);

		// Delay Show Card
		delay = 2f;
		yield return new WaitForSeconds(delay);

		// Set boolean to tell finish flip
		CardControl.Getsingleton().SetfinishFlip (true);

		// Set event card number
		CardControl.Getsingleton().Card (1);
	
		// Change card to back
		m_spriteRend.sprite = m_spriteBack;
		m_isFlip = false;

		yield break;

	}

	// Use to get event Card

	public int GetEventCard(){
		return m_eventCard;
	}
}
