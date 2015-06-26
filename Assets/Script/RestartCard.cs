using UnityEngine;
using System.Collections;

public class RestartCard : CardProp {

	public GameObject m_itemRestart;
	public EventClass m_restartObj;

	public override IEnumerator DoCardEvent()
	{
		Debug.Log ("CARD CONTROL(Restart) : " + m_cardControl);
		
		FloorProperties floorTrap;

		// Set for can Trap on path
		m_cardControl.SetIsDoingTrap (true);
		
		// Set Choose position to trap text
		Vector3 pos = Camera.main.transform.position;
		pos.y += 2;
		
		// Show text Choose
		m_textMesh.gameObject.SetActive(true);
		
		// Wait player trap on path
		while (!m_cardControl.IsFinishTrap())
			yield return null;
		
		m_textMesh.gameObject.SetActive(false);
		
		// Set for can't trap on path
		m_cardControl.SetIsDoingTrap (false);
		
		// Set postion to show item on screen

		floorTrap = m_cardControl.GetFloorTrap ();

		floorTrap.SetEvent (m_restartObj);

		Vector3 posTrap = floorTrap.gameObject.transform.position;
		posTrap.z = -3;
		m_itemRestart.transform.position = posTrap;
		m_itemRestart.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		
		// move camera to postion that set trap
		yield return StartCoroutine(	m_mainCameraMove.SetPosition(floorTrap.gameObject.transform.position));
		
		// Set can drag camera when animation running
		m_dragCamera.SetIsDrag(false);
		
		// Run animation that show trap set
		m_itemRestart.GetComponent<Animator>().SetTrigger("Appear");
		
		float delay;
		
		// Delay wait animation finish
		delay = m_itemRestart.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).length + 1f;
		yield return new WaitForSeconds(delay );
		
		// Set item restart position for hide it
		posTrap.z = -20;
		m_itemRestart.transform.position = posTrap;
		
		m_dragCamera.SetIsDrag(false);
		
		yield break;
	}
}
