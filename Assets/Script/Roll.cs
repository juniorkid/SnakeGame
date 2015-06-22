using UnityEngine;
using System.Collections;

public class Roll : MonoBehaviour {

	private bool m_hasClick;
	
	// Use this for initialization
	void Start(){
		m_hasClick = false;
	}

	void OnMouseDown() {
		if (!m_hasClick) {
	//		Debug.Log ("Rolled");
			m_hasClick = true;
		}
	}

	// Set player can click roll
	public void SetClickDefualt(){
		m_hasClick = false;
	}
	
	public bool GetClick(){
		return m_hasClick;
	}
}
