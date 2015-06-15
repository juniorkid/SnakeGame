using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

	private Vector3 m_resetCamera;
	private Vector3 m_origin;
	private Vector3 m_diference;
	private bool m_drag=false;
	private bool m_canDrag = false;

	private Vector3 m_lastPos;

	void Start () {
		m_resetCamera = Camera.main.transform.position;
	}
	void LateUpdate () {
		Vector3 posDrag;

		if (m_canDrag) {
			if (Input.GetMouseButton (0)) {
				m_diference = (Camera.main.ScreenToWorldPoint (Input.mousePosition)) - Camera.main.transform.position;
				if (m_drag == false) {
					m_drag = true;
					m_origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				}
			} else {
				m_drag = false;
			}
			if (m_drag == true) {
				posDrag = m_origin - m_diference;
				if(posDrag.x < -19f)
					posDrag.x = -17.33f;
				else if(posDrag.x > 0)
					posDrag.x = 0;
				if(posDrag.y < -2.4f)
					posDrag.y = -2.4f;
				else if(posDrag.y > m_lastPos.y)
					posDrag.y = m_lastPos.y;

				Camera.main.transform.position = posDrag;
			}
			//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
			if (Input.GetMouseButton (1)) {
				Camera.main.transform.position = m_resetCamera;
			}
		}
	}

	public void SetDrag(bool canDrag){
		m_canDrag = canDrag;

	}

	public void SetLastPos(Vector3 pos){
		m_lastPos = pos;
	}
}
