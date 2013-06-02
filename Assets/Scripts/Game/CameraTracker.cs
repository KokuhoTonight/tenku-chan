using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {
	
	public GameObject target;
	public float lerpStep = 10f;

	// Update is called once per frame
	void Update () {
		LerpTarget();
	}
	
	void LerpTarget()
	{
		MoveTarget(Vector3.Lerp(this.gameObject.transform.position, target.transform.position, lerpStep));
	}
	
	void MoveTarget(Vector3 to)
	{
		this.gameObject.transform.position = new Vector3(to.x, to.y, this.gameObject.transform.position.z);
	}
	
}
