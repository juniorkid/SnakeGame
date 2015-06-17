using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour {

	public int m_button;

	// Use this for initialization
	void OnMouseDown() {
		PlayerPrefs.SetInt ("numPlayer", m_button);
		Application.LoadLevel("playgame");
	}
}
