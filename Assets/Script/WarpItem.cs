using UnityEngine;
using System.Collections;

public class WarpItem : Item {

	public override IEnumerator ItemAbility ()
	{
		Player currPlayer;
		currPlayer = m_gameController.GetCurrentPlayer ();

		m_gameController.m_eventID = EventStateID.Warp;

		int currPos = currPlayer.GetCurrentPos ();

		int randomPos = (int)Random.Range (currPos+1, currPlayer.m_path.Count - 1.1f);

		currPlayer.SetCurrentPos (randomPos);
		currPlayer.MoveToCenter (); 
		
		m_gameController.m_eventID = EventStateID.NoEvent;
		
		yield return StartCoroutine(m_gameController.m_mainCameraMove.SetPosition(currPlayer.transform.position));
		
		yield break;
	}
}
