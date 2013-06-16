using UnityEngine;
using System.Collections;

public class DistanceKeeper : MonoBehaviour {

	[SerializeField] Transform target;
	float keepDistance = .5f;
	
	void Update(){
		Keep();
	}
	
	void Keep(){
		float distance = 
			Calculator.Distance(
				transform.position.ToVector2(),
				target.position.ToVector2()
				);
		
		if( keepDistance >= distance) return;
		float z = transform.position.z;
		Vector2 xy = Vector2.Lerp(
			transform.position.ToVector2(),
			target.position.ToVector2(),
			1 - keepDistance / distance
			);
		transform.position = new Vector3( xy.x, xy.y, z);
		Debug.Log( "difffff "+ distance + " keepdistance "+ keepDistance);
		
	}
	/*
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
		*/
}
