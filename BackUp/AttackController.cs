using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour {

    public GameObject attackline;
    public int spawnindex;

    private AttackDelay attackdelay;
    private Animator animator;

    void Awake() {

        attackdelay = GameObject.FindObjectOfType<AttackDelay>();
        animator = GetComponent<Animator>();
    }

    void OnMouseDown() {

        transform.position += new Vector3(0, 0.1f, 0);
        animator.SetBool("isAttacking", true);
    }

    void OnMouseUp() {

        transform.position -= new Vector3(0, 0.1f, 0);
        animator.SetBool("isAttacking", false);
        LineAttack();
    }

    // 라인어택
    public void LineAttack() {

        if (attackdelay.Attackbool == true) {

            Instantiate(attackline, Ground.GroundArrays[spawnindex].position, this.transform.rotation);
            attackdelay.AttackTime = 0;
            attackdelay.Attackbool = false;
        }
    }
}
