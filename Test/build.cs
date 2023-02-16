using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour {

    public static build instance;

    public GameObject standardWallPrefab;
    public GameObject anotherWallPrefab;
    private GameObject wallToBuild;

    private node selectNode;

    void Awake() {

        if(instance != null) {

            Debug.Log("not build script");
        }

        instance = this;
    }

    public GameObject GetWallToBuild() {

        return wallToBuild;
    }

    public void SetWallToBuild(GameObject wall) {

        wallToBuild = wall;
    }

    public void SelectNode(node node) {

        selectNode = node;
    }
}
