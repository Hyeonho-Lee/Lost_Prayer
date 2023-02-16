using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class SelectManager : MonoBehaviour {

    public GameObject SelectEffect;
    public Animator animator;

    public void ChangePosition() {

        SelectEffect.transform.position = this.gameObject.transform.position;
        StartAnimation();
    }

    public void StartAnimation() {

        StopAllCoroutines();
        animator.SetBool("IsStart", true);
        StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation() {

        yield return new WaitForSeconds(1f);
        animator.SetBool("IsStart", false);
    }
}
