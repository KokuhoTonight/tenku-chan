using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	[SerializeField]
	GameObject trackTarget;
	[SerializeField]
	Camera tracker;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		tracker.transform.position = new Vector3( tracker.transform.position.x, trackTarget.transform.position.y + 30, tracker.transform.position.z);
	}
}
