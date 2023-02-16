using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCube : MonoBehaviour {
	
	[Header("블럭 설정")]
	// 점프파워
    public float jumpPower = 15f;
	// 생존시간
    public float lifetime = 6f;
	// 데미지
	public float AttackDamage = 2f;

	// 이펙트
	public GameObject impactEffect;

    private AutoTower autotower;


    void Awake() {

        autotower = GameObject.Find("autoTower").GetComponent<AutoTower>();
    }

    // 처음시작시
    void Start () {

        // 중력을 위로 띄움
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
		// 일정시간뒤 삭제함
        Destroy(this.gameObject, lifetime);
        // 이펙트를 인스턴트로 생성
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // 이펙트를 n초후 삭재함
        //Destroy(effectIns, 2f);
    }

	// 충돌 햇을시
	void OnTriggerEnter(Collider col) {
		
		// 만약 태그가 적일시
		if(col.CompareTag("Enemy")) {

            AttackDamage = autotower.BlockDamages;

            // 스크립트를 호출함
            Enemy enemy = col.GetComponent<Enemy>();

	        // 적이 있다면
		    if(enemy != null) {

                // 데미지를 입힘
                enemy.damage = AttackDamage;
			    enemy.TakeDamage();
			    // 적을 띄움
			    enemy.TakeDamageCube();
		    }
			// 블럭을 삭재함
			//Destroy(this.gameObject);
		}
	}
}
