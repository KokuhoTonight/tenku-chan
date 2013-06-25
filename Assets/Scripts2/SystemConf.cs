using UnityEngine;
using System.Collections;

public class SystemConf : MonoBehaviour {
	
	public int targetFrameRate = 30;
	// Use this for initialization
	void Start () {
		SetFrameRate();
	}
	
	void SetFrameRate(){
		Application.targetFrameRate = targetFrameRate;
	}
}
