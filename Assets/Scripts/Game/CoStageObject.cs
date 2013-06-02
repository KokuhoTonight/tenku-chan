using UnityEngine;
using System.Collections;

public class CoStageObject : StageObject, ISubject {
	IObjserver observer;
	
	
	protected override void onCollisionEnter (Collision info)
	{
		base.onCollisionEnter (info);
		if( IsPlayerCharacter(info.gameObject)){
			//reset player rigidbody
			info.rigidbody.angularVelocity = Vector3.zero;
			info.rigidbody.velocity = Vector3.zero;
			
			Debug.Log("GetOn!");
			OnNotify();
		}
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
