using UnityEngine;
using System.Collections;

public class StopEvent : EventClass {

	public int m_stopTurn;

	public override IEnumerator DoEvent(Player player){
		player.SetEventStop (m_stopTurn);		
		yield break;
	}
}
