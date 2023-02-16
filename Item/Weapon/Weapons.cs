using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {

	[Header("타워 설정 부분")]
    public float range = 30f;
	private float checkAngle = 360f;
    public float BlockDamages = 1f;

    [Header("총알 설정 부분")]
    public float fireRate = 5f;
    public float fireCountdown = 0f;
    public float AttackDamages;
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
    //public GameObject AttackEffectPrefab;

    [Header("기타 설정 부분")]
	private string enemyTag = "Enemy";
	private Transform target;

    private Enemys targetEnemy;

    void Awake() {

        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start() {

		InvokeRepeating("UpdateTarget", 0f, 0.5f);

        laserCountdown = laserCount;
        laserCount = 0;
    }

	void UpdateTarget() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortesDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach(GameObject enemy in enemies) {

			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if(distanceToEnemy < shortesDistance) {

				shortesDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if(nearestEnemy != null && shortesDistance <= range) {

			target = nearestEnemy.transform;
            //targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}else {

			target = null;
		}
	}

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

        if (useLaser) {

            Laser();
        }else {

            Delay();
        }
    }

    void Delay() {

        if (fireCountdown <= 0f) {

            StartCoroutine(Shoot());
            fireCountdown = 1f / fireRate;
            //SoundController.Soundinstance.ShootSound();
        }

        fireCountdown -= Time.deltaTime;
    }

    IEnumerator Shoot() {

		//AudioManager.instance.PlaySound("Tower_0 Shoot Sound", transform.position);
        firePoint.position += Offset;
        firePoint.position += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
                                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.z));
        //AttackEffect();
        GameObject bulletShoot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullets bullet = bulletShoot.GetComponent<Bullets>();

        if (bullet != null) {

			bullet.Seek(target);
		}

		yield return null;
    }

    void Laser() {

        if (laserTakeDamage) {

            //targetEnemy.damage = laserDamage * Time.deltaTime;
            //targetEnemy.TakeDamage();
            //targetEnemy.Slow(slowAmount);

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

	void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
