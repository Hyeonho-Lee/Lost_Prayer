using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOption : MonoBehaviour {

    public GameObject UIPanel;

    public string nextSceneName;
    public float DelayTime = 1f;

    private LoadingSceneManager loadscenemanager;

    void Start() {

        loadscenemanager = GameObject.FindObjectOfType<LoadingSceneManager>();
    }

    public void DelayOpenPanle() {

        StopAllCoroutines();
        StartCoroutine(DelayOpen());
    }

    public void DelayClosePanle() {

        StopAllCoroutines();
        StartCoroutine(DelayClose());
    }

    IEnumerator DelayOpen() {

        yield return new WaitForSeconds(DelayTime);
        UIPanel.SetActive(true);
    }

    IEnumerator DelayClose() {

        yield return new WaitForSeconds(DelayTime);
        UIPanel.SetActive(false);
    }

    public void StartGame() {

        StopAllCoroutines();
        StartCoroutine(NextScene());
    }

    public void ExitGame() {

        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator NextScene() {

        yield return new WaitForSeconds(DelayTime);
        LoadingSceneManager.LoadScene(nextSceneName);
    }
}
