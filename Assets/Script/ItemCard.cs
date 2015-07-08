using UnityEngine;
using System.Collections;

public class ItemCard : CardProp {

	public Item m_prefab;

	public bool m_isNoItem;
	
	public override IEnumerator DoCardEvent(Player player)
	{
		if(!m_isNoItem)
			KeepItem(player, m_prefab);
		yield break;
	}
}
