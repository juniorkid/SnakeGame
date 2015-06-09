using UnityEngine;
using System.Collections;

public class Roll : MonoBehaviour {

	public bool m_hasRoll;
	
	// Use this for initialization
	void Start(){
		m_hasRoll = false;
	}

	void OnMouseDown() {
		if (!m_hasRoll) {
	//		Debug.Log ("Rolled");
			m_hasRoll = true;
		}
	}

	public void SetRoll(bool hasRoll){
		m_hasRoll = hasRoll;
	}
}
