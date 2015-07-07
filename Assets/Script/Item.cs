using UnityEngine;
using System.Collections;

public class Item : EventClass {
	public Item m_card;
	public SpriteRenderer m_spriteRend;

	public bool m_isUseItem = false;

	public Gamecontroller m_gameController;

	public bool m_isArmor = false;

	public bool m_isDoubleDraw = false;

	void Start(){
		m_spriteRend = gameObject.GetComponent<SpriteRenderer> ();
		m_gameController = Gamecontroller.Getsingleton ();
	}

	public void SetCardItem(Item card){
		Sprite sprite;
		m_card = card;
		if(card != null)
			sprite = card.GetComponent<SpriteRenderer> ().sprite;
		else {
			sprite = null;
		}
		m_spriteRend.sprite = sprite;
	}

	void OnMouseDown(){
		Debug.Log ("CLICK ITEM : " + m_card);
		if (!m_isUseItem && m_card != null && ((m_gameController.m_stateID == GameStateID.Rolling && !m_card.m_isDoubleDraw)  ||
		    (m_card.m_isDoubleDraw && m_gameController.m_stateID == GameStateID.DoingEvent)) ) {

			m_gameController.m_buttonRoll.gameObject.SetActive(false);

			StartCoroutine (UseItem ());
		}
	}

	public virtual IEnumerator ItemAbility(){
		yield break;
	}

	// Use item ability	
	public IEnumerator UseItem(){
		Debug.Log ("USE ITEM");

		m_spriteRend.sprite = null;

		yield return new WaitForSeconds (0.1f);

		yield return StartCoroutine (m_card.ItemAbility ());
		Destroy (m_card.gameObject);

		yield return new WaitForSeconds (0.1f);

		m_gameController.m_player [m_gameController.m_currID].ShowItem ();

		// Wait item
		yield return new WaitForSeconds (2f);



		if(!m_card.m_isDoubleDraw) {
			m_gameController.ChangePlayerTurn ();
			m_gameController.m_stateID = GameStateID.WaitRoll;
		}

		m_isUseItem = false;
	}	
}
