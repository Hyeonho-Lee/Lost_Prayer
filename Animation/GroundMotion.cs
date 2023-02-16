using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMotion : MonoBehaviour {


    private Animator animator;

    void Awake() {

        animator = GetComponent<Animator>();
    }

    public void ClickDown() {

        animator.SetBool("isAttacking", true);
    }

    public void ClickUp() {

        animator.SetBool("isAttacking", false);
    }
}
