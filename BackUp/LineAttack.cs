using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineAttack : MonoBehaviour {

    public float speed = 15f;
    public Transform attackBlock;

    public Transform target;

    private int wavepointIndex;
    public int firstindex;
    public int endindex;

    void Start() {

        target = Ground.GroundArrays[firstindex];
		wavepointIndex = firstindex;
	}

    void Update() {

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {

        // 방향값을 구함
        Vector3 dist = (target.position - transform.position).normalized;
        // 이동시킴
        transform.position = transform.position + (dist * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) {
            //다음 포인터로 이동
            GetNextWaypoint();
            //함수 호출
            Attack();
        }

        yield return null;
    }

    void Attack(){

        Instantiate(attackBlock, this.transform.position, this.transform.rotation); //블럭을 인스턴트 하여 호출
    }

    void GetNextWaypoint() {

		wavepointIndex++;
        target = Ground.GroundArrays[wavepointIndex];

        if (wavepointIndex >= endindex) {

            //오브젝트 삭제
            Destroy(gameObject);
            return;
        }
    }
}

