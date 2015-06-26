using UnityEngine;
using System.Collections;

public class ResetEvent : EventClass {

	public override IEnumerator DoEvent(Player player){
		yield return StartCoroutine( player.GoAnyPos (0));		
		yield break;
	}
}
