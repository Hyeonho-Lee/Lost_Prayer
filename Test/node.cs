using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class node : MonoBehaviour {

    private GameObject wall;

    public Vector3 positionOffset;

    public Color hoverColor;
    private Color startColor;

   // public Vector3 positionOffset;

    private Renderer rend;
    build build;

    void Start() {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        build = build.instance;
    }

    void OnMouseDown() {

        if (EventSystem.current.IsPointerOverGameObject()) {

            return;
        }

        if (build.GetWallToBuild() == null) {

            return;
        }
        
        if(wall != null) {

            Debug.Log("this node can't build wall");
            return;
        }

        GameObject wallToBuild = build.GetWallToBuild();
        wall = (GameObject)Instantiate(wallToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter() {

        if(EventSystem.current.IsPointerOverGameObject()) {

            return;
        }

        if (build.GetWallToBuild() == null) {

            return;
        }

        rend.material.color = hoverColor;
    }

    void OnMouseExit() {

        rend.material.color = startColor;
    }
}
