using UnityEngine;
using System.Collections;

public class StealItem : Item {

	public override IEnumerator ItemAbility ()
	{

		float chanceSteal = Random.Range (0, 1);
	
		if (chanceSteal >= 0) {

			Debug.Log("START STEAL");

			int playerID;
			int currID = m_gameController.m_currID;

			m_gameController.m_buttonRoll.gameObject.SetActive(false);

			Debug.Log("Player Curr ID : " + currID);

			do{
				playerID = (int)Random.Range(0,m_gameController.m_numPlayer-0.1f);
			}while(playerID == currID);

			Debug.Log("PLAYER ID : " + playerID);	

			int slot = (int)Random.Range(0,4);

			Debug.Log("SLOT : " + slot);

			Player playerTarget = m_gameController.m_player[playerID];

			Item itemTarget = playerTarget.m_item[0];

			if(itemTarget != null){

				GameObject tempItem = (GameObject)Instantiate(itemTarget.gameObject, new Vector3(0, 0, -30), Quaternion.identity);

				Item item = tempItem.GetComponent<Item>();

				m_gameController.m_player[currID].GetItem(item);
				
				Destroy(itemTarget.gameObject);

			}

		}

		yield break;
	}
}
