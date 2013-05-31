using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	[SerializeField] Locus locus;
	[SerializeField] GameObject target;
	[SerializeField] Camera cam;
	
	[SerializeField] float powerRegulation = 0.5f;
	
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
	
	bool IsMoving( GameObject target){
		return target.rigidbody.velocity == Vector3.zero ? false : true;
	}
	
	Vector3 TargetPosition{
		get{ 
			return new Vector3( target.transform.position.x, target.transform.position.y, 0);
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
	
	Vector3 origin = Vector3.zero;
	// Update is called once per frame
	void Update () {
		if( TouchStart && IsHit(target) ){
			Debug.Log("On");
			isDragging = true;
			
			//sling
			target.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			//target.rigidbody.MovePosition( DragPoint + Vector3.up*2);
			//sling--
			target.rigidbody.velocity = Vector3.zero;
			
			onPos = TargetPosition;
			locus.Show();
		}
		if( Touching && isDragging){
			//target.transform.position = DragPoint;
			//target.rigidbody.MovePosition( DragPoint);
		}
		if( TouchEnd && isDragging){
			//var ray = Camera.mainCamera.ScreenToWorldPoint( Input.mousePosition );
			//Debug.Log("Off :" +ray);
			isDragging = false;
		
			target.rigidbody.velocity = Vector3.zero;
			Throw();
		}
		if( isDragging){
			var f = CalcForce();
			locus.UpdatePoint(target.transform.position, f);
		}
		if( !Touching ){
			origin = TargetPosition;
		}
	}
	
	Vector3 CalcForce(){
		var frm = onPos;
		var to = DragPoint;
		float dist =  Calculator.Distance( frm.ToVector2(), to.ToVector2());
		var dir = Vector3.Normalize( frm - to);
		return dir * dist * powerRegulation;
	}
	
	void Throw(){
		locus.Hide();
		target.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
		var f = CalcForce();
		target.rigidbody.AddForce( f, ForceMode.Impulse);
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
