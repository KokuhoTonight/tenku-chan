using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	
	public GameObject floorPrefab;
	public GameObject friendPrefab;
	public GameObject enemyPrefab;
	public GameObject player;
	
	List<GameObject> floorList;
	List<GameObject> enemyList;
	List<GameObject> friendList;
	
	public float floorRangeMin = -5.0f;
	public float floorRangeMax = 5.0f;
	public float floorStep = 2.0f;
	public float characterHeight = 1.0f;
	
	bool startGenerate = false;
	
	// Use this for initialization
	void Start () {
		floorList = new List<GameObject>();
		enemyList = new List<GameObject>();
		friendList = new List<GameObject>();
		GenerateFirstFloor();
	}
	
	void GenerateFirstFloor()
	{
		GameObject firstFloor = Instantiate(floorPrefab) as GameObject;
		firstFloor.transform.localPosition = new Vector3(0, - floorStep, 0);
		floorList.Add(firstFloor);
		startGenerate = true;
	}
	
	void GenerateFriend(GameObject newFloor)
	{
		GameObject newFriend = Instantiate(friendPrefab) as GameObject;
		PackedSprite sprite = newFriend.GetComponent<PackedSprite>();
		newFriend.transform.localPosition = new Vector3(newFloor.transform.localPosition.x, newFloor.transform.localPosition.y + 0.78f, 0);
		sprite.PlayAnim(0,Random.Range(0,169));
		friendList.Add(newFriend);
	}
	
	void GenerateEnemy(GameObject newFloor)
	{
		GameObject newEnemy = Instantiate(enemyPrefab) as GameObject;
		newEnemy.transform.localPosition = new Vector3(newFloor.transform.localPosition.x, newFloor.transform.localPosition.y + characterHeight, 0);
		newEnemy.GetComponent<PackedSprite>().PlayAnim(0,48);
		enemyList.Add(newEnemy);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(startGenerate)
		{
			if(floorList.Count < 10)
			{
				GenerateFloor();
			}
			else
			{
				DestroyPanOutObjs();
			}
		}
	}
	
	
	void GenerateFloor()
	{
		GameObject newFloor = Instantiate(floorPrefab) as GameObject;
		newFloor.transform.localPosition = new Vector3(0, floorList[floorList.Count - 1].transform.localPosition.y - floorStep, 0);
		floorList.Add(newFloor);
		
		if(Random.Range(0, 100) > 90)
		{
			GenerateFriend(newFloor);
		}
		else if(Random.Range(0, 100) > 90)
		{
			GenerateEnemy(newFloor);
		}
	}
	
	void DestroyPanOutObjs()
	{
		float playerHeight = player.transform.localPosition.y;
		if(playerHeight + 10 < floorList[0].transform.localPosition.y)
		{
			Destroy(floorList[0]);
			floorList.Remove(floorList[0]);
		}
		
		if(friendList.Count > 0)
		{
			if(playerHeight + 10 < friendList[0].transform.localPosition.y)
			{
				if(!friendList[0].GetComponent<FriendController>().target)
				{
					Destroy(friendList[0]);
					friendList.Remove(friendList[0]);
				}
			}
		}
		
		if(enemyList.Count > 0)
		{
			if(playerHeight + 10 < enemyList[0].transform.localPosition.y)
			{
				Destroy(enemyList[0]);
				enemyList.Remove(enemyList[0]);
			}
		}
	}
}
