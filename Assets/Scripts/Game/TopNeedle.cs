using UnityEngine;
using System.Collections;

public class TopNeedle : StageObject {
	[SerializeField] float speed = 2;
	System.Action onHit;
	
	bool isPerceptable = false;
	
	public void Initialize( System.Action onHit){
		transform.position = new Vector3( 0, 8f, 0);
		this.onHit = onHit;
		isPerceptable = true;
	}
	
	public void Stop(){
		isPerceptable = false;
	}
	
	protected override bool isTrigger ()
	{
		return true;
	}
	
	protected override void onTriggerEnter (Collider other)
	{
		if( other.gameObject.name.Equals("HEAD")){
			if(onHit!=null) onHit();
			isPerceptable = false;
		}
	}
	
	void Update(){
		
		if( isPerceptable)transform.position = transform.position.Add( 0, -speed*Time.deltaTime, 0);
	}
}
