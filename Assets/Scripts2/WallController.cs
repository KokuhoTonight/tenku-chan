using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {
	
	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		FollowPlayer();
	}
	
	void FollowPlayer(){
		this.gameObject.transform.localPosition = new Vector3(0, player.transform.localPosition.y, 0);
	}
}
