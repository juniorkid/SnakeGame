using UnityEngine;
using System.Collections;

public class Item : EventClass {
	public Item m_card;
	public SpriteRenderer m_spriteRend;

	public bool m_isUseItem = false;

	public Gamecontroller m_gameController;

	public bool m_isArmor = false;

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
		if (!m_isUseItem && m_card != null && m_gameController.m_stateID == GameStateID.Rolling) {
			m_isUseItem = true;
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
		yield return StartCoroutine (m_card.ItemAbility ());
		Destroy (m_card.gameObject);
		m_isUseItem = false;
	}
}
