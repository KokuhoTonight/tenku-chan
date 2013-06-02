using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour {
	
	[SerializeField]
	float moveSpeed = 10.0f;
	[SerializeField]
	float moveWidth = 5.0f;
	GameObject target;
	float goal;
	bool moveLeft;
	// Use this for initialization
	void Start () {
		SetTarget();
		InitStartVector();
	}
	
	void SetTarget () {
		target = this.gameObject;
	}
	
	void InitStartVector () {
		Random.seed = (int)Time.realtimeSinceStartup + 1;
		if( Random.Range(0,100) < 50 )
			moveLeft = true;
		else
			moveLeft = false;
		moveSpeed = Random.Range(10, 20);
		moveWidth = Random.Range(-8, 8);
	}
	
	public float interval = 3f;
	float time = 0f;
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(time > interval)
		{
			moveLeft = !moveLeft;
			time = 0;
		}
		
		if(moveLeft)
			target.transform.position = 
				Vector3.Lerp(target.transform.position, new Vector3(target.transform.position.x - moveWidth, target.transform.position.y, target.transform.position.z), Time.deltaTime * moveSpeed);
		else
			target.transform.position = 
				Vector3.Lerp(target.transform.position, new Vector3(target.transform.position.x + moveWidth, target.transform.position.y, target.transform.position.z), Time.deltaTime * moveSpeed);
	}
			
	
}
