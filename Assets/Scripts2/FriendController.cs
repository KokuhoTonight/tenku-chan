using UnityEngine;
using System.Collections;

public class FriendController : MonoBehaviour {

	public GameObject target;
	public float speed;
	public bool isFollow = true;
	
	void LateUpdate () {
		if(isFollow && target){
			Vector3 myPos = this.gameObject.transform.position;
			Vector3 targetPos = target.transform.position;
			//this.gameObject.rigidbody.MovePosition(targetPos);
			this.gameObject.transform.position = Vector3.Lerp(myPos, new Vector3(targetPos.x, myPos.y, myPos.z), Time.deltaTime * speed);
			//this.gameObject.transform.rotation = Quaternion.Lerp (this.gameObject.transform.rotation, target.transform.rotation, Time.deltaTime * speed);
		}
		VelocityLimit();
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
	
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player" && !target)
		{
			this.target = collision.gameObject.GetComponent<PlayerController>().LastFollower();
			collision.gameObject.GetComponent<PlayerController>().AddFollower(this.gameObject);
			isFollow = true;
			this.gameObject.layer = 10;
			this.gameObject.rigidbody.useGravity = true;
			this.gameObject.constantForce.enabled = true;
		}
	}
	
	private void OnCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Player")
			isFollow = false;
	}
	
	private void OnCollisionExit(Collision collision){
			isFollow = true;
	}
	
	void VelocityLimit()
	{
		Vector3 velocity = this.rigidbody.velocity;
		if(velocity.y > 0)
			this.rigidbody.velocity = new Vector3(velocity.x, 0, 0);
		if(velocity.y < -11)
			this.rigidbody.velocity = new Vector3(velocity.x, -11, 0);
		
		if(velocity.x < -11)
		{
			this.rigidbody.velocity = new Vector3 (-11, velocity.y, 0);
		}
		else if(velocity.x > 11)
		{
			this.rigidbody.velocity = new Vector3(11, velocity.y, 0);
		}
	}
}
