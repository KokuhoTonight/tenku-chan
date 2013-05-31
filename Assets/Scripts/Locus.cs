using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Locus: MonoBehaviour {
	[SerializeField] GameObject pointPrefab;
	[SerializeField] int pointCount = 10;
	[SerializeField] float pointInterval = 0.15f;
	List<GameObject> points = new List<GameObject>();
	
	void Awake(){
		for( int i=0; i<pointCount; i++){
			var go = Instantiate( pointPrefab) as GameObject;
			go.name = i.ToString();
			points.Add(go);
			go.transform.parent = transform;
		}
	}
	
	void Start(){
		Hide();
	}
	
	public void Show(){
		PointObjectActivity(true);
	}
	
	public void Hide(){
		PointObjectActivity(false);
	}
	
	void PointObjectActivity( bool active){
		foreach( var p in points){
			p.active = active;
		}
	}
	
	public void UpdatePoint( Vector3 origin, Vector3 force){
		float time = 0;
		foreach( var p in points){
			time += pointInterval;
			var newPos = NonsensePhysics.CalcPosition(
				origin,
				force,
				time
				);
			p.transform.position = newPos;
		}
	}
	
}
