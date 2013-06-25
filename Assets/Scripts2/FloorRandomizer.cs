using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorRandomizer : MonoBehaviour {
	
	public List<GameObject> blocks;
	
	void Start()
	{
		DisableBlock();
	}
	
	void DisableBlock () {
		GameObject disable = blocks[Random.Range(0, blocks.Count)];
		disable.active = false;
		blocks.Remove(disable);
		
		if(Random.Range(0, 100) > 80)
		{
			disable = blocks[Random.Range(0, blocks.Count)];
			disable.active = false;
			blocks.Remove(disable);
		}
	}
}
