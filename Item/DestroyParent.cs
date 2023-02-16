using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour {

    public float DestroyTime = 2.7f;

    /*void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            Destroy(this.gameObject);
        }
    }*/

    void Start()
    {
        Destroy(this.gameObject, DestroyTime);
    }
}
