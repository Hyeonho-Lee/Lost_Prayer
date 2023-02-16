using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTower : MonoBehaviour {

	[Header("타워 설정 부분")]
    // 범위를 지정
    public float range = 30f;
    // 블럭데미지
    public float BlockDamages = 1f;

    [Header("총알 설정 부분")]
    // 연사속도
    public float fireRate = 5f;
    // 딜레이
    public float fireCountdown = 0f;
    // 총알데미지
    public float AttackDamages;
    // 총알 프리팹
    public GameObject bulletPrefab;

    [Header("레이저 설정 부분")]
    public bool useLaser = false;
    public bool laserTakeDamage = true;

    public float laserDamage = 1f;
    public float slowAmount = .5f;

    public float laserCount = 2f;
    private float laserCountdown;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("공격 위치 설정 부분")]
    public Transform firePoint;
    public Vector3 RandomizeIntensity = new Vector3(3.5f, 0, 0);
    public Vector3 Offset = new Vector3(0, 2, 0);

    [Header("이펙트 설정 부분")]
    public GameObject AttackEffectPrefab;

    [Header("기타 설정 부분")]
	// 적의 태그를 지정
	public string enemyTag = "Enemy";
	// 타겟 프리팹
	private Transform target;

    private StatManager statmanager;
    private Enemy targetEnemy;

    void Awake() {

        statmanager = GameObject.FindObjectOfType<StatManager>();
        lineRenderer = GetComponent<LineRenderer>();

		statmanager.Load();

		statmanager.tower_powers = AttackDamages;
		statmanager.tower_speeds = fireRate;
		statmanager.block_powers = BlockDamages;

		AttackDamages = statmanager.tower_powers;
		fireRate = statmanager.tower_speeds;
		BlockDamages = statmanager.block_powers;
		statmanager.Load();
		statmanager.Save();
    }

    // 처음 시작할떄
    void Start() {

		// 타겟 바꾸는거를 관리
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //StartCoroutine(Delay());

		statmanager.Load();

		AttackDamages = statmanager.tower_powers;
		fireRate = statmanager.tower_speeds;
		BlockDamages = statmanager.block_powers;

        laserCountdown = laserCount;
        laserCount = 0;
    }

	// 타겟 변화를 관리
	void UpdateTarget() {

        statmanager.tower_powers = AttackDamages;
        statmanager.tower_speeds = fireRate;
        statmanager.block_powers = BlockDamages;

        // 적을 배열로 지정
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		// 가까운적의 거리를 잼
		float shortesDistance = Mathf.Infinity;
		// 가까운적을 null로 지정
		GameObject nearestEnemy = null;

		// 적 탐색
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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}else {

			// 타겟을 null로 지정함
			target = null;
		}
	}

	// 변화값
	void Update() {

        if (target == null) {

            if(useLaser) {

                if(lineRenderer.enabled) {

                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }else {

                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }

            return;
        }

		// 레이저 활성화시
        if (useLaser) {

			// 함수 호출
            Laser();
        }else {

			// 딜레이
            Delay();
        }
    }

    // 타겟을 인식하는 범위를 표현
    void OnDrawGizmosSelected() {

        // 색을 빨강으로 지정
        Gizmos.color = Color.red;
        // 원으로 범위를 표시함
        Gizmos.DrawWireSphere(transform.position, range);
    }

	// 딜레이 함수
    void Delay() {

        // 딜레이가 0보다 작을시
        if (fireCountdown <= 0f) {

            // 함수호출
            StartCoroutine(Shoot());
            // 딜레이를 연사속도만큼 나눔
            fireCountdown = 1f / fireRate;
            // 발사소리 출력
            //SoundController.Soundinstance.ShootSound();
        }

        // 딜레이를 감소시킴
        fireCountdown -= Time.deltaTime;
    }

    // 총알을 쏨
    IEnumerator Shoot() {

		//AudioManager.instance.PlaySound("Tower_0 Shoot Sound", transform.position);
        firePoint.position += Offset;
        firePoint.position += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
                                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.z));
        //AttackEffect();
        // 총알을 인스턴트로 생성
        GameObject bulletShoot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // 스크립트를 가져옴
        Bullet bullet = bulletShoot.GetComponent<Bullet>();

        // 스크립트가 null이 아니라면
        if (bullet != null) {

			// 적을 찾아 지정
			bullet.Seek(target);
		}

		yield return null;
    }

	// 레이저 함수
    void Laser() {

        if (laserTakeDamage) {

            targetEnemy.damage = laserDamage * Time.deltaTime;
            targetEnemy.TakeDamage();
            targetEnemy.Slow(slowAmount);

            laserTakeDamage = false;
            laserCount = 0;
        }else {

            laserCount += Time.deltaTime;
        }

        if (laserCount > laserCountdown) {

            laserTakeDamage = true;
        }

        if (!lineRenderer.enabled) {

            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);

        if(target != null) {

            lineRenderer.SetPosition(1, target.position);
        }else {

            lineRenderer.SetPosition(1, firePoint.position);
        }

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(-dir);
    }

    void AttackEffect() {

        // 이펙트를 인스턴트로 생성
        GameObject effectlns = (GameObject)Instantiate(AttackEffectPrefab, firePoint.position, transform.rotation);
        // n초뒤 이펙트 삭제
        Destroy(effectlns, 1f);
    }
}
