using UnityEngine;
using System.Collections;

public class TriggerNotice : StageObject {
	
	[SerializeField] CoStageObject target;
	
	protected override bool isKinnematic ()
	{
		return true;
	}
	
	protected override void onTriggerEnter (Collider other)
	{
		var shadow = other.GetComponent<Shadow>();
		if( shadow==null)return;
		
		if( shadow.body.rigidbody.velocity.y < 0)
			target.Touchable(true);
	}
	
	protected override void onTriggerStay (Collider other)
	{	
		onTriggerEnter(other);
	}
	
}
