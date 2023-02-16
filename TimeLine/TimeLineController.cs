using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineController : MonoBehaviour {

    public PlayableDirector playable;
    public TimelineAsset timeline;

    public PlayableDirector nextPlayable;
    public TimelineAsset nextTimeline;

    public float DelayTimeLine = 1f;
    public float DelayTimes = 0f;
    public bool IsAuto = false;

    public void StartTimeLine() {

        if(!IsAuto) {

            StopAllCoroutines();
            StartCoroutine(StartTimeLines());
        }else {

            StopAllCoroutines();
            StartCoroutine(Delays());
        }
    }

    IEnumerator StartTimeLines() {

        yield return new WaitForSeconds(DelayTimeLine);
        playable.Play(timeline);
    }

    IEnumerator Delays() {

        StartCoroutine(StartTimeLines());
        yield return new WaitForSeconds(DelayTimes);
        nextPlayable.Play(nextTimeline);
    }

    public void StopTimeLine() {

        playable.Stop();
    }
}
