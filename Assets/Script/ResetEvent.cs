using UnityEngine;
using System.Collections;

public class ResetEvent : EventClass {

	public override IEnumerator DoEvent(Player player){
		yield return StartCoroutine (ShowTrap ("RestartIcon"));

		// Move player to start position
		yield return StartCoroutine( player.GoAnyPos (0));	

		// Delete object when event has done
		Destroy (gameObject);
		yield break;
	}
}
