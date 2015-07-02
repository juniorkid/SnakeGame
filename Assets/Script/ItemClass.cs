using UnityEngine;
using System.Collections;

public class ItemClass : MonoBehaviour {
	public GameObject m_card;
	public SpriteRenderer m_spriteRend;

	public void SetCardItem(GameObject card){
		m_card = card;
		m_spriteRend.sprite = card.GetComponent<SpriteRenderer> ().sprite;
	}

	void OnMouseDown(){
		Debug.Log ("CLICK ITEM");
	}
}
