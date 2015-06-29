using UnityEngine;
using System.Collections;

public class ResetEvent : EventClass {

	public override IEnumerator DoEvent(Player player){
		GameObject restart = GameObject.FindWithTag("RestartItem");
		Vector3 lastPos;
		Vector3 pos;

		// Set default position
		lastPos = restart.transform.position;

		// Set position to show icon trap
		pos = transform.position;
		pos.z = -3;
		restart.transform.position = pos;

		// Delay to show icon trap
		yield return new WaitForSeconds (0.4f);

		// Set item to defualt position
		restart.transform.position = lastPos;

		// Move player to start position
		yield return StartCoroutine( player.GoAnyPos (0));	

		// Delete object when event has done
		Destroy (gameObject);
		yield break;
	}
}
