using UnityEngine;
using System.Collections;

public class DoubleDiceCard : CardProp {

	public Item m_prefab;

	public override IEnumerator DoCardEvent(Player player)
	{
		KeepItem(player, m_prefab);
		yield break;
	}
}
