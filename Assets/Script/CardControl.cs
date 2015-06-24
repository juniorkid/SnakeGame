using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardControl : MonoBehaviour {

	// Use to check for start flip card

	private bool m_isFinishFlip;
	private bool m_isCanFlip;

	// Use to check for start trap

	private bool m_isFinishTrap;
	private bool m_isDoingTrap;

	public Camera m_mainCamera;

	private bool[] m_isEventCard;

	private int[] m_StopUpDownEvent;

	public GameObject m_prefabRestart;
	private GameObject m_itemRestart;
	
	public TextMesh m_textMesh;
	private GameObject m_textObj;

	// Type Card

	private int m_cardNum;

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

		// Get all child object (card)
		m_allCard = gameObject.GetComponentsInChildren <Transform>();

		// Set default value
		m_isFinishFlip = false;
		m_isFinishTrap = false;
		m_isCanFlip = false;
		m_isDoingTrap = true;

		// Create item Restart
		m_itemRestart = Instantiate(m_prefabRestart, new Vector3 ( 0, 0, -20), Quaternion.identity) as GameObject;

		// Hide Card
		ShowHideCard(false);

		// Hide Text 
		m_textMesh.gameObject.SetActive(false);

	}

	// Use to show card for player choose
	public IEnumerator ControlCard(){

		// Set Can't drag when wait flip
		m_mainCamera.GetComponent<DragCamera>().SetIsDrag(false);

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
		m_mainCamera.GetComponent<DragCamera>().SetIsDrag(true);
	
		yield break;

	}

	public IEnumerator CardEvent(int maxNode, List<Vector3> path){

		// Check if card is restart item
		if (m_cardNum == 1) {

			// Set for can Trap on path
			m_isDoingTrap = false;

			// Set Choose position to trap text
			Vector3 pos = Camera.main.transform.position;
			pos.y += 2;

			// Show text Choose
			m_textMesh.gameObject.SetActive(true);

			// Wait player trap on path
			while (!m_isFinishTrap)
				yield return null;

			m_textMesh.gameObject.SetActive(false);

			// Set for can't trap on path
			m_isDoingTrap = true;

			// Set value to tell where have event restart
			m_StopUpDownEvent [m_posTrap] = maxNode;

			// Set postion to show item on screen

			Vector3 posItem = path[m_posTrap];
			posItem.z = -3;
			m_itemRestart.transform.position = posItem;
			m_itemRestart.transform.localScale = new Vector3(1.3f, 1.3f, 1);

			// move camera to postion that set trap
			yield return StartCoroutine(	m_mainCamera.GetComponent<MainCameraMove>().SetPosition(path[m_posTrap]));

			// Set can drag camera when animation running
			m_mainCamera.GetComponent<DragCamera>().SetIsDrag(false);

			// Run animation that show trap set
			m_itemRestart.GetComponent<Animator>().SetTrigger("Appear");

			float delay;

			// Delay wait animation finish
			delay = m_itemRestart.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).length + 1f;
			yield return new WaitForSeconds(delay );

			// Set item restart position for hide it
			posItem.z = -20;
			m_itemRestart.transform.position = posItem;

			m_mainCamera.GetComponent<DragCamera>().SetIsDrag(false);

			yield break;
		}
	}

	// Show hide all card
	private void ShowHideCard(bool isSetCard){
		int length;

		length = m_allCard.Length;

		for (int i = 1; i < length; i++)
			m_allCard [i].gameObject.SetActive (isSetCard);
	}


	// Get all event from game control
	public void SetAllEvent(int[] StopUpDownEvent, bool[] isEventCard){
		m_StopUpDownEvent = StopUpDownEvent;
		m_isEventCard = isEventCard;
	}

	// Set finish  flip
	public void SetfinishFlip(bool isFinishFlip){
		m_isFinishFlip = isFinishFlip;
	}

	// Set finish trap
	public void SetfinishTrap(bool isFinishTrap){
		m_isFinishTrap = isFinishTrap;
	}

	// Get finish flip
	public bool Getfinish(){
		return m_isFinishFlip;
	}

	// Get value for tell can flip
	public bool GetCanFlip(){
		Debug.Log ("DOING IN CONTROL + " + m_isCanFlip);

		return m_isCanFlip;
	}

	// Set value for tell can flip
	public void SetCanFlip(bool isCanFlip){
		m_isCanFlip = isCanFlip;
	}

	// Get value for tell trap is doing
	public bool GetDoingTrap(){
		return m_isDoingTrap;
	}

	// Set position for trap
	public void SetTrap(int pos){
		m_posTrap = pos;
	}

	// Get type of card
	public void SetCard(int cardNum){
		m_cardNum = cardNum;
	}

	// Return type of card
	public int GetCard(){
		return m_cardNum;
	}

	// Check that postion don't have event
	public bool CheckEvent(int pos){
		if (m_StopUpDownEvent [pos] == 0 && m_isEventCard [pos] == false )
			return true;
		else {
			return false;
		}
	}
}
