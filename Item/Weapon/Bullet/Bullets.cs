using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets: MonoBehaviour {
	
	[Header("총알 설정")]
	private Transform target;
    public float speed = 50f;
    public float explosionPower = 10f;
    public float explosionRange = 0f;
    public float explosionUpForce = 1f;
    // 총알 이팩트
    //public GameObject bulletEffect;
    public float AttackDamage = 1f;

    //private AutoTower autotower;

    // 적을 찾는 함수
    public void Seek(Transform _target) {
	
		// 적을 지정
		target = _target;
	}

    void Awake() {

       // autotower = GameObject.FindObjectOfType<AutoTower>();
    }

    void Start(){

        //AttackDamage = autotower.AttackDamages;
    }

    // 변화값
    void Update() {

        // 타겟이 null일시
        if (target == null) {

            // 오브젝트 삭제
            Destroy(gameObject);
            return;
        }

        StartCoroutine(Move());
    }

    IEnumerator Move() {

        // 방향값을 구함
        Vector3 dist = (target.position - transform.position).normalized;
        // 이동시킴
        transform.position = transform.position + (dist * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dist); 

        yield return null;
    }

    // 충돌 햇을시
    void OnTriggerEnter(Collider col) {

        // 만약 태그가 적일시
        if (col.CompareTag("Enemy")) {

            // 이펙트를 인스턴트로 생성
            //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            // 이펙트를 n초후 삭재함
            //Destroy(effectIns, 1f);
            //AttackDamage = autotower.AttackDamages;

            // 스크립트를 호출함
            Enemys enemys = col.GetComponent<Enemys>();

            // 적이 있다면
            if (enemys != null) {

                if(explosionRange > 0f) {

                    Explode();
                }else {

                    //enemys.damage = AttackDamage;
                    //enemys.TakeDamage();
					//Destroy(enemys.gameObject);
                }
            }

            // 블럭을 삭재함
            Destroy(this.gameObject);
        }
    }

    void Explode() {

        Vector3 explosionPostion = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);

        foreach(Collider collider in colliders) {

            if(collider.tag == "Enemy") {

                Enemy enemy = collider.GetComponent<Enemy>();
                Rigidbody rb = collider.GetComponent<Rigidbody>();

                if (enemy != null) {

                    enemy.damage = AttackDamage;
                    enemy.TakeDamage();
                    rb.AddExplosionForce(explosionPower, explosionPostion, explosionRange, explosionUpForce, ForceMode.Impulse);
                }
            }
        }
    }

    void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
