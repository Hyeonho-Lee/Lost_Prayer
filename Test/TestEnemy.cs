using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour {

    private GameObject player;

    public float range = 3f;
    public float rotationMaxSpeed = 3f;
    public float checkAngle = 30f;

    BoxCollider boxcollider;

    void Start() {

        player = GameObject.Find("Player");
        boxcollider = GetComponent<BoxCollider>();
    }

    void Update() {

        var dir = player.transform.position - transform.position;

        if(Vector3.Distance(transform.position, player.transform.position) < range && Vector3.Angle(transform.forward, dir) < checkAngle / 2f) {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir, transform.up), rotationMaxSpeed);
            boxcollider.isTrigger = true;
        }else{

            boxcollider.isTrigger = false;
        }
    }

    void OnDrawGizmos() {

        // 원을 12각형으로 만듬
        var dt = Mathf.PI / 12f;
        var checkRadian = checkAngle * Mathf.Deg2Rad;

        Gizmos.DrawLine(transform.position, GetPosByAngle(-checkRadian / 2f));
        Gizmos.DrawLine(transform.position, GetPosByAngle(checkRadian / 2f));

        for (float t = -checkAngle / 2f; t < checkRadian / 2f; t += dt) {

            Gizmos.DrawLine(GetPosByAngle(t), GetPosByAngle(Mathf.Min(t + dt, checkRadian / 2f)));
        }

        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }

    public Vector3 GetPosByAngle(float radian) {

        return transform.position + range * transform.forward * Mathf.Cos(radian) + range * transform.right * Mathf.Sin(radian);
    }
}
