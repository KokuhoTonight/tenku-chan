using UnityEngine;
using System.Collections;

public class CoStageObject : StageObject, ISubject {
	IObjserver observer;
	
	
	protected override void onTriggerEnter (Collider other)
	{
		base.onTriggerEnter (other);
		if( other.rigidbody.velocity.y < 0)
			collider.isTrigger = false;
	}
	
	protected override void onCollisionExit (Collision info)
	{
		base.onCollisionExit (info);
		collider.isTrigger = true;
	}
	
	protected override void onCollisionEnter (Collision info)
	{
		base.onCollisionEnter (info);
		if( !IsPlayerCharacter(info.gameObject)) return;
		//reset player rigidbody
		info.rigidbody.angularVelocity = Vector3.zero;
		info.rigidbody.velocity = Vector3.zero;
		
		Debug.Log("GetOn!");
		OnNotify();
		
	}
	
	bool IsPlayerCharacter( GameObject go){
		return go.GetComponent<PlayerCharacter>() != null;
	}
	
	protected virtual void getOnWillStart(){
		OnNotify();
	}
	
	public void SetObserver( IObjserver observer){
		this.observer = observer;
	}
	
	protected void OnNotify(){
		this.observer.Notify( this);
	}
}
