using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Rigidbody))]
public class PlayerCharacter : MonoBehaviour {
	
	
	public void Initialize(){
		transform.position = new Vector3( 0, 2.6f, 0);
	}
	
}
