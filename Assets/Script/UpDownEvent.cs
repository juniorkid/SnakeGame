using UnityEngine;
using System.Collections;

public class UpDownEvent : EventClass {

	private int m_PosUpDown;

	public override IEnumerator DoEvent(Player player = null){
		yield return StartCoroutine( player.GoAnyPos (m_PosUpDown));	
		yield break;
	}

	public void SetPosUpDown(int posUpDown){
		m_PosUpDown = posUpDown;
	}
}
