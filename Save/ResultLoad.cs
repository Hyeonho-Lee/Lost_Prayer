using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLoad : MonoBehaviour {

    private StatManager statmanager;
    public Text scoretinfo;

	void Start () {

        statmanager = GameObject.FindObjectOfType<StatManager>();
        statmanager.Load();

		scoretinfo.text = statmanager.scores.ToString();
    }
}
