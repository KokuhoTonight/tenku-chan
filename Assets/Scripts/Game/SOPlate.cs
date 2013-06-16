using UnityEngine;
using System.Collections;

public class SOPlate : CoStageObject {

	protected override bool isTrigger ()
	{
		return false;
	}
	
	protected override void onCollisionEnter (Collision info)
	{
		//base.onCollisionEnter (info);
	}
	protected override void onCollisionExit (Collision info)
	{
		//base.onCollisionExit (info);
	}
}
