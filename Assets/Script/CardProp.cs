using UnityEngine;
using System.Collections;

// Mother class for each card
public class CardProp : MonoBehaviour {

	public CardControl m_cardControl;
	public TextMesh m_textMesh;
	//public FloorProperties m_floorProp;
	public MainCameraMove m_mainCameraMove;
	public DragCamera m_dragCamera;

	void Start(){
		// Set defualt value
		m_cardControl = CardControl.Getsingleton ();
		m_textMesh = GameObject.FindWithTag ("TextChoose").GetComponent<TextMesh>();
		m_mainCameraMove = Camera.main.GetComponent<MainCameraMove>();
		m_dragCamera = Camera.main.GetComponent<DragCamera>();
		Debug.Log ("CARD CONTROL : " + m_cardControl);
	}

	public virtual IEnumerator DoCardEvent()
	{
		yield return 0;
	}
}
