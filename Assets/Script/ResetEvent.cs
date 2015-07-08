using UnityEngine;
using System.Collections;

public class ResetEvent : EventClass {

	void Start(){
		m_iconName = "RestartIcon";
	}

	public override IEnumerator DoEvent(Player player){

		if (!IsArmor (player)) {
			yield return StartCoroutine (ShowTrap (m_iconName));

			// Move player to start position
			yield return StartCoroutine (player.GoAnyPos (0));	
		} else
			player.SetArmor (false);

		// Delete object when event has done
		Destroy (gameObject);
		yield break;
	}
}
