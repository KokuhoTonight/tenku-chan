using UnityEngine;
using System.Collections;

public class PhysicSettings {

	
	//System Settings
	public static readonly int targetFrameRate = 60;
	
	//Level Settings
	public static readonly int floorStep = 2;
	public static readonly float floorRange = 3.5f;
	
	//Movement Settings
	public static readonly float moveForce = 30f;
	public static readonly float moveAccel = 20f;
	public static readonly float cameraSpeed = 10f;
	public static readonly float velocityLimitX = 11f;
	public static readonly float velocityLimitY = 20f;
	public static readonly float constantGravity = -50f;
}
