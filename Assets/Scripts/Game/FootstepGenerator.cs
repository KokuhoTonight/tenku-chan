using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FootstepGenerator : MonoBehaviour, IObjserver {
	[SerializeField] GameObject footstep;
	
	List<GameObject> footSteps = new List<GameObject>();
	
	[SerializeField] int END_STEP_COUNT = 6;
	int stepCount;
	
	System.Action onGoalAction;
	
	void Awake(){
		Debug.Log( Display.Screen.width);
		Debug.Log( Display.World.origin);
		Debug.Log( Display.World.width);
	}
	
	public void Initialize( System.Action onGoalAction){
		stepCount = 0;
		this.onGoalAction = onGoalAction;
		foreach( var so in footSteps){
			so.SetActiveRecursively(false);
		}
		footSteps.Clear();
		
		for( int i=0; i<END_STEP_COUNT; i++)
		{
			Generate( -i*1f, new Gauge( UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
		}
			
	}
	
	public void Notify( CoStageObject cso){
		Debug.Log( "notify "+cso.gameObject.name);
			
		if( cso.gameObject.name.Contains( "goal")){
			OnGoal();
		} else {
			
		}
		
	}
	
	void OnGoal(){
		if( onGoalAction != null)onGoalAction();
		onGoalAction = null;
		Debug.Log("GOAL!");
	}
	
	GameObject Generate( GameObject prefab, Vector2 size, Vector2 pos){
		if( prefab==null)return null;
		GameObject go = Instantiate( prefab, pos.ToVector3(), Quaternion.identity) as GameObject;
		go.transform.localScale = go.transform.localScale.Multiply(size.ToVector3(30));
		Entry( go);
		go.name = prefab.name;
		return go;
	}
	
	void Entry( GameObject go){
		footSteps.Add( go);
		go.transform.parent = transform;
		go.GetComponentInChildren<CoStageObject>().SetObserver( this);		
	}
	
	
	public class Step{
		Anchor anchor = Anchor.Center;
		float width;
		float height;
		public Step( Anchor anchor=Anchor.Center, float width=1, float height=0.5f){
			this.anchor = anchor;
			this.width = width;
			this.height = height;
		}
		
	}
	
	public void Generate(float height, params Gauge[] gauges)
	{
		float leftEdge = Display.World.origin.x;
		float ratio = 1f / Display.World.width;
		
		foreach( Gauge g in gauges){
			var width = g.end - g.start;
			Generate( footstep,
				new Vector2( width/ratio, 0.5f),
				new Vector2( leftEdge + (g.start + width/2)/ratio, height)
			);
		}
			
	}
}

public class Gauge{
	public Gauge( float start, float end){
		this.start = start;
		this.end = end;
		Swap();
	}
	void Swap(){
		if( start < end) return;
		var b = start;
		start = end;
		end = b;
	}
	public float start;
	public float end;
}

public enum Anchor{
	Center,
	Left,
	Right
}

