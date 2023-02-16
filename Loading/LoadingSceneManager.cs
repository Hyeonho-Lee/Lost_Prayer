using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    public static string nextScene;
    public string loadScene;

    public Text InputText;
    [TextArea(3,10)]
    public string Texts;

    private float timer = 0f;

    [SerializeField]
    public Image progressBar;

    void Start() {

        StartCoroutine(LoadScene());
        InputText.text = Texts;
    }

    IEnumerator LoadScene() {

        // 화면을 불러옴
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene);
        // 로딩이 완료된 화면을 즉시 보여주는것을 비활성화
        ao.allowSceneActivation = false;

        while(!ao.isDone) {

            timer += Time.deltaTime / 5;

            if (ao.progress >= 0.9f) {

                // 로딩바 이미지의 양을 입력해둠
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if(progressBar.fillAmount == 1.0f) {

                    ao.allowSceneActivation = true;
                    LoadScene(loadScene);
                }
            }else {

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, ao.progress, timer);
                //timer += Time.deltaTime / 10;

                if (progressBar.fillAmount >= ao.progress) {

                    timer = 0f;
                }
            }

            yield return null;
        }

        yield return null;
    }

    public static void LoadScene(string sceneName) {

        nextScene = sceneName;
        SceneManager.LoadScene(nextScene);
    }
}
