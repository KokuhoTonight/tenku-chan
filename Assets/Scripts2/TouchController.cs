using UnityEngine;
using System.Collections;

public class TouchController: MonoBehaviour {
	
	public GameObject player;
	public PlayerController controller;
	public float fource = 50f;
	public float acceleration = 50f;
	public float velocityLimit = 50f;
	
	public bool isActiveFource = true;
	
	//for iOS controll
	protected virtual void CheckTouch()
    {
        if ( Input.touchCount <= 0 ){
            return;
        }
		
		Touch touch = Input.GetTouch(0);
		if ( touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary ){
			Vector2 point = touch.position;
			Vector3 pos = Camera.main.ScreenToWorldPoint(point);
			if(pos.x > 0)
			{
				player.rigidbody.AddForce(Vector3.right * fource, ForceMode.Force);
				player.rigidbody.AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
			}
			else
			{
				player.rigidbody.AddForce(Vector3.left * fource, ForceMode.Force);
				player.rigidbody.AddForce(Vector3.left * acceleration, ForceMode.Acceleration);
			}
			
			VelocityLimit();;
		}
	}
	
	//for UnityEditor controll
	protected virtual void CheckClick()
	{
		if(Input.GetMouseButton(0))
		{
			Vector2 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector3 pos = Camera.main.ScreenToWorldPoint(point);
			if(pos.x > 0)
			{
				player.rigidbody.AddForce(Vector3.right * fource, ForceMode.Force);
				player.rigidbody.AddForce(Vector3.right * fource, ForceMode.Acceleration);
			}
			else
			{
				player.rigidbody.AddForce(Vector3.left * fource, ForceMode.Force);
				player.rigidbody.AddForce(Vector3.left * fource, ForceMode.Acceleration);
			}
			
			VelocityLimit();
			//Vector3 playerPos = player.transform.localPosition;
			//player.transform.localPosition = Vector3.Slerp(playerPos, new Vector3(pos.x, playerPos.y, playerPos.z), fource);
		}
	}
	
	void VelocityLimit()
	{
		Vector3 velocity = player.rigidbody.velocity;
		if(velocity.y > 0)
			player.rigidbody.velocity = new Vector3(velocity.x, 0, 0);
		if(velocity.x < -velocityLimit)
		{
			player.rigidbody.velocity = new Vector3 (-velocityLimit, velocity.y, 0);
		}
		else if(velocity.x > velocityLimit)
		{
			player.rigidbody.velocity = new Vector3(velocityLimit, velocity.y, 0);
		}
	}
	
	void Update()
	{
		if(isActiveFource)
		{
#if UNITY_IPHONE
			CheckTouch();
#endif
			CheckClick();
		}
	}
}
