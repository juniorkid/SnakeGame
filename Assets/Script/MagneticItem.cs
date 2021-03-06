﻿using UnityEngine;
using System.Collections;

public class MagneticItem : Item {

	public override IEnumerator ItemAbility ()
	{
		Player player = null;
		Player currPlayer;
		currPlayer = m_gameController.GetCurrentPlayer ();
		
		m_gameController.m_eventID = EventStateID.Magnetic;
		
		while (player == null) {
			player = m_gameController.m_playerClick;
			if(player != null && player.m_playerPop.GetId() == currPlayer.m_playerPop.GetId())
				player = null;
			yield return null;
		}
		
		player.SetCurrentPos (currPlayer.GetCurrentPos ());
		player.MoveToCheckPoint ();

		m_gameController.m_playerClick = null;
		
		m_gameController.m_eventID = EventStateID.NoEvent;
		
		yield return StartCoroutine(m_gameController.m_mainCameraMove.SetPosition(currPlayer.transform.position));
		
		yield break;
	}

}
