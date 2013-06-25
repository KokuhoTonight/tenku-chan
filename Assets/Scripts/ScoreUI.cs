using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {
	
	public GameObject target;
	int score = 0;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpriteText>().Text = "Score: " + score + "points";
	}
	
	// Update is called once per frame
	void Update () {
		if(score < Mathf.Abs(target.transform.localPosition.y))
		{
			score = Mathf.Abs(Mathf.FloorToInt(target.transform.localPosition.y));
			this.gameObject.GetComponent<SpriteText>().Text = "Score: " + score + "points";
		}
	}
}
