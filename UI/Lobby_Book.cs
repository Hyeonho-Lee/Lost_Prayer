using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_Book : MonoBehaviour {

    public GameObject openUi;
    public GameObject unObjActive;

	public GameObject ItemInfoopenUi;

    void OnMouseDown() {

        openUi.SetActive(true);
        unObjActive.SetActive(false);
    }

	public void ItemInfoUIOpen() {

		ItemInfoopenUi.SetActive(true);
	}
}
