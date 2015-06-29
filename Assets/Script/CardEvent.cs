using UnityEngine;
using System.Collections;

public class CardEvent : EventClass {

	public GameObject m_cardControl;

	void Start(){
		m_cardControl = GameObject.FindWithTag ("Deck");
	}

	public override IEnumerator DoEvent(Player player){

		// Start function show card and flip card
		yield return StartCoroutine (m_cardControl.GetComponent<CardControl> ().ControlCardFlip ());

		// Start function call event function
		yield return StartCoroutine (m_cardControl.GetComponent<CardControl> ().CallEventCard ());
	}
}
