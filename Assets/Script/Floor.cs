using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	void Start(){
	}

	// Use this for initialization
	void OnMouseDown(){
		// Get bool for check to trap 
		bool doingTrap = CardControl.Getsingleton().GetDoingTrap ();
		bool canTrap = CardControl.Getsingleton().CheckEvent (int.Parse (gameObject.name));

		Debug.Log ("Doing Trap : " + doingTrap);

		// Check for trap
		if (!doingTrap && canTrap) {

			Debug.Log("Floor" + gameObject.name);

			CardControl.Getsingleton().SetTrap (int.Parse (gameObject.name));
			CardControl.Getsingleton().SetfinishTrap (true);
		}
	}
}
