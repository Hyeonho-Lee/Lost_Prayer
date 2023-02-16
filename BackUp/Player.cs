using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /*public float minspeed = 9f;
    public float maxspeed = 18f;
    private float speed;

    public float jumpPower = 10f;
    public float rotateSpeed = 3f;

    private Transform target;
    private Transform enemytarget;
    public float range = 30f;
    public string enemyTag = "Enemy";
    bool isAttacking = false;

    Rigidbody rigidbody;

    Vector3 movement;
    float horizontalMove;
    float verticalMove;

    private EnemyMotion enemymotion;

    void Awake() {

        rigidbody = GetComponent<Rigidbody>();
        enemymotion = GetComponent<EnemyMotion>();

        speed = minspeed;
    }

    void Start() {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        target = null;
    }

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
            enemymotion.Attacks();
        }

        if(target == null) {

            speed = 0;
            enemymotion.Moves();
            enemymotion.Attacks();
        }
    }

    void Update() {

        Move();
        Check();
    }

    void Move() {

        Vector3 dist;

        if (target == null) {

            enemymotion.Moves();
        }else{

            dist = (target.position - transform.position).normalized;
            transform.position = transform.position + (dist * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(dist);

            enemymotion.Move();
        }
    }

    void Check() {

        if (isAttacking == true) {

            speed = 0;
        }else {

            isAttacking = false;
            speed = minspeed;
        }
    }

    void OnTriggerEnter(Collider col) {

        if (col.CompareTag("Enemy")) {

            //key.EnemyAttackDamage = AttackDamage;
            //key.TakeDamage();
            Debug.Log("damage");
        }
    }

    void OnDrawGizmosSelected() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/
}
