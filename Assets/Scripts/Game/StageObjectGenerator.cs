using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageObjectGenerator : MonoBehaviour, IObjserver {
	[SerializeField] List<GameObject> objectPrefabs = new List<GameObject>();
	List<GameObject> stageObjects = new List<GameObject>();
	
	
	//TODO SEPARATE
	[SerializeField] CameraTracker cameraTracker;
	
	// Use this for initialization
	void Start () {
		GenerateHigherStep();
	}
	
	public GameObject GenerateHigherStep(){
		if( stageObjects.Count == 0)
			return Generate( Vector3.zero);
		else{
			var highest = FindHighest();
			var newPos = ThinkHigherPostion( highest.transform.position);
			return Generate( newPos);
		}
	}
	
	public void Notify( CoStageObject cso){
		Debug.Log( "notify "+cso.gameObject.name);
		if( FindHighest() == cso.gameObject){
			
			var generated = GenerateHigherStep();
			
			var highestScreenPos = cameraTracker.camera.WorldToScreenPoint( cso.transform.position);
			var screenHeight = cameraTracker.camera.GetScreenHeight();
			if( screenHeight * 0.7 < highestScreenPos.y){
				cameraTracker.LerpTarget();
			}
				
		}
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
			if( highest == null) highest = o;
			else if( highest.transform.position.y < o.transform.position.y){
				highest = o;
			}
		}
		
		return highest;
	}
	
	GameObject Generate( Vector3 pos){
		if( objectPrefabs.Count == 0)return null;
		GameObject go = Instantiate( objectPrefabs[0], pos, Quaternion.identity) as GameObject;
		Entry( go);
		return go;
	}
	
	void Entry( GameObject go){
		stageObjects.Add( go);
		go.GetComponent<CoStageObject>().SetObserver( this);		
	}
}
