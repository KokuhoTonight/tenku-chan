using UnityEngine;
using System.Collections;

public class TouchControlller3 : MonoBehaviour {
	
	public Camera GUICamera;
	public UIButton leftButton;
	public UIButton rightButton;
	
	public GameObject target;
	public float speedX = 1f;
	public float limitX = 7.5f;
	
	public void OnLeftTouched()
	{
		Debug.Log("OnLeftTouched");
		target.renderer.material.color = Color.red;
		MoveX(speedX * -1);
	}
	
	public void OnRightTouched()
	{
		Debug.Log("OnRightTouched");
		target.renderer.material.color = Color.blue;
		MoveX(speedX);
	}
				
	void MoveX(float val)
	{
		if(target.transform.localPosition.x + val > -limitX && target.transform.localPosition.x + val < limitX)
		{
			Vector3 currentPos = target.transform.localPosition;
			target.transform.localPosition = new Vector3(currentPos.x + val, currentPos.y, currentPos.z);
			
			//target.rigidbody.AddForce(Vector3.ri
		}
	}
	
  protected virtual void CheckTouch()
    {
        if ( Input.touchCount <= 0 )
        {
            return;
        }
		
		Touch touch = Input.GetTouch(0);
		if ( touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary )
		{
		
			Vector2 point = touch.position;
			RaycastHit hit = new RaycastHit();
			Ray ray = GUICamera.ScreenPointToRay( point );
			
			if ( Physics.Raycast( ray, out hit ) )
			{
				if(hit.transform.gameObject.tag == "Button")
				{
					hit.transform.gameObject.SendMessage("OnMouseEnter");
				}
			}
		}
		else if( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
		{
			Vector2 point = touch.position;
			RaycastHit hit = new RaycastHit();
			Ray ray = GUICamera.ScreenPointToRay( point );
			
			if ( Physics.Raycast( ray, out hit ) )
			{
				if(hit.transform.gameObject.tag == "Button")
				{
					hit.transform.gameObject.SendMessage( "OnMouseExit" );
				}
			}
		}
	}
	
	void Update()
	{
		CheckTouch();
	}
}
