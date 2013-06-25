using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public bool isActiveFource = false;
	public List<GameObject> followers;
	public GameObject particlePrefab;
	public GameObject sprite;
	
	void Start(){
		followers = new List<GameObject>();
		followers.Add(this.gameObject);
	}
	
	public GameObject LastFollower()
	{
		return followers[followers.Count - 1];
	}
	
	public void AddFollower(GameObject follower)
	{
		followers.Add(follower);
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Floor")
		{
			isActiveFource = true;
			//ScaleAction();
		}
		else if(collision.gameObject.tag == "Enemy")
		{
			isActiveFource = false;
		}
	}
	
	private void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.tag == "Floor")
			isActiveFource = true;
		else
			isActiveFource = false;
	}
	
	private void OnCollisionExit(Collision collision)
	{
		isActiveFource = false;
		//Vector3 velocity = this.gameObject.rigidbody.velocity;
		//this.gameObject.rigidbody.velocity = new Vector3(velocity.x * 0.3f, velocity.y, 0);
	}

	void ScaleAction()
	{
		Vector3 velocity = this.gameObject.rigidbody.velocity;
		iTween.PunchScale(sprite, iTween.Hash("x", velocity.y / 2, "y", velocity.x / 2, "time", 0.3f));
	}
	
}
