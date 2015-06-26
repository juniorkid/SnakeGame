using UnityEngine;
using System.Collections;

public class CardProp : MonoBehaviour {

	public CardControl m_cardControl;
	public TextMesh m_textMesh;
	public FloorProperties m_floorProp;
	public MainCameraMove m_mainCameraMove;
	public DragCamera m_dragCamera;

	void Start(){
		m_cardControl = CardControl.Getsingleton ();
		m_textMesh = GameObject.FindWithTag ("TextChoose").GetComponent<TextMesh>();
		Debug.Log ("CARD CONTROL : " + m_cardControl);
	}

	public virtual IEnumerator DoCardEvent()
	{
		yield return 0;
	}
}
