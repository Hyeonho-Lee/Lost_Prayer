using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//ngui라벨을 들고옴
	public Text cashlabel;

	// 돈을 얼마만큼 드랍을 할것이냐
	public float takeCash;
	// 돈을 얼마만큼 잃을 할것이냐
	public float loseCash;
	// 변화할 돈
	public float _cash;
	// 현재 가진 돈
	public float Cash {

		get {
		
			return _cash;
		}

		set {
		
			_cash = value;
            // 라벨의 텍스트를 바꿈
			cashlabel.text = _cash.ToString();
		}
	}

    private StatManager statmanager;

	// 처음시작할떄
	void Start() {

        statmanager = GameObject.FindObjectOfType<StatManager>();
        statmanager.Load();
        Cash = statmanager.coins;
    }

	// 돈을 올림
	public void TakeCash() {
		
		Cash += takeCash;
        statmanager.coins += takeCash;
    }

	// 돈을 잃음
	public void LoseCash() {

		Cash -= loseCash;
        statmanager.coins -= loseCash;
    }
}
