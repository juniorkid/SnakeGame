using UnityEngine;
using System.Collections;

public class DoubleDiceItem : Item {

	public Player m_player;

	public override IEnumerator ItemAbility ()
	{
		m_player = m_gameController.GetCurrentPlayer ();
		m_player.SetIsDoubleDice (true);

		yield break;
	}
}
