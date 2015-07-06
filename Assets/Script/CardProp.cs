using UnityEngine;
using System.Collections;

// Mother class for each card
public class CardProp : MonoBehaviour {

	public CardControl m_cardControl;
	public TextMesh m_textMesh;
	//public FloorProperties m_floorProp;
	public MainCameraMove m_mainCameraMove;
	public DragCamera m_dragCamera;

	public GameObject m_iconTrap;

	void Start(){
		// Set defualt value
		m_cardControl = CardControl.Getsingleton ();
		m_textMesh = GameObject.FindWithTag ("TextChoose").GetComponent<TextMesh>();
		m_mainCameraMove = Camera.main.GetComponent<MainCameraMove>();
		m_dragCamera = Camera.main.GetComponent<DragCamera>();
		Debug.Log ("CARD CONTROL : " + m_cardControl);
	}

	public virtual IEnumerator DoCardEvent(Player player)
	{
		yield return 0;
	}

	public IEnumerator Trap(string nameIcon, EventClass prefab){
		m_iconTrap = GameObject.FindWithTag(nameIcon);
		
		Debug.Log ("CARD CONTROL(Restart) : " + m_cardControl);
		
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
		
		EventClass restartObj;
		GameObject tempObj;
		
		tempObj = (GameObject)Instantiate (prefab.gameObject, floorTrap.transform.position, Quaternion.identity);
		
		restartObj = tempObj.GetComponent<EventClass> ();
		
		floorTrap.SetEvent (restartObj);
		
		Debug.Log ("RESET OBJ : " + restartObj);
		Debug.Log ("FLOOR TRAP : " + floorTrap);
		
		// Set trap to floor's position
		Vector3 posTrap = floorTrap.gameObject.transform.position;
		posTrap.z = -3;
		m_iconTrap.transform.position = posTrap;
		m_iconTrap.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		
		// move camera to postion that set trap
		yield return StartCoroutine(	m_mainCameraMove.SetPosition(floorTrap.gameObject.transform.position));
		
		// Set can drag camera when animation running
		m_dragCamera.SetIsDrag(false);
		
		// Run animation that show trap set
		m_iconTrap.GetComponent<Animator>().SetTrigger("Appear");
		
		float delay;
		
		// Delay wait animation finish
		delay = m_iconTrap.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).length + 1f;
		yield return new WaitForSeconds(delay );
		
		// Set item restart position for hide it
		posTrap.z = -20;
		m_iconTrap.transform.position = posTrap;
		
		m_cardControl.SetIsFinishTrap (false);
		
		m_dragCamera.SetIsDrag(false);
	}

	public void KeepItem(Player player, Item itemPrefab){
		GameObject tempItem;
		tempItem = (GameObject)Instantiate (itemPrefab.gameObject, new Vector3 (0, 0, -20), Quaternion.identity);
		player.GetItem (tempItem.GetComponent<Item>());
	}	
}
