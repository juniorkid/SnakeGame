using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardControl : EventClass {

	// Use to check for start flip card

	private bool m_isFinishFlip;
	private bool m_isCanFlip;

	// Use to check for start trap

	private bool m_isFinishTrap;
	private bool m_isDoingTrap;

	public Camera m_mainCamera;

	public GameObject m_prefabRestart;

	FloorProperties m_floorTrap;
	
	public TextMesh m_textMesh;
	private GameObject m_textObj;

	private DragCamera m_dragCamera;
//	private MainCameraMove m_mainCamearaMove;

	// Type Card

	private CardProp m_cardObj;

	// Position of trap

	private int m_posTrap;

	private Transform[] m_allCard;

	private static CardControl m_singleton;
	public static CardControl Getsingleton(){
		return m_singleton;
	}

	void Awake()
	{
		m_singleton = this;
	}

	// Use this for initialization
	void Start () {

		// Set main camera move and Drag cameara
		m_dragCamera = m_mainCamera.GetComponent<DragCamera> ();
	//	m_mainCamearaMove = m_mainCamera.GetComponent<MainCameraMove> ();

		Debug.Log ("Drag : " + m_dragCamera);

		// Get all child object (card)
		m_allCard = gameObject.GetComponentsInChildren <Transform>();

		// Set default value
		m_isFinishFlip = false;
		m_isFinishTrap = false;
		m_isCanFlip = false;
		m_isDoingTrap = false;

		// Create item Restart
	//	Instantiate(m_prefabRestart, new Vector3 ( 0, 0, -20), Quaternion.identity);

		// Hide Card
		ShowHideCard(false);

		// Hide Text 
		m_textMesh.GetComponent<MeshRenderer> ().enabled = false;

	}

	void Update(){
		m_dragCamera = m_mainCamera.GetComponent<DragCamera> ();
	}

	// Use to show card for player choose
	public IEnumerator ControlCardFlip(){

		Debug.Log ("DRAG IN FUNCTION : " + m_dragCamera);

		// Set Can't drag when wait flip
		m_dragCamera.SetIsDrag(false);

		// Show Card
		ShowHideCard (true);
	
		// Set for card can flap 
		m_isCanFlip = true;

		// Wait Flip card 
		while (!m_isFinishFlip)
			yield return null;

		// Hide Card
		ShowHideCard(false);

		// Set can drag
		m_dragCamera.SetIsDrag(true);
	
		// Set default value isfinishFlip
		m_isFinishFlip = false;

		yield break;

	}

	public IEnumerator CallEventCard(){

		// Check if card is restart item
		yield return StartCoroutine( m_cardObj.DoCardEvent ());
		Destroy (m_cardObj.gameObject);
	}

	// Show hide all card

	private void ShowHideCard(bool isSetCard){
		int length;

		length = m_allCard.Length;

		for (int i = 1; i < length; i++)
			m_allCard [i].gameObject.SetActive (isSetCard);
	}


	// Get all event from game control
	/*public void SetAllEvent(int[] StopUpDownEvent, bool[] isEventCard){
		m_StopUpDownEvent = StopUpDownEvent;
		m_isEventCard = isEventCard;
	}*/

	// Set finish  flip
	public void SetIsFinishFlip(bool isFinishFlip){
		m_isFinishFlip = isFinishFlip;
	}

	// Set finish trap
	public void SetIsFinishTrap(bool isFinishTrap){
		m_isFinishTrap = isFinishTrap;
	}

	// Set Doing trap
	public void SetIsDoingTrap(bool isDoingTrap){
		m_isDoingTrap = isDoingTrap;
	}

	// Get finish flip
	public bool IsFinishFlip(){
		return m_isFinishFlip;
	}

	// Get finish trap
	public bool IsFinishTrap(){
		return m_isFinishTrap;
	}

	//Getfinish Get value for tell can flip
	public bool IsCanFlip(){
		Debug.Log ("DOING IN CONTROL + " + m_isCanFlip);

		return m_isCanFlip;
	}

	// Set value for tell can flip
	public void SetIsCanFlip(bool isCanFlip){
		m_isCanFlip = isCanFlip;
	}

	// Get value for tell trap is doing
	public bool IsDoingTrap(){
		return m_isDoingTrap;
	}

	// Set position for trap
	public void SetFloorTrap(FloorProperties floorTrap){
		m_floorTrap = floorTrap;
	}

	public FloorProperties GetFloorTrap(){
		return m_floorTrap;
	}

	// Get type of card
	public void SetCard(CardProp cardNum){
		Debug.Log ("CARD NUM : " + cardNum);

		GameObject temp;

		temp = Instantiate( cardNum.gameObject, new Vector3( 0, 0, 0), Quaternion.identity) as GameObject;

		m_cardObj = temp.GetComponent<CardProp> ();

		Debug.Log ("CARD OBJ : " + temp);

	}
	
}
