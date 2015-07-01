using UnityEngine;
using System.Collections;

public class UFOCard : CardProp {

	public EventClass m_UFOPref;
	
	public override IEnumerator DoCardEvent()
	{
		yield return StartCoroutine (Trap("UFOIcon", m_UFOPref));
		yield break;
	}
}
	