using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testshop : MonoBehaviour {

    build build;

    void Start(){

        build = build.instance;
    }

    public void Wall_0() {

        Debug.Log("0 wall hi");
        build.SetWallToBuild(build.standardWallPrefab);
    }
    public void Wall_1() {

        Debug.Log("1 wall hi");
        build.SetWallToBuild(build.anotherWallPrefab);
    }
}
