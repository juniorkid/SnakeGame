using UnityEngine;
using System.Collections;

public class DoubleDrawItem : Item {

	void Awake(){
		 m_isDoubleDraw = true;
	}

	public override IEnumerator ItemAbility ()
	{
		CardControl m_cardControl = CardControl.Getsingleton ();
		m_cardControl.m_drawTime = 2;
		
		yield break;
	}
}
