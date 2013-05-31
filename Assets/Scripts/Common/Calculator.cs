using UnityEngine;
using System.Collections;

public static class Calculator {

	public static float Distance( Vector2 _from, Vector2 _to){
		return Mathf.Sqrt(	Mathf.Pow((_to.x - _from.x), 2 )+Mathf.Pow( (_to.y - _from.y),2 )); 
	}
}
