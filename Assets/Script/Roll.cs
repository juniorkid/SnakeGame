using UnityEngine;
using System.Collections;

public class Roll : MonoBehaviour {

	private bool m_isClick;
	
	// Use this for initialization
	void Start(){
		m_isClick = false;
	}

	void OnMouseDown() {
		if (!m_isClick) {
	//		Debug.Log ("Rolled");
			m_isClick = true;
		}
	}

	// Set player can click roll
	public void SetClick(bool isClick){
		m_isClick = isClick;
	}
	
	public bool GetClick(){
		return m_isClick;
	}
}
