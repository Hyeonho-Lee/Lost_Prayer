using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaidDialog : MonoBehaviour {

	public Animator animator;

    public void StartDialog() {

		StartCoroutine (StartFadeDialog ());
	}

	IEnumerator StartFadeDialog() {

        animator.SetBool("IsFade", true);
        yield return new WaitForSeconds(2f);
		animator.SetBool ("IsFade", false);
	}
}
