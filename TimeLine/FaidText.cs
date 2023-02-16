using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaidText : MonoBehaviour {

	public float OutanimTime;
	public float InanimTime;

	public Text fadeText;

	private float start = 0f;
	private float end = 0f;
	private float time = 0f;

	private bool isPlaying = false;

	void Awake() {

		fadeText = GetComponent<Text> ();
	}

	void Start() {

		StartCoroutine (StartText ());
	}

	public void StartFadeOutAnim() {

		if (isPlaying == true) {

			return;
		}

		StartCoroutine (PlayFadeOut ());
	}

	public void StartFadeInAnim() {

		if (isPlaying == true) {

			return;
		}

		StartCoroutine (PlayFadeIn ());
	}

	IEnumerator PlayFadeOut() {

		start = 0f;
		end = 1f;
		time = 0f;

		isPlaying = true;

		Color color = fadeText.color;
		color.a = Mathf.Lerp (start, end, time);

		while (color.a < 1f) {

			time += Time.deltaTime / OutanimTime;
			color.a = Mathf.Lerp (start, end, time);
			fadeText.color = color;

			yield return null;
		}

		isPlaying = false;
	}

	IEnumerator PlayFadeIn() {

		start = 1f;
		end = 0f;
		time = 0f;

		isPlaying = true;

		Color color = fadeText.color;
		color.a = Mathf.Lerp (start, end, time);

		while (color.a > 0f) {

			time += Time.deltaTime / InanimTime;
			color.a = Mathf.Lerp (start, end, time);
			fadeText.color = color;

			yield return null;
		}

		isPlaying = false;
	}

	IEnumerator StartText() {

		StartCoroutine (PlayFadeOut ());
		yield return new WaitForSeconds(2f);
		StartCoroutine (PlayFadeIn ());
		yield return new WaitForSeconds(2f);
		FindObjectOfType<DialogTrigger> ().TriggerDialog();
	}
}
