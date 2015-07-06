using UnityEngine;
using System.Collections;

public class TimeBombEvent : EventClass {

	public TimeBomb m_bomb;

	void Start(){
		m_iconName = "BombIcon";
	}

	public override IEnumerator DoEvent(Player player){

		Debug.Log ("PLAYERRR : " + player);

		if (!player.GetArmor ()) {
			yield return StartCoroutine (ShowTrap (m_iconName));

			m_bomb = player.GetBomb ();

			m_bomb.SetBombActive (true);
			player.StartTimeBomb (3);
			player.SetIsBomb (true);
		} else
			player.SetArmor (false);
		Destroy (gameObject);
		yield break;
	}
}
