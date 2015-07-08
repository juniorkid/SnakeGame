using UnityEngine;
using System.Collections;

public class DoubleDrawItem : Item {

	void Awake(){
		 m_isDoubleDraw = true;
	}

	public override IEnumerator ItemAbility ()
	{
		m_gameController.GetCurrentPlayer().m_drawTime = 2;
		
		yield break;
	}
}
