using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public Text nameText;
	public GameObject FadeOther;

	public Text dialogText;
	public float dialogTextSpeed = 0.035f;

	public Animator animator;

	private Queue<string> names;
	private Queue<string> sentences;

    TimeLineManager timelinemanager;

	// 처음 시작시
	void Start() {

		names = new Queue<string> ();
		sentences = new Queue<string>();

        timelinemanager = GameObject.FindObjectOfType<TimeLineManager>();
    }
		
	public void StartDialog(Dialog dialog) {

		animator.SetBool ("IsOpen", true);

		//nameText.text = dialog.names;

		names.Clear ();
		foreach (string name in dialog.names) {

			names.Enqueue(name);
		}

		sentences.Clear ();
		foreach (string sentence in dialog.sentences) {

			sentences.Enqueue(sentence);
		}

		// 다음 대화 함수
		DisplayNextSentence ();
	}

	public void DisplayNextSentence() {

        if(timelinemanager.canSkill) {

            if (sentences.Count == 0 && names.Count == 0) {

                EndDialog();
                StartCoroutine(FadeOthers());
                return;
            }

            string name = names.Dequeue();
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeName(name));
            StartCoroutine(TypeSentence(sentence));
        }
	}

	IEnumerator TypeName (string name) {

		nameText.text = "";

		foreach (char nameletter in name.ToCharArray()) {

			nameText.text += nameletter;
			yield return null;
		}
	}

	IEnumerator TypeSentence (string sentence) {

		dialogText.text = "";

		foreach (char dialogletter in sentence.ToCharArray()) {

			dialogText.text += dialogletter;
			yield return new WaitForSeconds (dialogTextSpeed);
		}
	}

	void EndDialog() {

		animator.SetBool ("IsOpen", false);
	}

	IEnumerator FadeOthers() {

        FindObjectOfType<FaidInOut>().StartPlayFadeOut();
        yield return new WaitForSeconds (2f);
        FadeOther.gameObject.SetActive(false);
	}
}