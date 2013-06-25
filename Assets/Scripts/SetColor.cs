using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.renderer.material.color = new Color(Random.Range(0.5f, 1.1f), Random.Range(0.5f, 1.1f), Random.Range(0.5f, 1.1f), 1);
	}

}
