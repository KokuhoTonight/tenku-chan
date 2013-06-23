using UnityEngine;
using System.Collections;

public class Display {

	public static Camera gameCamera{
		get {
			return Camera.main;
		}
	}
	
	public static class Screen{
		public static float width{
			get{
				return Display.gameCamera.GetScreenWidth();
			}
		}
	}
	
	public static class World{
		public static Vector3 origin{
			get{
				return Display.gameCamera.ScreenToWorldPoint( Vector3.zero);
			}
		}
		
		public static float width{
			get{
				return  Mathf.Abs( origin.x * 2);
			}
		}
	
	}
}
