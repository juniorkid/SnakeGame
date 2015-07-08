using UnityEngine;
using System.Collections;

public class FloorProperties : MonoBehaviour {

	private float m_delay = 0.1f;
	private bool m_isClick;
	public bool m_isCanDestroy;

	private EventClass m_eventObj ;

	private CardControl m_cardControl;

	private EventStateID m_eventID;

	private bool m_isDoingTrap;

	private Gamecontroller m_gameController;

	void Start(){
		m_cardControl = CardControl.Getsingleton ();
		m_eventObj = null;
		m_isCanDestroy = false;
		m_gameController = Gamecontroller.Getsingleton ();
	}
	
	void OnMouseDown(){
		m_delay = 0.1f;
		m_isClick = true;
	}

	void OnMouseUp(){
		m_isDoingTrap = m_cardControl.IsDoingTrap ();
		m_eventID = m_gameController.m_eventID;

		if (m_isClick) {
			if (m_isDoingTrap)
				TrapFloor ();
			else if (m_eventID == EventStateID.LoosenTrap) {
				StartCoroutine( LoosenTrap ());
			}
		}
	}

	void OnMouseDrag(){
		m_delay -= Time.deltaTime;
		if (m_delay < 0)
			m_isClick = false;
	}

	public IEnumerator LoosenTrap(){
		m_gameController.m_eventID = EventStateID.NoEvent;
		yield return StartCoroutine( m_gameController.m_mainCameraMove.SetPosition(transform.position));
		if (m_eventObj != null) {
			yield return StartCoroutine( m_eventObj.ShowTrap(m_eventObj.m_iconName));
			Destroy(m_eventObj.gameObject);
		}
	}

	public void SetEvent(EventClass eventObj){
		m_eventObj = eventObj;
	}

	public EventClass GetEvent(){
		return m_eventObj;
	}
	
	public void TrapFloor(){
		bool isTrapDone;

		Debug.Log ("OBJ : " + m_eventObj);

		// Check for trap
		if (m_eventObj == null) {
			isTrapDone = true;
		} else if (m_isCanDestroy) {
			Destroy (m_eventObj.gameObject);
			isTrapDone = true;
		} else
			isTrapDone = false;

		if (isTrapDone) {
			Debug.Log ("Floor" + gameObject.name);

			m_cardControl.SetFloorTrap (this);
			m_cardControl.SetIsFinishTrap (true);

			m_isCanDestroy = true;

			isTrapDone = false;
		}
	}
}
