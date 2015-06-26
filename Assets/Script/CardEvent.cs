using UnityEngine;
using System.Collections;

public class CardEvent : EventClass {

	public GameObject m_cardControl;

	void Start(){
		m_cardControl = GameObject.FindWithTag ("Deck");
	}

	public override IEnumerator DoEvent(Player player){
		yield return StartCoroutine (m_cardControl.GetComponent<CardControl> ().ControlCardFlip ());
		yield return StartCoroutine (m_cardControl.GetComponent<CardControl> ().CallEventCard ());
	}
}
