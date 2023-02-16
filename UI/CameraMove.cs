using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject LogoPanel;

    /*public Transform target;
    public GameObject UIPanel;
    public GameObject Object;
    public bool anyKey = false;

    Vector3 tpos = new Vector3(53.5f, 25.5f, -52.5f);
    Vector3 pos = new Vector3(102.9f, 130.4f, -92.3f);

    void Awake(){

        target.transform.position = Vector3.Lerp(target.transform.position, pos, Time.deltaTime);
        target.transform.rotation = Quaternion.Euler(40f, 315f, 2.229046f);
    }*/

    void Start(){

        LogoPanel.SetActive(true);
       // UIPanel.SetActive(false);
        //Object.SetActive(false);
    }

    void Update(){


        if (Input.anyKeyDown) {

            LogoPanel.SetActive(false);
            //UIPanel.SetActive(true);
            //Object.SetActive(true);
            //anyKey = true;
            //Destroy(this.gameObject, 5f);
        }

        /*if(anyKey.Equals(true)){

            Move();
        }*/
    }

   /* void Move(){

        target.transform.position = Vector3.Lerp(target.transform.position, tpos, Time.deltaTime);
        target.transform.rotation = Quaternion.Euler(11.84675f, 322.5476f, 0.0809903f);
    }*/
}
