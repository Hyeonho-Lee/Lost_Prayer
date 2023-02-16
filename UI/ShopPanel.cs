using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour {

    public float fadeTime = 1f;
    private Image shopBackground;

    private float start = 0f;
    private float end = 1f;
    private float time = 0f;

    private bool isFadeing = false;

    void Awake() {

        shopBackground = GetComponent<Image>();
    }

	// 창을 염
	public void Open() {

        // 활성화함
        gameObject.SetActive(true);

        if(isFadeing == true) {

            return;
        }

        // 코루틴 실행
        StartCoroutine(FadeIn());
    }

	// 창을 닫음
	public void Close() {

        if (isFadeing == true) {

            return;
        }

        // 코루틴 실행
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn() {

        isFadeing = true;

        Color color = shopBackground.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a < 1f) {

            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(start, end, time);
            shopBackground.color = color;
            yield return null;
        }

        isFadeing = false;
    }

    IEnumerator FadeOut() {

        isFadeing = true;

        Color color = shopBackground.color;
        time = 1f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a > 0f) {

            time -= Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(start, end, time);
            shopBackground.color = color;
            yield return null;
        }

        // 비활성화함
        isFadeing = false;
        gameObject.SetActive(false);
    }

    void RandomIndex() {



    }
}
