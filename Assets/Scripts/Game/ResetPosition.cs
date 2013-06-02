using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {
	
	[SerializeField]
	GameObject target;
	[SerializeField]
	Vector3 initialPos;
	public Rect buttonRect;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(buttonRect, "Reset"))
			target.transform.position = initialPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
