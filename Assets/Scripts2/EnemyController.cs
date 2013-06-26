using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	Vector3 currentVelocity;
	GameObject player;
	public GameObject particlePrefab;
	GameObject particle;
	// Use this for initialization
	private void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			player = collider.gameObject;
			this.collider.enabled = false;
			Time.timeScale = 0.1f;
			/*
			currentVelocity = collider.gameObject.rigidbody.velocity;
			collider.gameObject.rigidbody.velocity = currentVelocity * 0.001f;
			Camera.main.GetComponent<TouchController>().isActiveFource = false;
			*/
			//particle = Instantiate(particlePrefab) as GameObject;
			//particle.transform.parent = this.transform;
			//particle.transform.localPosition = new Vector3(0,0,-0.1f);

			StartCoroutine("FinishSlowmo");
		}
	}
	
	IEnumerator FinishSlowmo()
	{
		yield return new WaitForSeconds(0.021f);
		player.rigidbody.velocity = Vector3.zero;
		Time.timeScale = 1f;
		/*
		player.rigidbody.velocity = currentVelocity;
		Camera.main.GetComponent<TouchController>().isActiveFource = true;
		*/
		this.gameObject.active = false;
		//Destroy(particle);
		//particle.active = false;
	}
}