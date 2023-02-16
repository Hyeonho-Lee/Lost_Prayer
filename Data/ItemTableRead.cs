using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class ItemTableRead : MonoBehaviour {

    public TextAsset jsonData;

    public int ItemIndex;
	public int shopIndex;
    public GameObject imageLocal;

    public Text ItemLabel;
    public Text ItemText;
    public Sprite ItemImage;

	public float takecash;
	public float health;
	public float attack_power;
	public float attack_speed;
	public string name;
	public string info;

	private AutoTower autotower;
	private Key key;
	private GameController gamecontroller;
	private Lobby_Book lobby_book;
    private WaveClearPanel waveclearpanel;

	void Awake() {

		autotower = GameObject.FindObjectOfType<AutoTower>();
		key = GameObject.FindObjectOfType<Key>();
		gamecontroller = GameObject.FindObjectOfType<GameController>();
		lobby_book = GameObject.FindObjectOfType<Lobby_Book> ();
        waveclearpanel = GameObject.FindObjectOfType<WaveClearPanel>();
    }

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

        for (i = 0; i < getData["ItemTable"].Count; i++) {

            index = int.Parse(getData["ItemTable"][i]["Index"].ToString());
            id_number = int.Parse(getData["ItemTable"][i]["ID_Number"].ToString());
            name = getData["ItemTable"][i]["Name"].ToString();
            percentage = float.Parse(getData["ItemTable"][i]["Percentage"].ToString());
            rating = getData["ItemTable"][i]["Rating"].ToString();
            info = getData["ItemTable"][i]["Info"].ToString();
            coin = float.Parse(getData["ItemTable"][i]["Coin"].ToString());
            hp = float.Parse(getData["ItemTable"][i]["HP"].ToString());
            atk_power = float.Parse(getData["ItemTable"][i]["ATK_Power"].ToString());
            atk_speed = float.Parse(getData["ItemTable"][i]["ATK_Speed"].ToString());

            if (index.Equals(ItemIndex)) {
				
				shopIndex = ItemIndex;
                ItemLabel.text = getData["ItemTable"][ItemIndex]["Name"].ToString();
                ItemText.text = getData["ItemTable"][ItemIndex]["Info"].ToString();
                imageLocal.GetComponent<Image>().sprite = ItemImage;

				//name = getData["ItemTable"][ItemIndex]["Name"].ToString();
				//info = getData["ItemTable"][ItemIndex]["Info"].ToString();
            }
        }
    }

	public void DropItem() {

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

		for (i = 0; i < getData["ItemTable"].Count; i++) {

			index = int.Parse(getData["ItemTable"][i]["Index"].ToString());
			id_number = int.Parse(getData["ItemTable"][i]["ID_Number"].ToString());
			name = getData["ItemTable"][i]["Name"].ToString();
			percentage = float.Parse(getData["ItemTable"][i]["Percentage"].ToString());
			rating = getData["ItemTable"][i]["Rating"].ToString();
			info = getData["ItemTable"][i]["Info"].ToString();
			coin = float.Parse(getData["ItemTable"][i]["Coin"].ToString());
			hp = float.Parse(getData["ItemTable"][i]["HP"].ToString());
			atk_power = float.Parse(getData["ItemTable"][i]["ATK_Power"].ToString());
			atk_speed = float.Parse(getData["ItemTable"][i]["ATK_Speed"].ToString());

			if (index.Equals(ItemIndex)) {

				takecash = coin;
				health = hp;
				attack_power = atk_power;
				attack_speed = atk_speed;
			}
		}
	}

	void DropItemUpgrade() {
		
		autotower.AttackDamages += attack_power;
		autotower.fireRate += attack_speed;

		if (autotower.AttackDamages <= 0) {

			autotower.AttackDamages = 1;
		}

		key.health += health;

		gamecontroller.takeCash = takecash;
		gamecontroller.TakeCash();
	}

	void OnMouseDown() {

        Upgrade();
        Destroy (this.gameObject);
		//Debug.Log (takecash + "   " + health + "   "  + block_power + "   " + attack_speed + "업그레이드 완료");
	}

    void Upgrade() {

        DropItem();
        DropItemUpgrade();
        waveclearpanel.ItemInfoText =
                                      " 유물 공격력: " + attack_power +
                                      " 유물 공격속도: " + attack_speed;
        waveclearpanel.ItemDropInfoPanel();
    }
}
