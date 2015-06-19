﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardControl : MonoBehaviour {

	private bool m_finishFlip;
	private bool m_finishTrap;
	private bool m_doingFlip;
	private bool m_doingTrap;

	public Camera m_mainCamera;

	private bool[] m_eventCard;

	private int m_maxNode;

	private int[] m_StopUpDownEvent;

	public GameObject m_prefabRestart;
	private GameObject m_itemRestart;

	private int m_card;

	private int m_posTrap;

	private 
	// Use this for initialization
	void Start () {
		m_finishFlip = false;
		m_finishTrap = false;
		m_doingFlip = false;
		m_doingTrap = false;

		m_itemRestart = Instantiate(m_prefabRestart, new Vector3 ( 0, 0, -20), Quaternion.identity) as GameObject;

		transform.position = new Vector3(0,0,-20);
	}

	public IEnumerator ControlCard(){

		Vector3 defaulPos = new Vector3(0,0,-20f);
		Vector3 showPos = Camera.main.transform.position;

		showPos.x += 2.05f;
		showPos.y += 0.23f;
		showPos.z = -4;

		transform.position = showPos;

		m_doingFlip = false;

		while (!m_finishFlip)
			yield return null;

		transform.position = defaulPos;

		yield break;

	}

	public IEnumerator CardEvent(int[] StopUpDownEvent, bool[] eventCard, int maxNode, List<Vector3> path){

		Debug.Log ("CARD (CONTROL)" + m_card);

		if (m_card == 1) {

			m_StopUpDownEvent = StopUpDownEvent;
			m_eventCard = eventCard;
			m_maxNode = maxNode;

			m_doingTrap = false;

			while (!m_finishTrap)
				yield return null;

			m_doingTrap = true;

			m_StopUpDownEvent [m_posTrap] = maxNode;

			Vector3 posItem = path[m_posTrap];

			posItem.z = -3;

			m_itemRestart.transform.position = posItem;

			m_itemRestart.transform.localScale = new Vector3(1.3f, 1.3f, 1);

			StartCoroutine(	m_mainCamera.GetComponent<MainCameraMove>().SetPosition(path[m_posTrap]));

			yield return new WaitForSeconds(1f);

			m_itemRestart.GetComponent<Animator>().SetTrigger("Appear");

			float delay;

			delay = m_itemRestart.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).length;

			// Delay wait item disappear

			yield return new WaitForSeconds(delay + 2f);

			posItem.z = -20;

			m_itemRestart.transform.position = posItem;

			yield break;
		}
	}
	
	public void SetfinishFlip(bool finishFlip){
		m_finishFlip = finishFlip;
	}

	public void SetfinishTrap(bool finishTrap){
		m_finishTrap = finishTrap;
	}

	public bool Getfinish(){
		return m_finishFlip;
	}

	public bool GetDoingFlip(){
		return m_doingFlip;
	}

	public void SetDoingFlip(bool doingFlip){
		m_doingFlip = doingFlip;
	}

	public bool GetDoingTrap(){
		return m_doingTrap;
	}

	public void SetTrap(int pos){
		m_posTrap = pos;
	}

	public void Card(int card){
		m_card = card;
	}

	public int GetCard(){
		return m_card;
	}

	public bool CheckEvent(int pos){
		if (m_StopUpDownEvent [pos] == 0 && m_eventCard [pos] == false)
			return true;
		else {
			return false;
		}
	}
}
