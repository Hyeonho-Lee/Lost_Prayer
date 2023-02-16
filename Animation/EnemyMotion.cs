using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour {

	Animator animator;

	void Awake() {
		
		animator = GetComponent<Animator>();
	}

	public void Move() {

		animator.SetBool("isRunning", true);
	}

    public void Moves()
    {

        animator.SetBool("isRunning", false);
    }

    public void Attack() {

		animator.SetBool("isAttacking", true);
	}

    public void Attacks()
    {

        animator.SetBool("isAttacking", false);
    }
}
