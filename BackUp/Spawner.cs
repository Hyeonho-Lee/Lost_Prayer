using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public WaveSetting[] waveSetting;

    WaveSetting currentWave;
    public int currentWaveNumber;
    public bool waveNowing = false;

    // 스폰될 적의 수를 표시함
    int enemiesRemainingToSpawn;
    // 남은 적의 수를 표시함
    int enemiesRemainingAlive;
    // 다음 스폰시간을 표시함
    float nextSpawnTime;

    public float enemycount = 0;
    public float NextEnemyCount;

    public GameObject ShopCanvas;
    public GameObject CardPanel;
    public GameObject unActive;

    // 다음 웨이브를 호출하는 이벤트
    public event System.Action<int> OnNewWave;
    private EnemySpot enemyspot;
    private StatManager statmanager;
    private WaveClearPanel waveclearpanel;

    void Awake() {

        enemyspot = GetComponent<EnemySpot>();
        statmanager = GameObject.FindObjectOfType<StatManager>();
        waveclearpanel = GameObject.FindObjectOfType<WaveClearPanel>();
    }

    // 처음 시작할떄
    void Start() {

        ShopCanvas.SetActive(true);

        //statmanager.Load();
        //statmanager.Save();
    }

    // 변화값
    void Update() {

        StartCoroutine(Delay());
    }

    // 딜레이 계산
    IEnumerator Delay() {

        // 만약 스폰될 적의 수가 0보다크고 또는 무한상태일격우 현재시간이 다음스폰시간보다 크다면
        if ((enemiesRemainingToSpawn > 0) && Time.time > nextSpawnTime) {

            // 스폰될 수를 줄임
            enemiesRemainingToSpawn--;
            // 다음 스폰시간을 현재시간 + 다음 웨이브 진행될 시간 만큼 더함
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            // 코루틴을 불러옴
            StartCoroutine(SpawnEnemy());
        }

        yield return null;
    }

    // 다음으로 넘어갈 웨이브 함수
    void NextWave() {

        // 웨이브 숫자를 증가시킴
        currentWaveNumber++;

        // 만약 현재 웨이브의숫자를 뺸것이 배열 왼쪽이라면?
        if (currentWaveNumber - 1 < waveSetting.Length) {

            // wave라는 배열을 만듬
            currentWave = waveSetting[currentWaveNumber - 1];

            // 스폰해야할 적
            enemiesRemainingToSpawn = currentWave.enemyCount;
            // 아직 살아잇는적의 수는 스폰할 적의 수로 지정
            enemiesRemainingAlive = enemiesRemainingToSpawn;
            currentWave.waveNow = true;
            waveNowing = true;

            // 만약 다음웨이브가 null이 아니라면
            if (OnNewWave != null) {

                // 다음웨이브는 최대 웨이브수임
                OnNewWave(currentWaveNumber);
            }
        }
    }

    // 적을 생성하는 함수
    IEnumerator SpawnEnemy() {

        // 소환딜레이 시간
        float spawnDelay = 1;
        // 소환시간을 저장
        float spawnTimer = 0;

        // 소환시간이 딜레이보다 작으면 반복함
        while (spawnTimer < spawnDelay) {

            // 소환시간을 현재시간만큼 더함
            spawnTimer += Time.deltaTime;
            yield return null;
        }

		// 스폰 랜덤위치 배열에 스폰생성
        currentWave.randompos = Random.Range(0, 49);
        currentWave.spawnPoint = EnemySpot.EnemySpotArrays[currentWave.randompos];

        // 적을 인스턴트로 생성
        Instantiate(currentWave.enemyPrefab, currentWave.spawnPoint.position, currentWave.spawnPoint.rotation);
        enemycount++;
        statmanager.enemycounts++;
    }

    // 적이 죽엇을떄
    public void OnEnemyDeath() {

        // 살아잇는적의 수를 감소시킴
        enemiesRemainingAlive--;

        // 만약 살아잇는 적의 수가 0과 같다면
        if (enemiesRemainingAlive == 0) {

            // 함수 호출
            currentWave.waveNow = false;
            waveNowing = false;

			// 웨이브가 끝날시
            if (currentWave.waveNow == false) {

                statmanager.Save();
				waveclearpanel.WaveClear();
                ShopCanvas.SetActive(true);
            }

			// 웨이브 클리어 상태
            currentWave.waveClear = true;

			// 보스 웨이브 끝날시 카드 활성화
            if (currentWaveNumber == 5 && currentWave.waveClear == true) {
				
                CardPanel.SetActive(true);
                unActive.SetActive(false);
            } else if(currentWaveNumber == 10 && currentWave.waveClear == true) {
				
                CardPanel.SetActive(true);
                unActive.SetActive(false);
            } else if(currentWaveNumber == 15 && currentWave.waveClear == true) {
				
                CardPanel.SetActive(true);
                unActive.SetActive(false);
            } else if(currentWaveNumber == 20 && currentWave.waveClear == true) {

				// 스테이지 클리어
				StartCoroutine(StageClear_0());
                ShopCanvas.SetActive(false);
            }
        }
    }

	// 다음 웨이브 시작 함수
    public void ClickReady() {

        NextWave();
        statmanager.stage_waves++;
        ShopCanvas.SetActive(false);
        NextEnemyCount = currentWave.enemyCount;
        waveclearpanel.OnNewWave();
    }

    IEnumerator StageClear_0() {

        //Debug.Log("Clear!! 10s go  to lobby");
		statmanager.Save();
        statmanager.Load();
		waveclearpanel.AllWaveClear();
        yield return new WaitForSeconds(10f);
        statmanager.Save();
        Application.LoadLevel("Result_0");
    }

	IEnumerator StageClear_1() {

        //Debug.Log("Clear!! 10s go  to lobby");
        statmanager.Load();
		waveclearpanel.AllWaveClear();
        yield return new WaitForSeconds(10f);
        statmanager.Save();
        Application.LoadLevel("Result_0");
	}

    // wave 함수
    [System.Serializable]
    public class WaveSetting {

        //public bool shopOpen = false;
        public bool waveNow = false;
        public bool waveClear = false;
        public GameObject enemyPrefab;
        public Transform spawnPoint;
        public int randompos;
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
