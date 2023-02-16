using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	private float waitTime = 1f;
	private bool spawnedBoss;
	public GameObject boss;

	void Start() {
	
		InvokeRepeating("CheckBoss", .1f, .1f);
	}

	void CheckBoss() {

		if(waitTime <= 0 && spawnedBoss == false){

			for (int i = 0; i < rooms.Count; i++) {

				if(i == rooms.Count-1){

					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
					CancelInvoke("CheckBoss");
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}
}
