using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardControl : MonoBehaviour {

	// Use to check for start flip card

	private bool m_finishFlip;
	private bool m_canFlip;

	// Use to check for start trap

	private bool m_finishTrap;
	private bool m_doingTrap;

	public Camera m_mainCamera;

	private bool[] m_eventCard;

	private int[] m_StopUpDownEvent;

	public GameObject m_prefabRestart;
	private GameObject m_itemRestart;

	// Type Card

	private int m_card;

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
		m_finishFlip = false;
		m_finishTrap = false;
		m_canFlip = false;
		m_doingTrap = true;

		// Create item Restart
		m_itemRestart = Instantiate(m_prefabRestart, new Vector3 ( 0, 0, -20), Quaternion.identity) as GameObject;

		// Hide Card
		ShowHideCard(false);
	//	transform.position = new Vector3(0,0,-20);
	}

	// Use to show card for player choose
	public IEnumerator ControlCard(){

		// Show Card
		ShowHideCard (true);
	
		// Set for card can flap 
		m_canFlip = true;

		// Wait Flip card 
		while (!m_finishFlip)
			yield return null;

		// Hide Card
		ShowHideCard(false);
	
		yield break;

	}

	public IEnumerator CardEvent(int maxNode, List<Vector3> path){
	
		// Check if card is restart item
		if (m_card == 1) {

			// Set for can Trap on path
			m_doingTrap = false;

			// Wait player trap on path
			while (!m_finishTrap)
				yield return null;

			// Set for can't trap on path
			m_doingTrap = true;

			// Set value to tell where have event restart
			m_StopUpDownEvent [m_posTrap] = maxNode;

			// Set postion to show item on screen

			Vector3 posItem = path[m_posTrap];
			posItem.z = -3;
			m_itemRestart.transform.position = posItem;
			m_itemRestart.transform.localScale = new Vector3(1.3f, 1.3f, 1);

			// move camera to postion that set trap
			StartCoroutine(	m_mainCamera.GetComponent<MainCameraMove>().SetPosition(path[m_posTrap]));

			float delay;

			// Delay for wait move camera finish

			delay = 1f;

			yield return new WaitForSeconds(delay);

			// Set can drag camera when animation running
			m_mainCamera.GetComponent<DragCamera>().SetDrag(false);

			// Run animation that show trap set
			m_itemRestart.GetComponent<Animator>().SetTrigger("Appear");


			// Delay wait animation finish
			delay = m_itemRestart.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).length;
			yield return new WaitForSeconds(delay + 2f);

			// Set item restart position for hide it
			posItem.z = -20;
			m_itemRestart.transform.position = posItem;

			m_mainCamera.GetComponent<DragCamera>().SetDrag(false);

			yield break;
		}
	}

	// Show hide all card
	private void ShowHideCard(bool setCard){
		int length;

		length = m_allCard.Length;

		for (int i = 1; i < length; i++)
			m_allCard [i].gameObject.SetActive (setCard);
	}


	// Get all event from game control
	public void SetAllEvent(int[] StopUpDownEvent, bool[] eventCard){
		m_StopUpDownEvent = StopUpDownEvent;
		m_eventCard = eventCard;
	}

	// Set finish  flip
	public void SetfinishFlip(bool finishFlip){
		m_finishFlip = finishFlip;
	}

	// Set finish trap
	public void SetfinishTrap(bool finishTrap){
		m_finishTrap = finishTrap;
	}

	// Get finish flip
	public bool Getfinish(){
		return m_finishFlip;
	}

	// Get value for tell can flip
	public bool GetCanFlip(){
		Debug.Log ("DOING IN CONTROL + " + m_canFlip);

		return m_canFlip;
	}

	// Set value for tell can flip
	public void SetCanFlip(bool canFlip){
		m_canFlip = canFlip;
	}

	// Get value for tell trap is doing
	public bool GetDoingTrap(){
		return m_doingTrap;
	}

	// Set position for trap
	public void SetTrap(int pos){
		m_posTrap = pos;
	}

	// Get type of card
	public void Card(int card){
		m_card = card;
	}

	// Return type of card
	public int GetCard(){
		return m_card;
	}

	// Check that postion don't have event
	public bool CheckEvent(int pos){
		if (m_StopUpDownEvent [pos] == 0 && m_eventCard [pos] == false )
			return true;
		else {
			return false;
		}
	}
}
