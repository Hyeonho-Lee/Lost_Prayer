using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class BlockTableRead : MonoBehaviour {

    public TextAsset jsonData;

    public int ItemIndex;
    public GameObject imageLocal;

    public Text ItemLabel;
    public Text ItemText;
    public Sprite ItemImage;

    public float losecash;
    public float attack_power;
    public float attack_speed;

    private GameController gamecontroller;
    private AutoTower autotower;
    private WaveClearPanel waveclearpanel;
    private StatManager statmanager;
	private AttackDelay attackdelay;

    void Awake() {

        gamecontroller = GameObject.FindObjectOfType<GameController>();
        autotower = GameObject.FindObjectOfType<AutoTower>();
        waveclearpanel = GameObject.FindObjectOfType<WaveClearPanel>();
        statmanager = GameObject.FindObjectOfType<StatManager>();
		attackdelay = GameObject.FindObjectOfType<AttackDelay>();
    }

	// 아이템 불러오는 함수
    public void ItemInfo(){

        int i = 0;
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

        int index;
        int id_number;
        string name;
        int lv;
        string property;
        string info;
        float coin;
        float atk_power;
        float atk_speed;

        for (i = 0; i < getData["BlockTable"].Count; i++) {

            index = int.Parse(getData["BlockTable"][i]["Index"].ToString());
            id_number = int.Parse(getData["BlockTable"][i]["ID_Number"].ToString());
            name = getData["BlockTable"][i]["Name"].ToString();
            lv = int.Parse(getData["BlockTable"][i]["Lv"].ToString());
            property = getData["BlockTable"][i]["Property"].ToString();
            info = getData["BlockTable"][i]["Info"].ToString();
            coin = float.Parse(getData["BlockTable"][i]["Coin"].ToString());
            atk_power = float.Parse(getData["BlockTable"][i]["ATK_Power"].ToString());
            atk_speed = float.Parse(getData["BlockTable"][i]["ATK_Speed"].ToString());

            if (index.Equals(ItemIndex)) {

                statmanager.shopindex = ItemIndex;
                ItemLabel.text = getData["BlockTable"][ItemIndex]["Name"].ToString();
                ItemText.text = getData["BlockTable"][ItemIndex]["Info"].ToString();
                imageLocal.GetComponent<Image>().sprite = ItemImage;
            }
        }
    }

	// 아이템 읽어오는 함수
    public void ItemUpgrade() {

        int i = 0;
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

        int index;
        int id_number;
        string name;
        int lv;
        string property;
        string info;
        float coin;
        float atk_power;
        float atk_speed;

        for (i = 0; i < getData["BlockTable"].Count; i++) {

            index = int.Parse(getData["BlockTable"][i]["Index"].ToString());
            id_number = int.Parse(getData["BlockTable"][i]["ID_Number"].ToString());
            name = getData["BlockTable"][i]["Name"].ToString();
            lv = int.Parse(getData["BlockTable"][i]["Lv"].ToString());
            property = getData["BlockTable"][i]["Property"].ToString();
            info = getData["BlockTable"][i]["Info"].ToString();
            coin = float.Parse(getData["BlockTable"][i]["Coin"].ToString());
            atk_power = float.Parse(getData["BlockTable"][i]["ATK_Power"].ToString());
            atk_speed = float.Parse(getData["BlockTable"][i]["ATK_Speed"].ToString());

            if (index.Equals(statmanager.shopindex)){

                losecash = coin;
                attack_power = atk_power;
                attack_speed = atk_speed;
            }
        }
    }

	// 업그레이드 함수
    public void Upgrade() {

        ItemUpgrade();

        if (gamecontroller._cash < losecash) {

            waveclearpanel.ItemInfoText = "소지하신 코인이 부족합니다.";
            waveclearpanel.ItemDropInfoPanel();
        }else {

            autotower.BlockDamages += autotower.BlockDamages * (1 + attack_power/100);
			attackdelay.AttackDealy -= attackdelay.AttackDealy * (attack_speed/100);

            gamecontroller.loseCash = losecash;
            gamecontroller.LoseCash();

            waveclearpanel.ItemInfoText =
                              " 블럭공격력: " + "+" + attack_power + "% "+
                              " 블럭공격속도: " + "+" + attack_speed + "%";
            waveclearpanel.ItemDropInfoPanel();
        }
    }
}
