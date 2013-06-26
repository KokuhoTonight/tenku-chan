using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float speed = 1;
	public bool start = false;
	public AudioSource audio;
	
	public GameObject player;
	// Update is called once per frame
	void Update () {
		if(player.rigidbody.velocity.x > 0 && !start)
		{
			start = true;
			audio.Play();
		}
		if(start)
		{
			Vector3 myPos = this.gameObject.transform.localPosition;
			this.gameObject.transform.localPosition = new Vector3(myPos.x, myPos.y - speed, myPos.z);
		}
	}
			
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Time.timeScale = 1f;
			Application.LoadLevel(0);
		}
	}
	
	private void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Time.timeScale = 1f;
			Application.LoadLevel(0);
		}
	}
}
