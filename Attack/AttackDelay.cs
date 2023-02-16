using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelay : MonoBehaviour {

    public float AttackDealy = 1f;
    public float AttackTime = 0;
    public bool Attackbool = false;

    private AttackController attackcontroller;

    void Awake() {

        attackcontroller = GameObject.FindObjectOfType<AttackController>();
    }

    void Start() {

        Attackbool = true;
    }

    void Update(){

        if(AttackTime >= AttackDealy) {

            StopCoroutine(Delay());
        }
        else {

            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay() {

        AttackTime += Time.deltaTime;

        if (AttackTime >= AttackDealy) {

            Attackbool = true;
        }

        yield return null;
    }
}
