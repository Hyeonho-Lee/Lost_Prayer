using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour {

    public float rotateSpeed = 10f;
    public float moveSpeed = 2f;

	void Update () {

        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed);
		
	}
}
