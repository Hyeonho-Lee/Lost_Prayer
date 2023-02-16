using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaidButton : MonoBehaviour {

    public GameObject OpenUI;
    public float openDelay;

    void Start()
    {

        OpenButton();
    }

    public void OpenButton()
    {

        StartCoroutine(OpenUIs());
    }

    IEnumerator OpenUIs()
    {

        OpenUI.SetActive(false);
        yield return new WaitForSeconds(openDelay);
        OpenUI.SetActive(true);
    }
}
