using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	[SerializeField] StageObjectGenerator stageObjectGenerater;
	[SerializeField] PlayerCharacter player;
	[SerializeField] Camera camera;
	
	[SerializeField] GUIText text;
	
	bool isHidingText;
	void Awake(){
		isHidingText = false;
		Initialize();
	}
	
	void Initialize(){
		stageObjectGenerater.Initialize( OnGoal);
		player.Initialize();
		
		text.text = "START!";
		if(isHidingText) return;
		isHidingText = true;
		StartCoroutine(HideText());
	}
	IEnumerator HideText(){
		
		yield return new WaitForSeconds(2);
		text.text = "";
		isHidingText = false;
	}
	
	
	void Update(){
		
		if( OutOfScreenDown(player.gameObject))
			Initialize();
		
		if( Input.GetKeyDown(KeyCode.R)){
			Initialize();
		}
	}
	
	
	void OnGoal(){
		text.text = "GOOOOOOOOOOOOOOOAL!";
		StartCoroutine(CR_Restart());
	}
	IEnumerator CR_Restart(){
		yield return new WaitForSeconds(3);
		
		for( int i=3; i>0; i--){
			text.text = i.ToString();
			yield return new WaitForSeconds(1);
		}
		
		Initialize();
	}
	
	bool OutOfScreenDown( GameObject target){
		var ScreenPos = camera.WorldToScreenPoint( target.transform.position);
		var screenHeight = camera.GetScreenHeight();
		if( 0 > ScreenPos.y){
			return true;
		}
		return false;
	}
}
