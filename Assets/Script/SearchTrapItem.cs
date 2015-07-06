using UnityEngine;
using System.Collections;

public class SearchTrapItem : Item {

	public override IEnumerator ItemAbility ()
	{
		// Search Trap
		foreach (FloorProperties floor in m_gameController.m_path) {
			if(floor.GetEvent() != null){
				EventClass trap = floor.GetEvent();
				Debug.Log ("TRAP : " + trap);
				Debug.Log ("TAG : " +  trap.m_iconName);
				if(trap.m_iconName != ""){
					Debug.Log ("SEARCH !! ");

					yield return StartCoroutine( m_gameController.m_mainCameraMove.SetPosition(floor.transform.position));
					yield return StartCoroutine( trap.ShowTrap(trap.m_iconName));
				
					m_gameController.m_buttonRoll.gameObject.SetActive(true);

					break;
				}
			}
		}
		yield break;
	}
}
	