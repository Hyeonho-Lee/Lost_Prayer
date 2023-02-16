using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour {

	[Header("타워 설정 부분")]
	// 범위를 지정
	public float range = 25f;
	// 회전 속도
	public float turnSpeed = 10f;
	// 연사속도
	public float fireRate = 5f;
	// 딜레이
	public float fireCountdown = 0f;

	[Header("기타 설정 부분")]
	// 적의 태그를 지정
	public string enemyTag = "Enemy";

	// 타겟 프리팹
	private Transform target;
	// 회전 부분 프리팹
	public Transform partToRotate;

	// 총알 프리팹
	public GameObject bulletPrefab;
	// 총알 생성위치
	public Transform firePoint;

	// 처음 시작할떄
	void Start() {
		
		// 타겟 바꾸는거를 관리
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	// 타겟 변화를 관리
	void UpdateTarget() {
		
		// 적을 배열로 지정
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		// 가까운적의 거리를 잼
		float shortesDistance = Mathf.Infinity;
		// 가까운적을 null로 지정
		GameObject nearestEnemy = null;

		// 이상한 반복문
		foreach(GameObject enemy in enemies) {
			
			// 적과 타워사이의 거리를 계산
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			// 만약에 거리안에 들어왓을떄
			if(distanceToEnemy < shortesDistance) {
				
				// 가까운적의 거리를 타워사이의 거리로 지정
				shortesDistance = distanceToEnemy;
				// 가까운적을 적으로 지정
				nearestEnemy = enemy;
			}
		}

		// 가까운적이 null이 아니거나 가까운거리가 범위보다 작을시
		if(nearestEnemy != null && shortesDistance <= range) {
			
			// 타겟을 가까운적으로 지정
			target = nearestEnemy.transform;
		}else {
			
			// 타겟을 null로 지정함
			target = null;
		}

		// 딜레이가 0보다 작을시
		if(fireCountdown <= 0f) {
			
			// 함수호출
			Shoot();
			// 딜레이를 연사속도만큼 나눔
			fireCountdown = 1f / fireRate;
		}

		// 딜레이를 감소시킴
		fireCountdown -= Time.deltaTime;
	}

	// 변화값
	void Update() {
		
		// 타겟이 없을시
		if(target == null) {
			
			// 값을 리턴함
			return;
		}

		// 타겟의 사이거리를 구함
		Vector3 dir = target.position - transform.position;
		// 백터로 원하는 축을 회전하게함
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		// 회전값을 지정
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		// 회전 시킴
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

	}

	// 타겟을 인식하는 범위를 표현
	void OnDrawGizmosSelected() {
		
		// 색을 빨강으로 지정
		Gizmos.color = Color.red;
		// 원으로 범위를 표시함
		Gizmos.DrawWireSphere(transform.position, range);
	}

	// 총알을 쏨
	void Shoot() {
		
		// 총알을 인스턴트로 생성
		GameObject bulletShoot = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		// 스크립트를 가져옴
		Bullet bullet = bulletShoot.GetComponent<Bullet>();

		// 스크립트가 null이 아니라면
		if(bullet != null) {
			
			// 적을 찾아 지정
			bullet.Seek(target);
		}
	}
}
