using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	[SerializeField]
	GameObject target;
	[SerializeField]
	Camera cam;
	//[SerializeField]
	//iTween.EaseType easetype;
	[SerializeField]
	float backTime;
	
	// Use this for initialization
	void Start () {
		EnvironmentSetting();
	}
	bool TouchStart{
		get{
			return Input.GetMouseButtonDown(0);
		}
	}
	bool TouchEnd{
		get{
			return Input.GetMouseButtonUp(0);
		}
	}
	bool Touching {
		get{ 
			return Input.GetMouseButton(0);
		}
	}
	Vector3 DragPoint{
		get{
			//return Camera.mainCamera.ScreenToWorldPoint( Input.mousePosition );
			return cam.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y,  cam.transform.position.z * -1) );
		}
	}
	bool IsHit(GameObject target){
		RaycastHit hit = new RaycastHit();
		Ray ray = cam.ScreenPointToRay(  new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z * -1) );
		Physics.Raycast(ray, out hit, 1000f);
		
		if( hit.collider == null ){
			return false;
		} else {
			Debug.Log(hit.collider.name);
			return hit.collider.gameObject.Equals(target);
		}
	}
	bool isDragging;
	Vector3 onPos;
	Vector3 offPos;
	Vector3 origin = Vector3.zero;
	// Update is called once per frame
	void Update () {
		if( TouchStart && IsHit(target) ){
			Debug.Log("On");
			isDragging = true;
			
			//sling
			target.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			target.rigidbody.MovePosition( DragPoint + Vector3.up*30);
			//sling--
			onPos = origin;
		}
		if( Touching && isDragging){
			//target.transform.position = DragPoint;
			target.rigidbody.velocity = Vector3.zero;
			//target.rigidbody.MovePosition( DragPoint);
		}
		if( TouchEnd && isDragging){
			//var ray = Camera.mainCamera.ScreenToWorldPoint( Input.mousePosition );
			//Debug.Log("Off :" +ray);
			isDragging = false;
			
			//offPos = TargetPosition;
			offPos = DragPoint;
			
			//direction = Vector3.Normalize( offPos - onPos) * -1;
			direction = Vector3.Normalize( offPos - onPos);
			distance = Mathf.Sqrt( Mathf.Pow((offPos.x - onPos.x), 2 ) + Mathf.Pow( (offPos.y - onPos.y),2 ) ); 
			//iTween.MoveTo( target, iTween.Hash("position", origin, "time", backTime, "oncomplete", "Throw", "oncompletetarget",gameObject));
			
			
			//target.rigidbody.MovePosition( origin );
			target.rigidbody.velocity = Vector3.zero;
			Throw();
		}
		
		if( !Touching ){
			origin = TargetPosition;
		}
	}
	bool IsMoving( GameObject target){
		return target.rigidbody.velocity == Vector3.zero ? false : true;
	}
	Vector3 TargetPosition{
		get{ 
			return new Vector3( target.transform.position.x, target.transform.position.y, 0);
		}
	}
	Vector3  direction;
	float distance;
	[SerializeField]
	int num;
	void EnvironmentSetting(){
		num = 100;
		Physics.gravity = new Vector3( 0, (float)-9.81 * num, 0);
	}
	void Throw(){
		target.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
		direction = new Vector3( direction.x, direction.y, 0);
		target.rigidbody.AddForce( -direction * distance * num / 10, ForceMode.Impulse);
		Debug.Log(direction);
	}
	
		/*
		if (Input.touchCount > 0) {
			var ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
			Debug.Log( ray );
		}
        

//        var touchDeltaPosition : Vector2 = Input.GetTouch (0).deltaPosition;

        
		/*
        if(Physics.Raycast(ray,hit,50,layerMask))

        {           

            if(Input.touchCount == 1)

            {           

                hit.collider.gameObject.transform.Translate (touchDeltaPosition.x * speed, touchDeltaPosition.y * speed,0);     

            }   

        }*/
}
