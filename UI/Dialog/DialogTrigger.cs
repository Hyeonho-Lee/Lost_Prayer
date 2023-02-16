using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	// 대화 불러오기
	public Dialog dialog;

	// 대화 트리거 함수
	public void TriggerDialog() {

		FindObjectOfType<DialogManager> ().StartDialog (dialog);
	}
}
