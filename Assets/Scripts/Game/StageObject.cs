using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody))]
public class StageObject : MonoBehaviour {
	
	void Awake(){
		rigidbody.constraints = constraint();
		rigidbody.useGravity = useGravity();
		collider.isTrigger = isTrigger();
		rigidbody.isKinematic = isKinnematic();
	}
	
	protected virtual bool isTrigger(){
		return true;
	}
	
	protected virtual bool useGravity(){
		return false;
	}
	
	protected virtual bool isKinnematic(){
		return false;
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
	void OnTriggerStay( Collider other){
		onTriggerStay(other);
	}
	protected virtual void onTriggerStay( Collider other){}
		
	
	void OnCollisionEnter( Collision info){
		onCollisionEnter(info);
	}
	protected virtual void onCollisionEnter( Collision info){
		Debug.Log( "onCollisionEnter "+info.gameObject.name);
	}
	void OnCollisionStay( Collision info){
		onCollisionStay(info);
	}
	protected virtual void onCollisionStay( Collision info){
		//Debug.Log( "onCollisionStay "+info.gameObject.name);
	}
	void OnCollisionExit( Collision info){
		onCollisionExit(info);
	}
	protected virtual void onCollisionExit( Collision info){
		Debug.Log( "onCollisionExit "+info.gameObject.name);
	}
}
