using UnityEngine;
using System.Collections;

public class BombEvent : EventClass {

	public override IEnumerator DoEvent(Player player){
		GameObject bomb = GameObject.FindWithTag("BombIcon");
		Vector3 pos;
		Vector3 defaultPos;
		defaultPos = bomb.transform.position;
		pos = player.transform.position;
		pos.z = -3;

		bomb.transform.position = pos;

		yield return new WaitForSeconds (0.5f);

		bomb.transform.position = defaultPos;
		player.SetBombActive (true);
		player.SetTimeBomb ();
		Destroy (gameObject);
		yield break;
	}
}
