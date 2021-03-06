using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPop
{
	public string m_name;
	public int m_id;
	public List<int> m_itemIdList;
	public int m_gold;

	public void SetId(int id){
		m_id = id;
	}

	public int GetId(){
		return 	m_id;
	}
}

public class Player : MonoBehaviour {

	private int m_currentPos;
	public Camera m_mainCamera;
	float m_moveSpeed = 1f;

	public List<FloorProperties> m_path;

	private int m_stopTurn;

	private int m_timeBomb;

	public PlayerPop m_playerPop;

	public int m_id;

	private bool m_isUFO;

	private bool m_isBomb;

	public UFOControl m_ufoControl;

	public SpriteRenderer m_playerWindow;

	public Sprite m_playerWindowSprite;

	public TimeBomb m_bomb;

	public TextMesh m_namePlayer;

	public string m_name;

	public Item[] m_slotItem;

	public Item[] m_item;

	private bool m_isDoubleDice;

	private bool m_isArmor;

	public int m_drawTime;

	public Gamecontroller m_gameController ;

	void Start(){
		m_gameController = Gamecontroller.Getsingleton ();
		m_playerPop = new PlayerPop ();
		m_timeBomb = -1;
		m_isUFO = false;
		m_playerPop.SetId (m_id);
		m_playerPop.m_name = m_name;
		m_isDoubleDice = false;
	}

	void OnMouseDown(){
		if(m_gameController.m_eventID == EventStateID.Swap || m_gameController.m_eventID == EventStateID.Magnetic)
			m_gameController.m_playerClick = this;
	}

	// Use to go next position by pointDice
	public IEnumerator GoNextPos(int pointDice, int maxNode){
		
		bool backward = false;
		Vector3 nextPos;
		
		for (int i = 1; i <= pointDice; i++) {
			
			Debug.Log("CURRENT POSITION : " + m_currentPos);
			
			// Check if walk over win floor or walk under start

			if(m_currentPos >= maxNode)
				backward = true;
			else if(m_currentPos <= 0)
				backward = false;

			//Debug.Log("BACKWARD : " + backward);

			if(backward){
				m_currentPos --;
			}
			else{
				m_currentPos ++;
			}
			
			nextPos = m_path[m_currentPos].transform.position;
			
			//		Debug.Log (m_currentPos);
			//		Debug.Log ("Curr Pos : " + currPos);
			//		Debug.Log ("Next Pos : " + nextPos);
			
			nextPos.z = -2;
			
			// Move to next position
			
			iTween.MoveTo(gameObject, nextPos, m_moveSpeed);
			
			// Delay for wait player move
			//	yield return new WaitForSeconds(0.1f);	
			
			// Move camera follow player
			yield return StartCoroutine( m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (nextPos));
			
			// Delay for wait camera
			yield return new WaitForSeconds(0.5f);	
		}
		
		yield break;
	}

	public void ChangePlayerWindow(){
		m_playerWindow.sprite = m_playerWindowSprite;
		m_namePlayer.text = m_playerPop.m_name;
		ShowItem ();
	}

	public void SetActivePlayerWindow(bool active){
		m_playerWindow.gameObject.SetActive (active);
	}

	// Show item in list on Slot on item

	public void ShowItem(){
		for (int i = 0; i < 4; i++) {
			m_slotItem[i].SetCardItem(m_item[i]);
		}
	}

	// Put item in list
	public void GetItem(Item item){
		for (int i = 0; i < 4; i++) {
			if(m_item[i] == null){
				m_item[i] = item;
				break;
			}
		}
	}

	// Go any posion 
	public IEnumerator GoAnyPos(int pos){
		Debug.Log ("UP DOWN EVENT");
		Debug.Log ("Current Pos : " + m_currentPos);
		
		Vector3 goPos;
		int upDown;
		
		// Check that posiotion before or after current position
		if (pos < m_currentPos)
			upDown = -1;
		else
			upDown = 1;
		
		for (int i = m_currentPos + upDown; i != pos + upDown; i+=upDown) {
			
			Debug.Log("Pos : " + i);
			
			goPos = m_path[i].transform.position;
			goPos.z = -2;
			
			// Move to next position
			
			iTween.MoveTo (gameObject, goPos, m_moveSpeed);
			
			// Move camera follow player
			
			StartCoroutine (m_mainCamera.GetComponent<MainCameraMove> ().SetPosition (goPos));
			
			yield return new WaitForSeconds(0.1f);
		}
		
		m_currentPos = pos ;
		
		yield return new WaitForSeconds (0.5f);
		
		yield break;
	}

	public void SetArmor(bool isArmor){
		m_isArmor = isArmor;
	}

	public bool GetArmor(){
		Debug.Log (" RETURN ARMOR : " + m_isArmor);
		return m_isArmor;
	}

	// Get current position
	
	public int GetCurrentPos(){
		return m_currentPos;
	}

	public void SetCurrentPos(int pos){
		m_currentPos = pos;
	}

	public void SetStartPos(){
		m_currentPos = 0;
	}

	public void SetEventStop(int eventStop){
		m_stopTurn = eventStop;
	}

	public void SetIsBomb(bool isBomb){
		m_isBomb = isBomb;
	}

	public void StartTimeBomb(int timeBomb){
		m_timeBomb = timeBomb;
	}
	
	public bool IsBomb(){
		return m_isBomb;
	}

	public int GetTimeBomb(){
		return m_timeBomb;
	}
	
	public void DecreaseTimeBomb(){
		m_timeBomb --;
	}

	public int GetEventStop(){
		return m_stopTurn;
	}

	public bool IsDoubleDice(){
		return m_isDoubleDice;
	}

	public void SetIsDoubleDice(bool isDoubleDice){
		m_isDoubleDice = isDoubleDice;
	}

	public void DecreaseEventStop(){
		m_stopTurn --;
	}

	public void SetPath(List<FloorProperties> path){
		m_path = path;
	}
	
	public void MoveToCheckPoint(){
		Transform[] checkPoint;
		Vector3 pos;
		checkPoint = m_path [m_currentPos].GetComponentsInChildren <Transform>();
		pos = checkPoint[m_playerPop.GetId ()].position;
		pos.z = -2;
		gameObject.transform.position = pos;
	}

	public void MoveToCenter(){
		Transform[] checkPoint;
		Vector3 pos;
		checkPoint = m_path [m_currentPos].GetComponentsInChildren <Transform>();
		pos = checkPoint[0].position;
		pos.z = -2;
		gameObject.transform.position = pos;
	}

	public void SetIsUFO(bool isUFO){
		m_isUFO = isUFO;
	}

	// Check player in UFO EVENT
	public bool IsUFO(){
		return m_isUFO ;
	}

	// Get object UFO
	public UFOControl GetUFO(){
		return m_ufoControl;
	}

	// Get object bomb
	public TimeBomb GetBomb(){
		return m_bomb;
	}
}
