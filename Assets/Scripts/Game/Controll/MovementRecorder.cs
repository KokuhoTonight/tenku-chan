using UnityEngine;
using System.Collections;

public class MovementRecorder : MonoBehaviour{
	float startTime;
	Vector3 startPos;
	
	MovementParam movementMovementParam;
	
	public void Initialize( MovementParam movementMovementParam){
		this.movementMovementParam = movementMovementParam;
	}
	
	public void Start(){
		//startPos = transform.position;
		startTime = Time.realtimeSinceStartup;
	}
	
	public void Stop(){
		this.movementMovementParam.endPos = transform.position;
		this.movementMovementParam.duration = (Time.realtimeSinceStartup - startTime);
	}
	
	public MovementParam ExportMovementParam(){
		return this.movementMovementParam;
	}
}
public class MovementParam{
	public Vector3 velocity{ set; get;}
	public float duration{ set; get;}
	public Vector3 endPos{ set; get;}
	
	public override string ToString(){
		return string.Format("velocity={0}, duration={1}", velocity, duration);
	}
}