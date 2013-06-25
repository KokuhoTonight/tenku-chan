using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public bool followRotation = false;
	public bool smoothRotation = true;
	public bool followBehind = true;
	public float rotationDamping = 10.0f;
	//public bool startFollow = false;
	
	void LateUpdate () {
	//	if(startFollow){
			Vector3 wantedPosition;
			if(followBehind)
				wantedPosition = target.TransformPoint(0, height, -distance);
			else
				wantedPosition = target.TransformPoint(0, height, distance);
				transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
		
			if (smoothRotation && followRotation){
				Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
				transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
			}
			else if(followRotation)
				transform.LookAt (target, target.up);
		//}
	}
}