using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRot : MonoBehaviour {

    public Transform target;

    void Update() {

        // 방향값을 구함
        Vector3 dist = (target.position - transform.position).normalized;
        // 이동시킴
        transform.rotation = Quaternion.LookRotation(dist);
    }
}
