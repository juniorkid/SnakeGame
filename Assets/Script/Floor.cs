using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	private GameObject m_deck;

	void Start(){
		m_deck = GameObject.FindWithTag ("Deck");
	}

	// Use this for initialization
	void OnMouseDown(){
		bool doingTrap = CardControl.Getsingleton().GetDoingTrap ();
		bool canTrap = CardControl.Getsingleton().CheckEvent (int.Parse (gameObject.name));

		Debug.Log ("Doing Trap : " + doingTrap);

		if (!doingTrap && canTrap) {

			Debug.Log("Floor" + gameObject.name);

			CardControl.Getsingleton().SetTrap (int.Parse (gameObject.name));
			CardControl.Getsingleton().SetfinishTrap (true);
		}
	}
}
