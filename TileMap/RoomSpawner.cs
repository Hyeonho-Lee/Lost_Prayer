using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomSpawner : MonoBehaviour {

	public int openingDirection;
	// 1 --> need bottom door
	// 2 --> need top door
	// 3 --> need left door
	// 4 --> need right door

	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;

	private string holderName;
	private Transform mapHolder;

	public float waitTime = 4f;

	private NavMeshSurface surface;

	void Start(){
		Destroy(gameObject, waitTime);
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		surface = GameObject.FindObjectOfType<NavMeshSurface>();

		surface.BuildNavMesh();
		Invoke("Spawn", 0.1f);
	}


	void Spawn(){

		if(spawned == false){
			
			mapHolder = GameObject.FindGameObjectWithTag("Rooms").transform;

			if(openingDirection == 1){
				// Need to spawn a room with a BOTTOM door.
				rand = Random.Range(0, templates.bottomRooms.Length);
				GameObject Tiles = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation) as GameObject;
				Tiles.transform.parent = mapHolder;
			} else if(openingDirection == 2){
				// Need to spawn a room with a TOP door.
				rand = Random.Range(0, templates.topRooms.Length);
				GameObject Tiles = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation) as GameObject;
				Tiles.transform.parent = mapHolder;
			} else if(openingDirection == 3){
				// Need to spawn a room with a LEFT door.
				rand = Random.Range(0, templates.leftRooms.Length);
				GameObject Tiles = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation) as GameObject;
				Tiles.transform.parent = mapHolder;
			} else if(openingDirection == 4){
				// Need to spawn a room with a RIGHT door.
				rand = Random.Range(0, templates.rightRooms.Length);
				GameObject Tiles = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation) as GameObject;
				Tiles.transform.parent = mapHolder;
			}
			spawned = true;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("SpawnPoint")){
			spawned = true;
		}
	}
}