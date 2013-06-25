using UnityEngine;
using System.Collections;

public class FriendController : MonoBehaviour {

	public GameObject target;
	public float speed;
	public bool isFollow = true;
	
	void Update () {
		if(isFollow && target){
			Vector3 myPos = this.gameObject.transform.position;
			Vector3 targetPos = target.transform.position;
			this.gameObject.transform.position = Vector3.Lerp(myPos, new Vector3(targetPos.x, targetPos.y, myPos.z), Time.deltaTime * speed);
			this.gameObject.transform.rotation = Quaternion.Lerp (this.gameObject.transform.rotation, target.transform.rotation, Time.deltaTime * speed);
		}
	}
	
	void CheckPositionForOmit()
	{
		Vector3 myPos = this.gameObject.transform.position;
		Vector3 targetPos = target.transform.position;
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
