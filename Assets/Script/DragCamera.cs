using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

	private Vector3 m_resetCamera;
	private Vector3 m_origin;
	private Vector3 m_diference;
	private bool m_isDrag=false;
	private bool m_isCanDrag = false;

	private float m_rightX;
	private float m_leftX;
	private float m_startY;

	private Vector3 m_lastPos;


	void Start () {

		// Set minimum Y
		m_startY = -3.38f;
		m_resetCamera = Camera.main.transform.position;
	}
	void LateUpdate () {
		Vector3 posDrag;

		// Check can drag
		if (m_isCanDrag) {

			// Drag by click left
			if (Input.GetMouseButton (0)) {
				m_diference = (Camera.main.ScreenToWorldPoint (Input.mousePosition)) - Camera.main.transform.position;
				if (m_isDrag == false) {
					m_isDrag = true;
					m_origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				}
			} else {
				m_isDrag = false;
			}
			if (m_isDrag == true) {
				posDrag = m_origin - m_diference;

				// limit position drag
				if(posDrag.x < m_leftX)
					posDrag.x = m_leftX ;
				else if(posDrag.x > m_rightX)
					posDrag.x = m_rightX;
				if(posDrag.y < m_startY)
					posDrag.y = m_startY;
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

	// Set can drag
	public void SetIsDrag(bool isCanDrag){
		m_isCanDrag = isCanDrag;

	}

	// Set position to limit Y
	public void SetLastPos(Vector3 pos){
		m_lastPos = pos;
	}

	// Set to limit X
	public void SetX(float rX, float lX){
		m_rightX = rX;
		m_leftX = lX;
	}
}
