using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	private float m_delay = 0.1f;
	private bool m_isTrap;


	private CardControl m_cardControl;

	void Start(){
		m_cardControl = CardControl.Getsingleton ();
	}
	
	void OnMouseDown(){
		m_delay = 0.1f;
		m_isTrap = true;
	}

	void OnMouseUp(){
		if (m_isTrap) {
			bool isDoingTrap = m_cardControl.GetDoingTrap ();
			bool isCanTrap = m_cardControl.CheckEvent (int.Parse (gameObject.name));
			
			Debug.Log ("Doing Trap : " + isDoingTrap);
			
			// Check for trap
			if (!isDoingTrap && isCanTrap) {
				
				Debug.Log("Floor" + gameObject.name);
				
				m_cardControl.SetTrap (int.Parse (gameObject.name));
				m_cardControl.SetfinishTrap (true);
			}
		}
	}
	void OnMouseDrag(){
		m_delay -= Time.deltaTime;
		if (m_delay < 0)
			m_isTrap = false;
	}
}
