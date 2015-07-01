using UnityEngine;
using System.Collections;

public class UFOEvent : EventClass {

	public override IEnumerator DoEvent(Player player){
		yield return	StartCoroutine (player.runUFO (1.0f));

		Debug.Log ("END UFO EVENT");

		// Delete object when event has done
		Destroy (gameObject);
		yield break;
	}
}
