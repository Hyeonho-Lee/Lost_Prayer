using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLock : MonoBehaviour {

    public GameObject LockObject;

    public Vector3 LockRot = new Vector3(0, 0, 0);
    private Vector3 LockRotSave;

    void Start() {

        LockRotSave = LockRot;
        LockRotate();
    }

    void Update() {

        LockRotate();
    }

    public void LockRotate() {

        if(LockRotSave != LockRot) {

            LockObject.transform.Rotate(LockRot);
        }
    }
}
