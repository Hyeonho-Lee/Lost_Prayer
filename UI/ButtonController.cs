using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    
	public ShopPanel shopPanel;

    // 공격 딜레이
    public float AttackDealy = 1f;
    private float AttackTime = 0;
    // 공격 가능여부
    public bool Attackbool = false;

    [Header("공격 부분 설정")]
    public GameObject player1_1;
    public int spawnindex1_1;

    public GameObject player1_2;
    public int spawnindex1_2;

    public GameObject player1_3;
    public int spawnindex1_3;

    public GameObject player1_4;
    public int spawnindex1_4;
    /*--------------------------*/
    public GameObject player2_1;
    public int spawnindex2_1;

    public GameObject player2_2;
    public int spawnindex2_2;

    public GameObject player2_3;
    public int spawnindex2_3;

    public GameObject player2_4;
    public int spawnindex2_4;
    /*--------------------------*/
    public GameObject player3_1;
    public int spawnindex3_1;

    public GameObject player3_2;
    public int spawnindex3_2;

    public GameObject player3_3;
    public int spawnindex3_3;

    public GameObject player3_4;
    public int spawnindex3_4;

    /*
	public GameObject qplayer;
	public int qspawnindex;

	public GameObject wplayer;
	public int wspawnindex;

    public GameObject eplayer;
    public int espawnindex;

    public GameObject rplayer;
    public int rspawnindex;
    */

    // 처음시작시
    void Start() {

		Attackbool = true;
	}

	// 변화값
	void Update() {

		AttackTime += Time.deltaTime;

		if (AttackTime >= AttackDealy) {

			Attackbool = true;
		}
	}

	// 라인어택
	public void LineAttack_1_1() {

		if (Attackbool == true) {

			Instantiate(player1_1, Ground.GroundArrays[spawnindex1_1].position, this.transform.rotation);
			AttackTime = 0;
			Attackbool = false;
		}
	}

    public void LineAttack_1_2()
    {

        if (Attackbool == true)
        {

            Instantiate(player1_2, Ground.GroundArrays[spawnindex1_2].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_1_3()
    {
        
        if (Attackbool == true)
        {

            Instantiate(player1_3, Ground.GroundArrays[spawnindex1_3].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_1_4()
    {

        if (Attackbool == true)
        {

            Instantiate(player1_4, Ground.GroundArrays[spawnindex1_4].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_2_1()
    {

        if (Attackbool == true)
        {

            Instantiate(player2_1, Ground.GroundArrays[spawnindex2_1].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_2_2()
    {

        if (Attackbool == true)
        {

            Instantiate(player2_2, Ground.GroundArrays[spawnindex2_2].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_2_3()
    {

        if (Attackbool == true)
        {

            Instantiate(player2_3, Ground.GroundArrays[spawnindex2_3].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_2_4()
    {

        if (Attackbool == true)
        {

            Instantiate(player2_4, Ground.GroundArrays[spawnindex2_4].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_3_1()
    {

        if (Attackbool == true)
        {

            Instantiate(player3_1, Ground.GroundArrays[spawnindex3_1].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_3_2()
    {

        if (Attackbool == true)
        {

            Instantiate(player3_2, Ground.GroundArrays[spawnindex3_2].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_3_3()
    {

        if (Attackbool == true)
        {

            Instantiate(player3_3, Ground.GroundArrays[spawnindex3_3].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }

    public void LineAttack_3_4()
    {

        if (Attackbool == true)
        {

            Instantiate(player3_4, Ground.GroundArrays[spawnindex3_4].position, this.transform.rotation);
            AttackTime = 0;
            Attackbool = false;
        }
    }


    // 시간을 멈추는 함수
    public void TimeStop() {
		
		// 만약 현재시간이 0배속이라면
		if(Time.timeScale == 0.0f) {
			
			// 1배속으로 지정
			Time.timeScale = 1.0f;
		}else {
			
			// 0배속으로 지정
			Time.timeScale = 0.0f;
		}
	}

    
	// 상점을 오픈하는 함수
	void shopOpen() {
		
		// 함수를 호출함
		//shopPanel.Open();
	}
    
}
