using UnityEngine;
using System.Collections;

public class TimeBombCard : CardProp {

	public EventClass m_BombPref;
	
	public override IEnumerator DoCardEvent(Player player)
	{
		yield return StartCoroutine (Trap("BombIcon", m_BombPref));
		yield break;
	}
}
