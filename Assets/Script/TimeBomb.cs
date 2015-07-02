using UnityEngine;
using System.Collections;

public class TimeBomb : MonoBehaviour {

	public Sprite[] m_timeBombSprite;
	
	public SpriteRenderer m_bombSpriteRend;

	public Player m_player;

	public int m_timeBomb;

	void Update(){
		int timeBomb = m_player.GetTimeBomb ();
		if (timeBomb > 0) {
			m_bombSpriteRend.sprite = m_timeBombSprite[timeBomb-1];
		} else if (timeBomb == 0) {
			m_player.SetIsBomb(false);
			m_player.SetEventStop(3);
			SetBombActive(false);
		}
	}

	public void SetBombActive(bool active){
		m_bombSpriteRend.enabled = active;
	}
}
