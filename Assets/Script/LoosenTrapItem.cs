using UnityEngine;
using System.Collections;

public class LoosenTrapItem : Item {

	public override IEnumerator ItemAbility ()
	{
		m_gameController.m_eventID = EventStateID.LoosenTrap;

		yield break;
	}
}
