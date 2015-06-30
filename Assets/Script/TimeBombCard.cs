using UnityEngine;
using System.Collections;

public class TimeBombCard : CardProp {

	private GameObject m_iconBomb ;
	public EventClass m_BombPref;
	
	public override IEnumerator DoCardEvent()
	{
		yield return StartCoroutine (Trap("BombIcon", m_BombPref));
		yield break;
	}
}
