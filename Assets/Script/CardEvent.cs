﻿using UnityEngine;
using System.Collections;

public class CardEvent : EventClass {

	public CardControl m_cardControl;

	void Start(){
		m_cardControl = CardControl.Getsingleton();
	}

	public override IEnumerator DoEvent(Player player){

		player.SetActivePlayerWindow (true);

		if(player.m_drawTime != 2)
			player.m_drawTime = 1;

		while(player.m_drawTime != 0){
			// Start function show card and flip card
			yield return StartCoroutine (m_cardControl.ControlCardFlip ());

			// Start function call event function
			yield return StartCoroutine (m_cardControl.CallEventCard ());
		}
	}
}
