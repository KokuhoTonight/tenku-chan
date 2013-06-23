using UnityEngine;
using System.Collections;

[RequireComponent( typeof(MovementRecorder))]
public class MovementSection : MonoBehaviour {
	[SerializeField] bool head;
	[SerializeField] float speed = 0.12f;
	MovementParam currentMovementParam;
	MovementParam pastMovementParam;
	
	[SerializeField] float updateInterval = 0.1f;
		
	float updatedTime;
	
	void Awake(){
	}
	
	void Update(){
		
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			if( head){
				transform.position = transform.position.Add( speed, 0, 0);
			}
		}
		if(Input.GetKeyUp(KeyCode.RightArrow)){
			
		}
		
		
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			if( head){
				transform.position = transform.position.Add( -speed, 0, 0);
			}
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow)){
			
		}
	}
}
