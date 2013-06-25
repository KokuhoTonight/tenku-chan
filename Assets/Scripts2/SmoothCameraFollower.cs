using UnityEngine;
using System.Collections;

public class SmoothCameraFollower : MonoBehaviour {
	
	public GameObject target;
	public float speed;
	// Update is called once per frame
	void Update () {
		if(target)
		{
			float targetHeight = target.transform.localPosition.y;
			Vector3 currentPos = this.gameObject.transform.localPosition;
			this.gameObject.transform.localPosition = Vector3.Lerp(currentPos, new Vector3(currentPos.x, targetHeight, currentPos.z), Time.deltaTime * speed); 
		}
	}
}
