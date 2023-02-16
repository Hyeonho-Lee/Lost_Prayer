using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class PlayerDataRead : MonoBehaviour {

    public TextAsset jsonData;

    public int index;
    public int id_number;
    public string name;
    public string info;
    public int lv;
    public float percentage;
    public float health;
    public float block_power;
    public float block_speed;
    public float tower_power;
    public float tower_speed;
    public float coin;
    public string peoperty;

    void Start()
    {
        ItemInfo();
    }

    public void ItemInfo() {

        int i = 0;
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

        for (i = 0; i < getData["PlayerData"].Count; i++)
        {

            index = int.Parse(getData["PlayerData"][i]["Index"].ToString());
            id_number = int.Parse(getData["PlayerData"][i]["ID_Number"].ToString());
            name = getData["PlayerData"][i]["Name"].ToString();
            info = getData["PlayerData"][i]["Info"].ToString();
            lv = int.Parse(getData["PlayerData"][i]["Lv"].ToString());
            percentage = float.Parse(getData["PlayerData"][i]["Percentage"].ToString());
            health = float.Parse(getData["PlayerData"][i]["Health"].ToString());
            block_power = float.Parse(getData["PlayerData"][i]["Block_power"].ToString());
            block_speed = float.Parse(getData["PlayerData"][i]["Block_speed"].ToString());
            tower_power = float.Parse(getData["PlayerData"][i]["Tower_power"].ToString());
            tower_speed = float.Parse(getData["PlayerData"][i]["Tower_speed"].ToString());
            coin = float.Parse(getData["PlayerData"][i]["Coin"].ToString());
            peoperty = getData["PlayerData"][i]["Peoperty"].ToString();
        }
    }
}
