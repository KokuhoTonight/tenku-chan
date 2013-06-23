using UnityEngine;
using System.Collections;

public class CoStageObject : StageObject, ISubject {
	IObjserver observer;
	
	public void Touchable( bool active){
		collider.isTrigger = !active;
	}
	
	protected override void onCollisionExit (Collision info)
	{
		base.onCollisionExit (info);
		Touchable(false);
	}
	
	protected override void onCollisionEnter (Collision info)
	{
		base.onCollisionEnter (info);
		if( !IsPlayerCharacter(info.gameObject)) return;
		//reset player rigidbody
		//info.rigidbody.angularVelocity = Vector3.zero;
		//info.rigidbody.velocity = Vector3.zero;
		
		//Debug.Log("GetOn!");
		OnNotify();
		
	}
	
	bool IsPlayerCharacter( GameObject go){
		return go.GetComponent<MovementSection>() != null;
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
	protected override bool isKinnematic ()
	{
		return true;
	}
	protected override bool isTrigger ()
	{
		return true;
	}
}
