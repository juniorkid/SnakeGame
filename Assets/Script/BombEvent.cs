using UnityEngine;
using System.Collections;

public class BombEvent : EventClass {

	public override IEnumerator DoEvent(Player player){
		yield return StartCoroutine (ShowTrap ("BombIcon"));

		player.SetBombActive (true);
		player.SetTimeBomb ();
		Destroy (gameObject);
		yield break;
	}
}
