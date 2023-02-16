using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSave : MonoBehaviour {

    private StatManager statmanager;

    void Start() {

        statmanager = GameObject.FindObjectOfType<StatManager>();
		statmanager.Load();
        statmanager.ScoreReset();
		statmanager.Save ();
    }
}
