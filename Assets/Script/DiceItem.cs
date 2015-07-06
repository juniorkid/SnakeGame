using UnityEngine;
using System.Collections;

public class DiceItem : Item {

	public Dice m_dice;
	public Roll m_roll;

	public int m_point;

	public override IEnumerator ItemAbility ()
	{
		m_dice = GameObject.FindWithTag ("Dice").GetComponent<Dice>();
		m_roll = GameObject.FindWithTag ("Roll").GetComponent<Roll>();
		m_dice.m_isSetPoint = true;
		m_dice.m_pointDice = m_point;
		m_roll.SetClick (true);
	
		yield break;
	}
}
