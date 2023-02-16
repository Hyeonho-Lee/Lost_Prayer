using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {

    private Vector3 target;
    private Transform target_pos;
    private Vector3 prevPos;
    private Vector3 startPos;
    private Transform trans;

    public float speed = 5f;
    public float turnspeed = 5f;

    private float randomOffset = 0.0f;

    public float time = 100f;
    public float timer = 0.0f;

    private float xPow;
    private float yPow;
    private float zPow;

    // 적을 찾는 함수
    public void Seek(Transform _target) {

        // 적을 지정
        target_pos = _target;
    }

    void Start() {

        target = target_pos.position;

        xPow = Random.Range(0.4f, 3.0f);
        yPow = Random.Range(0.4f, 3.0f);
        zPow = Random.Range(0.4f, 3.0f);

        trans = transform;
        prevPos = trans.position;
        startPos = transform.position;
        time = (target - trans.position).magnitude / speed;
    }

    void Update() {

        Vector3 v3 = startPos;
        v3.x = Mathf.Lerp(v3.x, target.x, Mathf.Pow(timer / time, xPow));
        v3.y = Mathf.Lerp(v3.y, target.y, Mathf.Pow(timer / time, yPow));
        v3.z = Mathf.Lerp(v3.z, target.z, Mathf.Pow(timer / time, zPow));
        trans.position = v3;
        timer += Time.deltaTime;

        target = target_pos.position;

        if (trans.position != prevPos)
        {
            trans.rotation = Quaternion.LookRotation(trans.position - prevPos);
        }

        prevPos = trans.position;

        if (target != null) {

            if (timer > 1) {

                if ((target_pos.position - transform.position).magnitude > 50) {

                    randomOffset = 100.0f;
                    turnspeed = 90.0f;
                }else {

                    randomOffset = 5f;
                    if ((target_pos.position - transform.position).magnitude < 10) {

                        turnspeed = 180.0f;
                    }
                }
            }

            Vector3 direction = target_pos.position - transform.position + Random.insideUnitSphere * randomOffset;
            direction.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnspeed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (timer > time) {

            Destroy(transform.gameObject, 0.3f);
        }
    }
}