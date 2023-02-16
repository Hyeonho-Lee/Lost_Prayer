using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ui 스크립트

public class Enemy : MonoBehaviour {

	[Header("적 벨런스 설정")]
    // 타겟 프리팹
    public Transform target;
    private Transform enemytarget;
    public string enemyTag = "Key";
    public string enemyTag_1 = "Player";
    // 공격 범위
    public float range = 5f;
    public bool isAttacking = false;
    public bool isBulletAttacking = false;
    // 채력지정
    public float startHealth = 10f;
	public float health;
	private float percenthealth;
	// 속도지정
	public float startSpeed = 15f;
    private float speed;
	// 점프파워
    public float jumpPower = 7f;
	// 총알 데미지
	public float damage = 0f;
    // 적의 공격력
    public float AttackDamage = 0f;
	// 드랍 캐쉬
	public float dropcash = 1f;
    // 데미지 받을시
    public bool isDamageing = false;
    public float startDelay = 1f;
    private float delay;

    public bool isBoss = false;
	private bool isBossDie = false;

    public GameObject CanvasParent;
    public GameObject FloatingText;

    [Header("적 아이템 설정")]
    public TextAsset jsonData;
    private int ItemIndex;
    private string ItemName;

	[Header("적 점수 설정")]
	public float DropScore;

    public DropItem[] dropItem;

    DropItem currentItem;
    public int currentItemNumber = 0;
    public event System.Action<int> OnNewItem;

    [System.Serializable]
    public class DropItem {

        public GameObject Item;
    }

    //확률을 배열에 저장
    int[] ind = new int[100];
    float[] per = new float[100];
    string[] nam = new string[100];

    [Header("적 이펙트 설정")]
	// 스폰 이펙트 프리팹
	public GameObject SpawnEffectPrefab;
    public GameObject HitEffectPrefab;
    public GameObject DieEffectPrefab;
    public float EffectTime = 1f;
    public Vector3 RandomizeIntensity = new Vector3(3.5f, 0, 0);

    [Header("UI 설정")]
	// 이미지 관리 프리팹
	public Image healthBar;
    // 최대 채력
    //public Text startHealthText;
    // 현재 채력
    //public Text healthText;

    private GameController gamecontroller;
    private Spawner spawner;
    private Key key;
	private EnemyMotion enemymotion;
	private StatManager statmanager;
    private EnemyBoss enemyboss;
	private SoundManager soundmanager;
    Animator animator;
    Rigidbody rigidbody;

    void Awake() {

        // 스크립트를 가져옴
        gamecontroller = GameObject.FindObjectOfType<GameController>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        key = GameObject.FindObjectOfType<Key>();
		enemymotion = GameObject.FindObjectOfType<EnemyMotion>();
		statmanager = GameObject.FindObjectOfType<StatManager> ();
        enemyboss = GameObject.FindObjectOfType<EnemyBoss>();
		soundmanager = GameObject.FindObjectOfType<SoundManager>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // 처음 시작할때
    void Start () {

        // 체력 초기화
		percenthealth = startHealth * (1 + (float)statmanager.stage_waves * 0.5f);
		startHealth = percenthealth;
		health = startHealth;
        speed = startSpeed;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        int iTemp = Random.Range(1, 100);
        float perMin = 100.0f;

        ItemInfo();

        for (int i = 0; i < per.Length; i++) {
            
            if (iTemp <= per[i]) {

                //확률에 만족한 아이템 리스트를 Debug에 출력-->나중에 이부분 지우면 될듯
                //Debug.Log("확률 " + iTemp + " " + "아이템 이름: " + Istr[i] + "         " + "아이템 드랍 확률: " + per[i]);

                if (perMin > per[i]) {

                    perMin = per[i];
                    //그중 가장 확률이 낮은 아이템을 출력
                    //Debug.Log("진짜 드랍된 아이템 이름: " + nam[i] + "         " + "아이템 드랍 확률: " + per[i]);
                    ItemIndex = ind[i];
                    ItemName = nam[i];
                }
            }
        }

        SpawnEffect();
		soundmanager.EnemyGolemSpawnSound();
    }

    // 변화값
    void Update() {

        Move();
        Check();
    }

    // 움직일때
    void Move() {

        // 방향값을 구함
        Vector3 dist = (target.position - transform.position).normalized;
        // 이동시킴
        transform.position = transform.position + (dist * startSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dist); 
		enemymotion.Move();

        speed = startSpeed;
    }

    void Check(){

        if (health < startHealth * 0.5 && isBoss != false) {

            SpawnEffect();
            enemyboss.BossPatten_0();
			startSpeed += 1;
            SpawnEffect();
			isAttacking = false;
            isBoss = false;
			isBossDie = true;
        }

        if (isDamageing == true) {

            startSpeed = 0;
            delay += Time.deltaTime;

            if (startDelay <= delay) {

                isDamageing = false;
                delay = 0;
            }
        }else if (isDamageing == false) {

            startSpeed = speed;
        }

        if (isAttacking == true) {

            startSpeed = 0;
        }
        else {
			isAttacking = false;
            startSpeed = speed;
        }
    }

    // 총알 데미지를 받았을떄
    public void TakeDamage() {

        // 채력을 깍음
        health -= damage;
        // 채력바를 채력으로 지정
        healthBar.fillAmount = health / startHealth;
		soundmanager.HitSound();

        if (FloatingText && health > 0) {

            HitEffect();
            ShowFloatingText();
        }

		// 만약 채력이 0 미만일시 삭제
		if(health <= 0f) {
			
			// 함수 호출
			Die();
		}
	}

    public void Slow(float amount) {

        speed = startSpeed * (1f - amount);
    }

    void ShowFloatingText() {

        var go = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        go.transform.parent = CanvasParent.transform;

        var strhealth = GetN2(health);
        go.GetComponent<TextMesh>().text = strhealth.ToString();
    }

    // 죽었을떄
    public void Die() {

		statmanager.scores += DropScore;
        //오브젝트 삭제
        Destroy(gameObject);
        DropItemIndex();

        // 발사소리 출력
        //SoundController.Soundinstance.DieSound();
		if (isBossDie != false) {

			for(int i = 0; i < 10; i++) {

				enemyboss.BossDie_0();
			}
		} else {

			DieEffect();
		}

        spawner.OnEnemyDeath();
    }

	// attackcube에 맞을시
	public void TakeDamageCube() {

        isDamageing = true;
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
	}

    // 타겟을 인식하는 범위를 표현
    void OnDrawGizmosSelected() {

        // 색을 빨강으로 지정
        Gizmos.color = Color.blue;
        // 원으로 범위를 표시함
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // 타겟 변화를 관리
    void UpdateTarget() {

        // 적을 배열로 지정
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // 가까운적의 거리를 잼
        float shortesDistance = Mathf.Infinity;
        // 가까운적을 null로 지정
        GameObject nearestEnemy = null;

        // 이상한 반복문
        foreach (GameObject enemy in enemies) {

            // 적과 타워사이의 거리를 계산
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // 만약에 거리안에 들어왓을떄
            if (distanceToEnemy < shortesDistance) {

                // 가까운적의 거리를 타워사이의 거리로 지정
                shortesDistance = distanceToEnemy;
                // 가까운적을 적으로 지정
                nearestEnemy = enemy;
            }
        }

        // 가까운적이 null이 아니거나 가까운거리가 범위보다 작을시
        if (nearestEnemy != null && shortesDistance <= range) {

            // 타겟을 가까운적으로 지정
            enemytarget = nearestEnemy.transform;

            isAttacking = true;
			enemymotion.Attack ();
			soundmanager.EnemyFireGolemAttackSound();
        }else {

            // 타겟을 null로 지정함
            enemytarget = null;
            enemymotion.Attacks();
        }
    }

    // 충돌 햇을시
    void OnTriggerEnter(Collider col) {

        // 만약 태그가 적일시
        if (col.CompareTag("Key")) {

            key.EnemyAttackDamage = AttackDamage;
            key.TakeDamage();
        }
    }

    //아이템 정보 읽기
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
            name = getData["ItemTable"][i]["Name"].ToString();
            percentage = float.Parse(getData["ItemTable"][i]["Percentage"].ToString());

            ind[i] = index;
            per[i] = percentage;
            nam[i] = name;
        }
    }

	// 아이템 인덱스 함수
    public void DropItemIndex() {

        int i = 0;

        for(i = 0; i < dropItem.Length; i++) {

            currentItemNumber++;

            if (currentItemNumber - 1 < dropItem.Length) {

                // wave라는 배열을 만듬
                currentItem = dropItem[currentItemNumber - 1];

                if (i == ItemIndex) {

                    //GameObject Items = (GameObject)Instantiate(dropItem[i].Item, transform.position, transform.rotation) as GameObject;
                    GameObject Items = (GameObject)Instantiate(dropItem[i].Item, transform.position, Quaternion.identity) as GameObject;
                    Destroy(Items, 15f);
                    //Debug.Log("아이템 인덱스: " + ItemIndex + "   " + "아이템 이름: " + ItemName);

                    gamecontroller.takeCash = dropcash;
                    gamecontroller.TakeCash();

                    /*if (dropItem [i].Item == dropItem [0].Item) {
						
						// 돈을 올림
						gamecontroller.takeCash = dropcash;
						gamecontroller.TakeCash();

						Destroy (Items, 2f);
					}*/
                }

                if (OnNewItem != null) {

                    OnNewItem(currentItemNumber);
                }
            }
        }
    }

	// 스폰 이펙트 함수
    void SpawnEffect() {

        // 이펙트를 인스턴트로 생성
        GameObject effectlns = (GameObject)Instantiate(SpawnEffectPrefab, transform.position, transform.rotation);
        // n초뒤 이펙트 삭제
        Destroy(effectlns, EffectTime);
    }

	// 타격 이펙트 함수
    void HitEffect() {

        // 이펙트를 인스턴트로 생성
        GameObject effectlnss = (GameObject)Instantiate(HitEffectPrefab, transform.position, transform.rotation);
        effectlnss.transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
                                                        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                                                        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.z));
        // n초뒤 이펙트 삭제
        Destroy(effectlnss, EffectTime);
    }

	// 죽는 이펙트 함수
    void DieEffect() {

        // 이펙트를 인스턴트로 생성
        GameObject effectdielns = (GameObject)Instantiate(DieEffectPrefab, transform.position, transform.rotation);
        effectdielns.transform.localPosition += new Vector3(0, 2, 0);
        // n초뒤 이펙트 삭제
        Destroy(effectdielns, 2f);
    }

	// 소수점 출력 함수
    public string GetN2(float num) {

        string result = string.Empty;

        if(num == (int)num){

            result = num.ToString();
        }else {

            result = num.ToString("N2");
        }

        return result;
    }
}
