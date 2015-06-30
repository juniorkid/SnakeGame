using UnityEngine;
using System.Collections;

public class UFOCard : EventClass {

	public override IEnumerator DoEvent(Player player){
		Debug.Log ("UFO");
		yield break;
	}
}
