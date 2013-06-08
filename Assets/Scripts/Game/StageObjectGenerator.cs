using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageObjectGenerator : MonoBehaviour, IObjserver {
	[SerializeField] List<GameObject> objectPrefabs = new List<GameObject>();
	List<GameObject> stageObjects = new List<GameObject>();
	
	[SerializeField] string footstepKey = "plate";
	[SerializeField] string goalKey = "goal_plate";
	//TODO SEPARATE
	[SerializeField] CameraTracker cameraTracker;
	[SerializeField] int END_STEP_COUNT = 6;
	int stepCount;
	
	
	System.Action onGoalAction;
	public void Initialize( System.Action onGoalAction){
		stepCount = 0;
		this.onGoalAction = onGoalAction;
		foreach( var so in stageObjects){
			so.active = false;
		}
		stageObjects.Clear();
		cameraTracker.Move( Vector3.zero.Add( 0, 2.5f, 0));
		for( int i=0; i<6; i++){
			var go = GenerateHigherStep(footstepKey);
			UnityEngine.Random.seed = (int)Time.realtimeSinceStartup * 1000000+i;
			int num = UnityEngine.Random.Range(0,10);
			if( num % 2 != 1)
				go.GetComponentInChildren<Animation>().Play();
		}
	}
	
	public GameObject GenerateHigherStep( string prefabKey){
		stepCount++;
		if( stageObjects.Count == 0)
			return Generate( prefabKey, Vector3.zero);
		else{
			var highest = FindHighest();
			var newPos = ThinkHigherPostion( highest.transform.position);
			return Generate( prefabKey, newPos);
		}
	}
	
	public void Notify( CoStageObject cso){
		Debug.Log( "notify "+cso.gameObject.name);
		if( FindHighest() == cso.gameObject){
			
			if( cso.gameObject.name.Contains( goalKey)){
				OnGoal();
			} else {
				string key = footstepKey;
				if( stepCount >= END_STEP_COUNT){
					key = goalKey;
				}
				var generated = GenerateHigherStep(key);
				
				
			}
		}
		var highestScreenPos = cameraTracker.camera.WorldToScreenPoint( cso.transform.position);
		var screenHeight = cameraTracker.camera.GetScreenHeight();
		if( screenHeight * 0.7 < highestScreenPos.y){
			cameraTracker.LerpTarget();
		}
	}
	
	void OnGoal(){
		if( onGoalAction != null)onGoalAction();
		onGoalAction = null;
		Debug.Log("GOAL!");
	}
	
	Vector3 ThinkHigherPostion( Vector3 pos){
		return pos
			.SetX( UnityEngine.Random.Range( -2.2f, 2.2f))
				//.Add( 0, UnityEngine.Random.Range(0.5f, 3f), 0);
				.Add(0, 2.5f, 0);
	}
	
	GameObject FindHighest(){
		GameObject highest = null;
		foreach( var o in stageObjects){
			if( !o.active) continue;
			if( highest == null) highest = o;
			else if( highest.transform.position.y < o.transform.position.y){
				highest = o;
			}
		}
		
		return highest;
	}
	
	GameObject Generate( string key, Vector3 pos){
		var prefab = objectPrefabs.Where( p => p.name.Equals(key)).FirstOrDefault();
		if( prefab==null)return null;
		GameObject go = Instantiate( prefab, pos, Quaternion.identity) as GameObject;
		Entry( go);
		go.name = prefab.name;
		return go;
	}
	
	void Entry( GameObject go){
		stageObjects.Add( go);
		go.GetComponentInChildren<CoStageObject>().SetObserver( this);		
	}
	[System.SerializableAttribute]
	public class Prefab{
		public string Name{
			get{return this.body.name;}
		}
		public GameObject body;
	}
	
}

