using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {
	public GameObject target;
	public float lerpStep = 10f;

	// Update is called once per frame
	void Update () {
		//LerpTarget();
		
		var cameraScreenPos = camera.WorldToScreenPoint( target.transform.position);
		var screenHeight = camera.GetScreenHeight();
		if( screenHeight * 0.7 < cameraScreenPos.y || screenHeight * 0.3 < cameraScreenPos.y){
			LerpTarget();
		}
	}
	
	public void LerpTarget()
	{
		fromPos = transform.position;
		toPos = target.transform.position.SetX( fromPos.x).SetZ( fromPos.z);
		StartCoroutine("Track", 0.5f);
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
	
	public void Move(Vector3 to)
	{
		this.gameObject.transform.position = new Vector3(to.x, to.y, this.gameObject.transform.position.z);
	}
	
}
