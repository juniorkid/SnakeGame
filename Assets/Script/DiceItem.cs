using UnityEngine;
using System.Collections;

public class DiceItem : Item {

	public Dice m_dice;
	public Roll m_roll;

	public int m_point;

	public override IEnumerator ItemAbility ()
	{
		m_dice = m_gameController.m_dice;
		m_roll = m_gameController.m_buttonRoll;
		m_dice.m_isSetPoint = true;
		m_dice.m_pointDice = m_point;
		m_roll.SetClick (true);
	
		yield break;
	}
}
