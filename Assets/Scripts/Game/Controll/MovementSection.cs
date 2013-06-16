using UnityEngine;
using System.Collections;

[RequireComponent( typeof(MovementRecorder))]
public class MovementSection : MonoBehaviour {
	[SerializeField] bool head;
	float speed = 0.5f;
	MovementParam currentMovementParam;
	MovementParam pastMovementParam;
	
	[SerializeField] float updateInterval = 0.1f;
	
	[SerializeField] MovementRecorder recorder;
	[SerializeField]MovementSection childMovementSection;
		
	
	void Awake(){
		MovementRecorder recorder = gameObject.GetComponent<MovementRecorder>();
	}
	
	void Update(){
		
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			RecordStart(speed); 
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			if( head){
				Debug.Log("righti");
			transform.position = transform.position.Add( speed, 0, 0);
			}
		}
		if(Input.GetKeyUp(KeyCode.RightArrow)){
			RecordStop();
		}
		
		
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			RecordStart(-speed);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			if( head){
				Debug.Log("lefti");
			transform.position = transform.position.Add( -speed, 0, 0);
			}
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow)){
			RecordStop();
		}
	}
	
	
	public void Play( MovementParam movementParam){
		StopCoroutine( "CR_Move");
		pastMovementParam = currentMovementParam;
		currentMovementParam = movementParam;
		StartCoroutine( "CR_Move",movementParam);
	}
	
	IEnumerator CR_Move( MovementParam movementParam){
		float t = 0;
		Vector3 fromPos = transform.position;
		Vector3 pos = fromPos;
		while( pos.x != movementParam.endPos.x){
			pos = Vector3.Lerp( fromPos, movementParam.endPos, t);
			t += Time.deltaTime/movementParam.duration;
			yield return null;
			transform.position = new Vector3( pos.x, transform.position.y, transform.position.z);//change position
			
		}
		OnRecordFinish( movementParam);
	}
	/*
	 * color = fromColor;
		
		float t = 0;
		while( color != toColor){
			color = Color.Lerp( fromColor, toColor, t);
			t += Time.deltaTime/time;
			yield return null;
		}
		OnFinish();*/
	
	void RecordStart( float velocityX){
		MovementParam p = new MovementParam();
		p.velocity = new Vector3( velocityX, 0, 0);
		
		recorder.Initialize( p);
		recorder.Start();
	}
	void RecordStop(){
		recorder.Stop();
		OnRecordFinish(recorder.ExportMovementParam());
	}
	void OnRecordFinish( MovementParam movementParam){
		if( childMovementSection!=null)childMovementSection.Play( movementParam);
	}
	
	
	
	
	
	
}
