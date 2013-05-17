using UnityEngine;
using System.Collections;

public class ConnectSide : MonoBehaviour {
	[SerializeField]
	GameObject targetObject;
	
	[SerializeField]
	GameObject leftLimit;
	[SerializeField]
	GameObject rightLimit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( targetObject.transform.position.x < leftLimit.transform.position.x ){
			WarpToRight(targetObject);
		}
		if( rightLimit.transform.position.x < targetObject.transform.position.x ){
			WarpToLeft(targetObject);
		}
	}
	void WarpToRight(GameObject target){
		target.transform.position = new Vector3( rightLimit.transform.position.x, target.transform.position.y, target.transform.position.z);
	}
	void WarpToLeft(GameObject target){
		target.transform.position = new Vector3( leftLimit.transform.position.x, target.transform.position.y, target.transform.position.z);
	}
}
