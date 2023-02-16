using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class TimeLineManager : MonoBehaviour {

	public TimeLineSetting[] timelineSetting;
	TimeLineSetting timelineSet;

    public Image skillFilter;

    private float currentCoolTime;
    public bool canSkill = true;

    public int timelineNumber;
	public event System.Action<int> OnNewTimeLine;

    public string NextSceneName;

    private DialogManager dialogmanager;

	[System.Serializable]
	public class TimeLineSetting {

		public PlayableDirector playable;
		public TimelineAsset timeline;
        public float coolTime;
        public bool AutoNext;
		public float AutoTime;
	}

	void Start() {

		NextTimeline ();
        skillFilter.fillAmount = 1;

        dialogmanager = GameObject.FindObjectOfType<DialogManager>();
        //dialogmanager.StartDialog(dialog);
    }

	public void NextTimeline() {

        if (canSkill) {

            //Debug.Log("사용중");
            StartCoroutine(NextTimelines());

            skillFilter.fillAmount = 0;
            StartCoroutine(CoolTime());

            currentCoolTime = timelineSet.coolTime;
            StartCoroutine(CoolTimeCounter());

            canSkill = false;
        }else {

            //Debug.Log("아직은 못씀");
        }
    }

    public void OnlyCooltime() {

        if (canSkill) {

            //Debug.Log("사용중");
            //StartCoroutine(NextTimelines());

            skillFilter.fillAmount = 0;
            StartCoroutine(CoolTime());

            currentCoolTime = 2f;
            StartCoroutine(CoolTimeCounter());

            canSkill = false;
        }else {

            //Debug.Log("아직은 못씀");
        }
    }

    IEnumerator NextTimelines() {

        timelineNumber++;

        if (timelineNumber - 1 < timelineSetting.Length) {

            timelineSet = timelineSetting[timelineNumber - 1];

            if (timelineSet.AutoNext) {

                StartCoroutine(AutoPlayTimeLine());
            }else {

                StartCoroutine(PlayTimeLine());
            }

            if (OnNewTimeLine != null) {

                OnNewTimeLine(timelineNumber);
            }
        }

        if (timelineNumber - 1 == timelineSetting.Length) {

            StartCoroutine(NextScene());
        }

        yield return null;
    }

    IEnumerator AutoPlayTimeLine() {

		timelineSet.playable.Play (timelineSet.timeline);
		timelineSet.AutoNext = false;

		yield return new WaitForSeconds(timelineSet.AutoTime);
		NextTimeline ();
    }

    IEnumerator CoolTime() {

        while (skillFilter.fillAmount < 1) {

            skillFilter.fillAmount += 1 * Time.smoothDeltaTime / timelineSet.coolTime;

            yield return null;
        }

        canSkill = true;

        yield break;
    }

    IEnumerator CoolTimeCounter() {

        while (currentCoolTime > 0) {

            yield return new WaitForSeconds(1f);

            currentCoolTime -= 1f;
        }

        yield break;
    }

    IEnumerator PlayTimeLine() {

		timelineSet.playable.Play (timelineSet.timeline);

		yield break;
	}

    IEnumerator NextScene() {

        yield return new WaitForSeconds(2f);
        LoadingSceneManager.LoadScene(NextSceneName);
    }

    public void TimeLineSkip() {

        LoadingSceneManager.LoadScene(NextSceneName);
    }
}
