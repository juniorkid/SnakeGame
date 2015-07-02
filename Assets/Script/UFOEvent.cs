using UnityEngine;
using System.Collections;

public class UFOEvent : EventClass {

	public UFOControl m_ufoControl;

	public override IEnumerator DoEvent(Player player){
		m_ufoControl = player.GetUFO ();
		player.SetEventStop (1);
		player.SetIsUFO (true);

		yield return	StartCoroutine (m_ufoControl.SetAnimationUFO ());

		Debug.Log ("END UFO EVENT");

		// Delete object when event has done
		Destroy (gameObject);
		yield break;
	}
}
