using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHierarchy : MonoBehaviour {

    public GameObject Sword;
    public GameObject Magic;

    public void SwordChangSkill() {

        Sword.transform.SetAsLastSibling();
        Magic.transform.SetAsFirstSibling();
    }

    public void MagicChangSkill() {

        Magic.transform.SetAsLastSibling();
        Sword.transform.SetAsFirstSibling();
    }
}
