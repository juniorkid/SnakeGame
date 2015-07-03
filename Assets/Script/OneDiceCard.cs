using UnityEngine;
using System.Collections;

public class OneDiceCard : CardProp {

	public ItemClass m_onePrefab;
	
	public override IEnumerator DoCardEvent(Player player)
	{
		KeepItem(player, m_onePrefab);
		yield break;
	}
}
