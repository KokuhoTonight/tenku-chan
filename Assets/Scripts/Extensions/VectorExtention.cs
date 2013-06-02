using UnityEngine;
using System.Collections;

public static class VetorExtentions{
	//Vector2
	public static Vector3 ToVector3( this Vector2 v, float z=0){
		return new Vector3( v.x, v.y, z);
	}
		
		
	//Vector3
	public static Vector2 ToVector2( this Vector3 v){
		return new Vector2( v.x, v.y);
	}
	public static Vector3 SetX( this Vector3 v, float x){
		return new Vector3( x, v.y, v.z);
	}
	public static Vector3 Add( this Vector3 v, float x, float y, float z){
		return new Vector3( v.x+x, v.y+y, v.z+z);
	}
	
}