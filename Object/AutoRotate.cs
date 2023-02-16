using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    public float speed = 0f;

    void FixedUpdate() {

        transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
    }
}
