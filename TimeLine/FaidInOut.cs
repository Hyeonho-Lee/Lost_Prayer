using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaidInOut : MonoBehaviour {

	public float animTime;

	public Image fadeImage;

	private float start = 0f;
	private float end = 0f;
	private float time = 0f;

	private bool isPlaying = false;

	void Awake() {

		fadeImage = GetComponent<Image> ();
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
        this.transform.SetAsLastSibling();

		Color color = fadeImage.color;
		color.a = Mathf.Lerp (start, end, time);

		while (color.a < 1f) {

			time += Time.deltaTime / animTime;
			color.a = Mathf.Lerp (start, end, time);
			fadeImage.color = color;

			yield return null;
		}

		isPlaying = false;
	}

	IEnumerator PlayFadeIn() {

		yield return new WaitForSeconds (1f);

		start = 1f;
		end = 0f;
		time = 0f;

		isPlaying = true;
        this.transform.SetAsLastSibling();

        Color color = fadeImage.color;
		time = 0f;
		color.a = Mathf.Lerp (start, end, time);

		while (color.a > 0f) {

			time += Time.deltaTime / animTime;
			color.a = Mathf.Lerp (start, end, time);
			fadeImage.color = color;

			yield return null;
		}

		isPlaying = false;
	}

	public void StartPlayFadeOut() {

		StartCoroutine (PlayFadeOut ());
	}
}
