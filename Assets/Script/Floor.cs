using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	private GameObject m_deck;

	void Start(){
		m_deck = GameObject.FindWithTag ("Deck");
	}

	// Use this for initialization
	void OnMouseDown(){
		bool doingTrap = m_deck.GetComponent<CardControl> ().GetDoingTrap ();
		bool canTrap = m_deck.GetComponent<CardControl> ().CheckEvent (int.Parse (gameObject.name));

		if (!doingTrap && canTrap) {

			Debug.Log("Floor" + gameObject.name);

			m_deck.GetComponent<CardControl> ().SetTrap (int.Parse (gameObject.name));
			m_deck.GetComponent<CardControl> ().SetfinishTrap (true);
		}
	}
}
