using UnityEngine;
using System.Collections;

public class TimeBombCard : CardProp {

	private GameObject m_iconBomb ;
	public EventClass m_BombPref;
	public GameObject m_Bomb;
	
	public override IEnumerator DoCardEvent()
	{
		m_iconBomb = GameObject.FindWithTag("BombIcon");
		
		Debug.Log ("CARD CONTROL(Bomb) : " + m_cardControl);
		
		FloorProperties floorTrap;
		
		// Set for can Trap on path
		m_cardControl.SetIsDoingTrap (true);
		
		// Set Choose position to trap text
		Vector3 pos = Camera.main.transform.position;
		pos.y += 2;
		
		// Show text Choose
		m_textMesh.GetComponent<MeshRenderer> ().enabled = true;
		
		// Wait player trap on path
		while (!m_cardControl.IsFinishTrap())
			yield return null;
		
		m_textMesh.GetComponent<MeshRenderer> ().enabled = false;
		
		// Set for can't trap on path
		m_cardControl.SetIsDoingTrap (false);
		
		// Create event object
		
		floorTrap = m_cardControl.GetFloorTrap ();

		EventClass bombObj;
		GameObject tempObj;

		tempObj = (GameObject)Instantiate (m_BombPref.gameObject, floorTrap.transform.position, Quaternion.identity);
//		
		bombObj = tempObj.GetComponent<EventClass> ();

		floorTrap.SetEvent (bombObj);
//		
//		Debug.Log ("RESET OBJ : " + restartObj);
		Debug.Log ("FLOOR TRAP : " + floorTrap);
		
		// Set trap to floor's position
		Vector3 posTrap = floorTrap.gameObject.transform.position;
		posTrap.z = -3;
		m_iconBomb.transform.position = posTrap;
		m_iconBomb.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		
		// move camera to postion that set trap
		yield return StartCoroutine(	m_mainCameraMove.SetPosition(floorTrap.gameObject.transform.position));
		
		// Set can drag camera when animation running
		m_dragCamera.SetIsDrag(false);
		
		// Run animation that show trap set
		m_iconBomb.GetComponent<Animator>().SetTrigger("Appear");
		
		float delay;
		
		// Delay wait animation finish
		delay = m_iconBomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).length + 1f;
		yield return new WaitForSeconds(delay );
		
		// Set item restart position for hide it
		posTrap.z = -20;
		m_iconBomb.transform.position = posTrap;
		
		m_cardControl.SetIsFinishTrap (false);
		
		m_dragCamera.SetIsDrag(false);
		
		yield break;
	}
}
