using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemys : MonoBehaviour {
	
	public NavMeshAgent agent;
	public Animator animator;
	public Transform nearTarget;
	private Transform target;

	public float startSpeed = 15f;
	private float speed;

	public bool isAttacking = false;

	private float targetArea = 20f;
	private float checkAngle = 360f;
	private bool isTarget = false;
	private string enemyTag = "Player";

	void Start() {
		
		target = GameObject.FindGameObjectWithTag(enemyTag).transform;
		speed = startSpeed;

		InvokeRepeating("Check", 0f, 0.5f);
	}

	void Check(){
		
		CheckTarget();
		UpdateTarget();
		MoveTarget();
    }

	void CheckTarget() {
	
		if (isAttacking == true) {

            startSpeed = 0;
			//animator.SetBool("isAttacking", true);
        }else {

            startSpeed = speed;
			//animator.SetBool("isAttacking", false);
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

        if (nearestEnemy != null && shortesDistance <= targetArea/3) {

            nearTarget = nearestEnemy.transform;
            isAttacking = true;
        }else {

            nearTarget = null;
			isAttacking = false;
        }
    }

	void MoveTarget() {
		
		agent.speed = startSpeed;
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, targetArea);

        foreach (Collider hit in colliders) {

            if (hit.CompareTag(enemyTag) && isTarget == false) {

                target = hit.transform;
				isTarget = true;
            }
        }

		agent.SetDestination(target.position);
	}

	void OnDrawGizmos() {
		
		Gizmos.color = Color.red;

        var dt = Mathf.PI / 12f;
        var checkRadian = checkAngle * Mathf.Deg2Rad;

        for (float t = -checkAngle / 2f; t < checkRadian / 2f; t += dt) {

            Gizmos.DrawLine(GetPosByAngle(t), GetPosByAngle(Mathf.Min(t + dt, checkRadian / 2f)));
        }
    }

	 public Vector3 GetPosByAngle(float radian) {

        return transform.position + targetArea / 3 * transform.forward * Mathf.Cos(radian) + targetArea / 3 * transform.right * Mathf.Sin(radian);
    }
}
