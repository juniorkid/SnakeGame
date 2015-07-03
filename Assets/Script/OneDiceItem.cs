using UnityEngine;
using System.Collections;

public class OneDiceItem : ItemClass {

	public Dice m_dice;
	public Roll m_roll;

	public override IEnumerator ItemAbility ()
	{
		Debug.Log ("USE ITEM ONE DICE");
		m_dice = GameObject.FindWithTag ("Dice").GetComponent<Dice>();
		m_roll = GameObject.FindWithTag ("Roll").GetComponent<Roll>();
		m_dice.m_isSetPoint = true;
		m_dice.m_pointDice = 0;
		m_roll.SetClick (true);
	
		m_isUseItem = false;
		yield break;
	}
}
