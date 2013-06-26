using UnityEngine;
using System.Collections;

public class SceneReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width/2 + 100, Screen.height/2 - 350, 100, 40), "Reset"))
		{
			Time.timeScale = 1f;
			Application.LoadLevel(0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
