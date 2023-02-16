using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFirst : MonoBehaviour {

	void Start() {

        StartCoroutine(StartDialogs());
	}

    IEnumerator StartDialogs() {

        yield return new WaitForSeconds(0.2f);

        FindObjectOfType<DialogTrigger>().TriggerDialog();
    }
}
