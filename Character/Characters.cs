using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Characters : MonoBehaviour {
	
	public NavMeshAgent agent;
	public Animator animator;

	//public GameObject weapon;
	public Transform target;
	public bool isAttacking = false;

	public float startSpeed = 15f;
	private float speed;

	public float targetArea = 20f;
	private float checkAngle = 360f;
	private string enemyTag = "Enemy";

	void Start() {
		
		target = null;
		speed = startSpeed;
	}

	void Update() {
		
		CheckTarget();
		UpdateTarget();
	}

	void CheckTarget() {
	
		if (isAttacking == true) {

            startSpeed = 0;
			agent.speed = startSpeed;
			transform.LookAt(target);
			animator.SetBool("isRunning", false);
        }else {

            startSpeed = speed;
			agent.speed = startSpeed;
        }
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

        if (nearestEnemy != null && shortesDistance <= targetArea) {

            target = nearestEnemy.transform;
            isAttacking = true;
        }else {

            target = null;
			isAttacking = false;
        }
    }
}
