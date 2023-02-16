using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class TowerTableRead : MonoBehaviour {

    public TextAsset jsonData;

    public int ItemIndex;
    public GameObject imageLocal;

    public Text ItemLabel;
    public Text ItemText;
    public Sprite ItemImage;

    public void ItemInfo() {

        int i = 0;
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

        int index;
        int id_number;
        string name;
        float percentage;
        string rating;
        string info;
        float coin;
        float hp;
        float atk_power;
        float atk_speed;

        for (i = 0; i < getData["TowerTable"].Count; i++) {

            index = int.Parse(getData["TowerTable"][i]["Index"].ToString());
            id_number = int.Parse(getData["TowerTable"][i]["Index"].ToString());
            name = getData["TowerTable"][i]["Index"].ToString();
            percentage = float.Parse(getData["TowerTable"][i]["Index"].ToString());
            rating = getData["TowerTable"][i]["Index"].ToString();
            info = getData["TowerTable"][i]["Index"].ToString();
            coin = float.Parse(getData["TowerTable"][i]["Index"].ToString());
            hp = float.Parse(getData["TowerTable"][i]["Index"].ToString());
            atk_power = float.Parse(getData["TowerTable"][i]["Index"].ToString());
            atk_speed = float.Parse(getData["TowerTable"][i]["Index"].ToString());

            if (index.Equals(ItemIndex)) {

                ItemLabel.text = getData["TowerTable"][ItemIndex]["Name"].ToString();
                ItemText.text = getData["TowerTable"][ItemIndex]["Info"].ToString();
                imageLocal.GetComponent<Image>().sprite = ItemImage;
            }
        }
    }
}