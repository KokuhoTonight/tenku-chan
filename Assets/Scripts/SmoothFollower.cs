using UnityEngine;
using System.Collections;

public class SmoothFollower : MonoBehaviour {
	
	public GameObject target;
	public float speed;
	bool isFollow = true;
	
	// Update is called once per frame
	void Update () {
		//CheckPositionForOmit();
		if(isFollow && target){
			Vector3 myPos = this.gameObject.transform.localPosition;
			Vector3 targetPos = target.transform.localPosition;
			this.gameObject.transform.localPosition = Vector3.Lerp(myPos, new Vector3(targetPos.x, targetPos.y, myPos.z), Time.deltaTime * speed);
			this.gameObject.transform.rotation = Quaternion.Lerp (this.gameObject.transform.rotation, target.transform.rotation, Time.deltaTime * speed);
		}
	}
	
	void CheckPositionForOmit()
	{
		Vector3 myPos = this.gameObject.transform.localPosition;
		Vector3 targetPos = target.transform.localPosition;
		if(Mathf.Floor(myPos.x) == Mathf.Floor(targetPos.x) && Mathf.Floor(myPos.y) == Mathf.Floor(targetPos.y)){
			isFollow = false;;
		}
		else
			isFollow = true;
	}
	
	private void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Player" && !target)
		{
			this.target = collision.gameObject.GetComponent<PlayerController>().LastFollower();
			collision.gameObject.GetComponent<PlayerController>().AddFollower(this.gameObject);
			//this.gameObject.collider.enabled = false;
			isFollow = true;
		}
	}
	
	private void OnCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Player")
			isFollow = false;
	}
	
	private void OnCollisionExit(Collision collision){
			isFollow = true;
	}
}
