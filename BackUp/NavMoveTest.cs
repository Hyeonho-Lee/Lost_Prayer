using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class NavMoveTest : MonoBehaviour {

    NavMeshAgent pathfinder;
    private Transform target;
    private Transform enemytarget;

    [Header("캐릭터 설정")]
    public float SwordDamage = 1f;
    public float range = 3f;
    public float startSpeed = 5f;
    public float distance;

    public string enemyTag = "Enemy";
    public bool isAttacking = false;

    [Header("채력및 코인 설정 부분")]
    public float startHealth = 100f;
    public float health;
    public float EnemyAttackDamage;

    [Header("UI 설정")]
    public Image healthBar;
    public Text healthText;
    public Text maxText;

    private Spawner spawner;
    private EnemyMotion enemymotion;
    private Enemy enemy;

	// 처음 시작시
    void Start () {

        pathfinder = GetComponent<NavMeshAgent>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        enemymotion = GameObject.FindObjectOfType<EnemyMotion>();
        enemy = GameObject.FindObjectOfType<Enemy>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        target = null;

        pathfinder.speed = startSpeed;
        health = startHealth;

        maxText.text = Mathf.Round(startHealth).ToString();
        healthText.text = Mathf.Round(health).ToString();
    }

	// 가까운 타겟 변경
    void UpdateTarget() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance) {

                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortesDistance <= range) {

            enemytarget = nearestEnemy.transform;
            isAttacking = true;
            enemymotion.Attack();
        }else {

            enemytarget = null;
            isAttacking = false;
            enemymotion.Attacks();
        }

        if (target == null) {

            pathfinder.speed = 0;
            enemymotion.Moves();
            enemymotion.Attacks();
        }
    }

    void Update () {

        Check();
    }

    void Check() {

        if (spawner.waveNowing != false) {

            pathfinder.speed = startSpeed;
            StartCoroutine(UpdatePath());
            StartCoroutine(Delay());
        }else{

            StopCoroutine(Delay());
            StopCoroutine(UpdatePath());
            target = null;
        }

		// 공격이 참이거나 거리가 가까울시
        if (isAttacking == true || distance <= range - .5f) {

            pathfinder.speed = 0;
            pathfinder.SetDestination(this.transform.position);
        }else {

            pathfinder.speed = startSpeed;
        }
    }

    IEnumerator UpdatePath() {

        float delay = .25f;

        while (target != null) {

            Vector3 targetPos = new Vector3(target.position.x, 0.5f, target.position.z);

            distance = Vector3.Distance(target.transform.position, this.transform.position);

            //pathfinder.speed = 5f;
            enemymotion.Move();
            pathfinder.SetDestination(targetPos);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Delay() {

        float delay = 1f;

        yield return new WaitForSeconds(delay);

        if (spawner.waveNowing != false) {

            target = GameObject.FindGameObjectWithTag(enemyTag).transform;
        }
        else {

            target = null;
        }
    }

    void OnDrawGizmosSelected() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

	// 적과 충돌했을시
    void OnTriggerEnter(Collider col) {

        if (col.CompareTag(enemyTag)) {

            // 스크립트를 호출함
            Enemy enemy = col.GetComponent<Enemy>();

            // 적이 있다면
            if (enemy != null) {

                // 데미지를 입힘
                enemy.damage = SwordDamage;
                enemy.TakeDamage();
            }
        }
    }

    // 데미지를 받았을떄
    public void TakeDamage() {

        // 채력을 깍음
        health -= EnemyAttackDamage;

        // 채력바를 채력으로 지정
        healthBar.fillAmount = health / startHealth;
        healthText.text = Mathf.Round(health).ToString();

        // 만약 채력이 0 미만일시 삭제
        if (health <= 0f) {

            //오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
