using UnityEngine;
using System.Collections;

public class StopEvent : EventClass {

	public int m_stopTurn;

	public override IEnumerator DoEvent(Player player){

		// Set player stop turn
		player.SetEventStop (m_stopTurn);		
		yield break;
	}
}
