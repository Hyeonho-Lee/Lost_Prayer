using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowDropItem : MonoBehaviour {

    public Transform target;
    public float MinModifier = 0.5f;
    public float MaxModifier = 2;

    Vector3 _velocity = Vector3.zero;
    bool isFolowing = false;

    public void StartFolowing()
    {
        isFolowing = true;
    }

    void Update()
    {

        if(isFolowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}
