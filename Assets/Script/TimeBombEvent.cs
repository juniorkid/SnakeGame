using UnityEngine;
using System.Collections;

public class TimeBombEvent : EventClass {

	public TimeBomb m_bomb;

	public override IEnumerator DoEvent(Player player){
		yield return StartCoroutine (ShowTrap ("BombIcon"));

		m_bomb = player.GetBomb ();

		m_bomb.SetBombActive (true);
		player.StartTimeBomb (3);
		player.SetIsBomb (true);
		Destroy (gameObject);
		yield break;
	}
}
