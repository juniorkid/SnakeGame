using UnityEngine;
using System.Collections;

public class ItemClass : EventClass {
	public ItemClass m_card;
	public SpriteRenderer m_spriteRend;

	public bool m_isUseItem = false;

	public void SetCardItem(ItemClass card){
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
		if (!m_isUseItem) {
			m_isUseItem = true;
			StartCoroutine (UseItem ());
		}
	}

	public virtual IEnumerator ItemAbility(){
		yield break;
	}

	public IEnumerator UseItem(){
		yield return StartCoroutine (m_card.ItemAbility ());
		m_spriteRend.sprite = null;
		Destroy (m_card.gameObject);
		m_isUseItem = false;
	}
}
