using UnityEngine;
using System.Collections;

public class FourceBoucer : MonoBehaviour {
	
	public float boucePower = 25f;
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			//Debug.Log ("Bouce");
			Vector3 direction;
			if(collision.gameObject.transform.localPosition.x > 0)
				direction = new Vector3(-1, -0.2f, 0);
			else
				direction = new Vector3(1, -0.2f, 0);
			//collision.gameObject.rigidbody.velocity = Vector3.zero;
			collision.gameObject.rigidbody.AddForce(direction * boucePower, ForceMode.Impulse);
		}
	}
}
