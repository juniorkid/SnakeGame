using UnityEngine;
using System.Collections;

public class FloorProperties : MonoBehaviour {

	private float m_delay = 0.1f;
	private bool m_isTrap;
	public bool m_isCanDestroy;

	private EventClass m_eventObj ;

	private string m_eventType;

	private CardControl m_cardControl;

	void Start(){
		m_cardControl = CardControl.Getsingleton ();
		m_eventObj = null;
		m_eventType = "Normal";
		m_isCanDestroy = false;
	}
	
	void OnMouseDown(){
		m_delay = 0.1f;
		m_isTrap = true;
	}

	void OnMouseUp(){

		bool isTrapDone;

		if (m_isTrap) {
			bool isDoingTrap = m_cardControl.IsDoingTrap ();
			
			Debug.Log ("Doing Trap : " + isDoingTrap);
			Debug.Log ("OBJ : " + m_eventObj);
			// Check for trap
			if(isDoingTrap && m_eventObj == null){
				isTrapDone = true;
			}
			else if (isDoingTrap && m_isCanDestroy) {
				Destroy(m_eventObj.gameObject);
				isTrapDone = true;
			}
			else
				isTrapDone = false;

			if(isTrapDone){
				Debug.Log("Floor" + gameObject.name);
				
				m_cardControl.SetFloorTrap (this);
				m_cardControl.SetIsFinishTrap (true);

				m_isCanDestroy = true;

				isTrapDone = false;
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
