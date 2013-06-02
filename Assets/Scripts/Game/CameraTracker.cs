using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {
	public GameObject target;
	public float lerpStep = 10f;

	// Update is called once per frame
	void Update () {
		//LerpTarget();
	}
	
	public void LerpTarget()
	{
		fromPos = transform.position;
		toPos = target.transform.position.SetX( fromPos.x).SetZ( fromPos.z);
		StartCoroutine("Track", 0.7f);
		//MoveTarget(Vector3.Lerp(this.gameObject.transform.position.SetX(0), target.transform.position.SetX(0), lerpStep));
	}
	
	Vector3 fromPos;
	Vector3 toPos;
	
	IEnumerator Track (float time){
		transform.position = fromPos;
		
		float t = 0;
		while( transform.position != toPos){
			transform.position = Vector3.Lerp( fromPos, toPos, t);
			t += Time.deltaTime/time;
			yield return null;
		}
	}
	
	void MoveTarget(Vector3 to)
	{
		this.gameObject.transform.position = new Vector3(to.x, to.y, this.gameObject.transform.position.z);
	}
	
}
