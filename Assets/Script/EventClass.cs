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
}
