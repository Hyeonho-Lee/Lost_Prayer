using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveClearPanel : MonoBehaviour {

    public float speed = 1.5f;
    public RectTransform newWaveBanner;
    public Text InfoText;
    public string ItemInfoText; 

    private Spawner spawner;

    void Awake(){

        spawner = GameObject.FindObjectOfType<Spawner>();
    }

    public void OnNewWave(){

        InfoText.text = "남은 적수: " + spawner.NextEnemyCount;

        StopCoroutine("AnimateNewWaveBanner");
        StartCoroutine("AnimateNewWaveBanner");
    }

	public void WaveClear(){

		InfoText.text = "웨이브 클리어!";

		StopCoroutine("AnimateNewWaveBanner");
		StartCoroutine("AnimateNewWaveBanner");
	}

	public void AllWaveClear(){

		InfoText.text = "웨이브 클리어! 10초뒤 결과창으로 이동합니다.";

		StopCoroutine("AnimateNewWaveBanner");
		StartCoroutine("AnimateNewWaveBanner");
	}

    public void ItemDropInfoPanel() {

        InfoText.text = ItemInfoText;

        StopCoroutine("AnimateNewWaveBanner");
        StartCoroutine("AnimateNewWaveBanner");
    }

    // 배너 효과 코루틴
    IEnumerator AnimateNewWaveBanner() {

        // 기다리는 시간 지정
        float delayTime = 1.5f;
        // 퍼센트 지정
        float animatePercent = 0;
        // 원래값
        int dir = 1;

        // 모션후 기다리는 시간
        float endDelayTime = Time.time + 1 / speed + delayTime;

        // 만약 퍼센트가 0보다 크거나 같으면 반복
        while (animatePercent >= 0) {

            // 퍼센트를 시간*속도*원래값만큼 더함
            animatePercent += Time.deltaTime * speed * dir;

            // 만약 퍼센트가 1보다 크거나 같으면
            if (animatePercent >= 1) {

                // 퍼센트를 1로 지정
                animatePercent = 1;
                // 시간이 모션후 기다리는 시간보다 클시
                if (Time.time > endDelayTime)
                {

                    // 원래값을 -1로 지정
                    dir = -1;
                }
            }

            // 배너의 위치를 바꿈
            newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-660, -460, animatePercent);
            // 무한반복 방지
            yield return null;
        }
    }
}
