using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour {

    public float offsetX = -12f;
    public float offsetY = 10f;
    public float offsetZ = -12f;

    public GameObject player;

    void Update() {

        transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);
    }
}
