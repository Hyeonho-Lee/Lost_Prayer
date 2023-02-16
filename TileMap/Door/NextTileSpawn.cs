using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTileSpawn : MonoBehaviour {

    //public float centerRange = 20f;
    public float doorRange = 15f;
	private float checkAngle = 360f;
	private int SpawnCount = 0;
    private Vector3 Center;

    private TileMapGenereator tilemap;

    void Start() {

        Center = new Vector3(0, 0, 0);
        tilemap = GameObject.FindObjectOfType<TileMapGenereator>();
        doorRange = (tilemap.mapSize.x + tilemap.mapSize.y);

        CheckDoor();
        //CountDoor();
    }

    void CheckDoor() {

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, doorRange); 

		foreach(Collider hit in colliders) {

			if(hit.tag == "SpawnPos") { 
				
				SpawnCount++;
			}
		}

		if(SpawnCount >= 2) {

            SpawnCount = 0;
            this.gameObject.SetActive(true);
        }else {

            SpawnCount = 0;
            Destroy(this.gameObject);
		}
	}

    /*void CountDoor() {

        Collider[] colliders = Physics.OverlapSphere(Center, centerRange);

        foreach (Collider hit in colliders)
        {

            if (hit.tag == "SpawnPos")
            {

                SpawnCount++;
            }
        }

        if (SpawnCount == 1)
        {

            SpawnCount = 0;
            Debug.Log("1");
        }
        if (SpawnCount == 2)
        {

            SpawnCount = 0;
            Debug.Log("2");
        }
        if (SpawnCount == 3)
        {

            SpawnCount = 0;
            Debug.Log("3");
        }
        if (SpawnCount == 4)
        {

            SpawnCount = 0;
            Debug.Log("4");
        }
    }*/

    void OnDrawGizmos() {
		
		Gizmos.color = Color.green;

        var dt = Mathf.PI / 12f;
        var checkRadian = checkAngle * Mathf.Deg2Rad;

        for (float t = -checkAngle / 2f; t < checkRadian / 2f; t += dt) {

            Gizmos.DrawLine(GetPosByAngle(t), GetPosByAngle(Mathf.Min(t + dt, checkRadian / 2f)));
        }
    }

	 public Vector3 GetPosByAngle(float radian) {

        return transform.position + doorRange * transform.forward * Mathf.Cos(radian) + doorRange * transform.right * Mathf.Sin(radian);
    }
}
