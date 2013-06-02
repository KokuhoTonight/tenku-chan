using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody))]
public class StageObject : MonoBehaviour {
	
	void Awake(){
		rigidbody.constraints = constraint();
		if( isStatic())
			rigidbody.useGravity = false;
	}
	
	protected virtual bool isStatic(){
		return true;
	}
	
	protected virtual RigidbodyConstraints constraint(){
		return RigidbodyConstraints.FreezeAll;
	}
	
	void OnTriggerEnter( Collider other){
		onTriggerEnter(other);
	}
	protected virtual void onTriggerEnter( Collider other){
		Debug.Log( "onTriggerEnter "+other.gameObject.name);
	}
	
	void OnCollisionEnter( Collision info){
		onCollisionEnter(info);
	}
	protected virtual void onCollisionEnter( Collision info){
		Debug.Log( "onCollisionEnter "+info.gameObject.name);
	}
}
