using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class AnimationManager : MonoBehaviour {

    public Animator animator;

    public void StartAnimation() {

        animator.SetBool("IsStart", true);
    }

    public void StopAnimation() {

        animator.SetBool("IsStart", false);
    }
}
