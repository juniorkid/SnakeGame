using UnityEngine;
using System.Collections;

public class FloorProperties : MonoBehaviour {

	private float m_delay = 0.1f;
	private bool m_isTrap;
	private EventClass m_eventObj ;

	private string m_eventType;

	private CardControl m_cardControl;

	void Start(){
		m_cardControl = CardControl.Getsingleton ();
		m_eventObj = null;
		m_eventType = "Normal";
	}
	
	void OnMouseDown(){
		m_delay = 0.1f;
		m_isTrap = true;
	}

	void OnMouseUp(){
		if (m_isTrap) {
			bool isDoingTrap = m_cardControl.IsDoingTrap ();
			
			Debug.Log ("Doing Trap : " + isDoingTrap);
			
			// Check for trap
			if (isDoingTrap && m_eventType == "Normal") {
				
				Debug.Log("Floor" + gameObject.name);
				
				m_cardControl.SetFloorTrap (this);
				m_cardControl.SetIsFinishTrap (true);
			}
		}
	}

	void OnMouseDrag(){
		m_delay -= Time.deltaTime;
		if (m_delay < 0)
			m_isTrap = false;
	}

	public void SetEvent(EventClass eventObj){
		m_eventObj = eventObj;
//		Debug.Log (m_eventObj);
	}

	public EventClass GetEvent(){
		return m_eventObj;
	}

	public string GetEventType(){
		return m_eventType;
	}

	public void SetEventType(string eventType){
		m_eventType = eventType;
	}
}
