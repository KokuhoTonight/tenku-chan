using UnityEngine;
using System.Collections;

public class NonsensePhysics : MonoBehaviour {

	
	public static Vector3 Gravity{
		get{
			return Physics.gravity;
		}
	}
	
	public static Vector3 CalcPosition(
		Vector3 position,
		Vector3 velocity,
		float time
		)
	{
		return position + velocity * time + .5f * Gravity * time * time;
	}
}
