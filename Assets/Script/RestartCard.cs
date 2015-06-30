using UnityEngine;
using System.Collections;

public class RestartCard : CardProp {

	private GameObject m_iconRestart ;
	public EventClass m_restartPref;

	public override IEnumerator DoCardEvent()
	{
		yield return StartCoroutine(Trap ("RestartIcon", m_restartPref));
		
		yield break;
	}
}
