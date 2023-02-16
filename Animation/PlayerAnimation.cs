using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public float speed = 1f;

    void Update() {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!(h == 0 && v == 0)){

            Vector3 move = new Vector3(h, 0, v); 
            Quaternion dir = Quaternion.LookRotation(move.normalized);
            dir.x = 0;
            dir.z = 0;
            transform.rotation = dir;
        }
    }
}
