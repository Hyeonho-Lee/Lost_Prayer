using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class CameraMoveCharacter : MonoBehaviour {

    public NavMeshAgent agent;
    public Animator animator;

    public Transform Target;
    public float CheckArea = 1f;
    private float distance;

    private int PlayerCount = 0;

    void Update() {

        distance = Vector3.Distance(Target.position, this.transform.position);
        CheckPlayer();
    }

    void CheckPlayer() {

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, CheckArea);

        foreach (Collider hit in colliders) {

            if (hit.tag == "Player") {

                PlayerCount++;
            }
        }

        if (PlayerCount >= 2) {

            agent.SetDestination(this.transform.position);
            animator.SetBool("isRunning", false);
            PlayerCount = 0;
        }
        else {

            agent.SetDestination(Target.position);
            animator.SetBool("isRunning", true);
            PlayerCount = 0;
        }
    }

    void OnDrawGizmosSelected() {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, CheckArea);
    }
}
