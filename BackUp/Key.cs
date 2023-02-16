using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ui 스크립트

public class Key : MonoBehaviour {

    [Header("공격 부분 설정")]
	// 이미지 관리 프리팹
	public Image healthBar;

	[Header("채력및 코인 설정 부분")]
	public float startHealth = 100f;
	public float health;
    public float EnemyAttackDamage;

    [Header("UI 설정")]
    public Text healthText;
    public Text maxText;

    private Spawner spawner;
    private Enemy enemy;
    private StatManager statmanager;

    // 처음 시작할떄
    void Start() {

        spawner = GameObject.FindObjectOfType<Spawner>();
        enemy = GameObject.FindObjectOfType<Enemy>();
        statmanager = GameObject.FindObjectOfType<StatManager>();
        // 채력 초기화
        health = startHealth;
        statmanager.healths = health;
        maxText.text = Mathf.Round(startHealth).ToString();
        healthText.text = Mathf.Round(health).ToString();
    }

	// 데미지를 받았을떄
	public void TakeDamage() {

        // 채력을 깍음
        health -= EnemyAttackDamage;
        statmanager.healths = health;

        // 채력바를 채력으로 지정
        healthBar.fillAmount = health / startHealth;
        healthText.text = Mathf.Round(health).ToString();

        // 만약 채력이 0 미만일시 삭제
        if (health <= 0f) {
			
			//오브젝트 삭제
			Destroy(gameObject);
            // 게임을 정지함
            StartCoroutine(StageFail());
        }
	}

	// 스테이지 실패할시
    IEnumerator StageFail() {

        statmanager.Save();
        statmanager.Load();
        Application.LoadLevel("DieResult");

        yield return null;
    }
}