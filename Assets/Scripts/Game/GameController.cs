using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	[SerializeField] FootstepGenerator footstepGenerater;
	[SerializeField] PlayerCharacter player;
	[SerializeField] TopNeedle topNeedle;
	[SerializeField] Camera camera;
	
	[SerializeField] GUIText text;
	bool isGoalable;
	bool isHidingText;
	void Awake(){
		isHidingText = false;
		
	}
	
	void Start(){
		Initialize();
	}
	
	void Initialize(){
		isGoalable = true;
		footstepGenerater.Initialize( OnGoal);
		topNeedle.Initialize(OnGameover);
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
		
		if( Input.GetKeyDown(KeyCode.R)){
			Initialize();
		}
	}
	
	void OnGameover(){
		isGoalable = false;
		text.text = "GAME OVER!!!!";
		StartCoroutine(CR_Restart());
	}
	void OnGoal(){
		if( !isGoalable)return;
		topNeedle.Stop();
		text.text = "GOOOOOOOOOOOOOOOAL!";
		StartCoroutine(CR_Restart());
	}
	IEnumerator CR_Restart(){
		yield return new WaitForSeconds(1);
		
		for( int i=3; i>0; i--){
			text.text = i.ToString();
			yield return new WaitForSeconds(.7f);
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
