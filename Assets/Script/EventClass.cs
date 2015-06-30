using UnityEngine;
using System.Collections;

public enum EVENT_TYPE
{
	xx,
}

// Mother class for event
public class EventClass : MonoBehaviour{


	// Use this for initialization
	public virtual IEnumerator DoEvent(Player player = null)
	{
		yield break;
	}

	public IEnumerator ShowTrap(string nameIcon)
	{
		GameObject iconTrap = GameObject.FindWithTag(nameIcon);
		Vector3 lastPos;
		Vector3 pos;
		
		// Set default position
		lastPos = iconTrap.transform.position;
		
		// Set position to show icon trap
		pos = transform.position;
		pos.z = -3;
		iconTrap.transform.position = pos;
		
		// Delay to show icon trap
		yield return new WaitForSeconds (1f);
		
		// Set item to defualt position
		iconTrap.transform.position = lastPos;

	}
}
