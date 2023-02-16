using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMap : MonoBehaviour {

	public WaveSetting[] waveSetting;
	public Transform[] EnemySpawnPos;

    WaveSetting currentWave;
    private int currentWaveNumber;

    // 스폰될 적의 수를 표시함
    private int enemiesRemainingToSpawn;
    // 남은 적의 수를 표시함
    private int enemiesRemainingAlive;
    // 다음 스폰시간을 표시함
    private float nextSpawnTime;
	private int RandomPosCount;

	public bool waveClear = false;
	public bool waveNow = false;

	private int playercount = 0;
    private float enemycount = 0;
    private float NextEnemyCount;

    // 다음 웨이브를 호출하는 이벤트
    public event System.Action<int> OnNewWave;

	void OnTriggerEnter(Collider col) {

        if (col.CompareTag("Player")) {

			playercount++;
			if(playercount % 2 == 0 && waveClear ==  false && waveNow == false) {
				
				NextWave();
			}
        }
    }

	void Start() {

		//NextWave();
		//Debug.Log("hellos");
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

        if (currentWaveNumber - 1 < waveSetting.Length) {

            // wave라는 배열을 만듬
            currentWave = waveSetting[currentWaveNumber - 1];

            // 스폰해야할 적
            enemiesRemainingToSpawn = currentWave.enemyCount;
            // 아직 살아잇는적의 수는 스폰할 적의 수로 지정
            enemiesRemainingAlive = enemiesRemainingToSpawn;
            waveNow = true;

            // 만약 다음웨이브가 null이 아니라면
            if (OnNewWave != null) {

                // 다음웨이브는 최대 웨이브수임
                OnNewWave(currentWaveNumber);
            }
        }else {
		
			waveClear = true;
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
		RandomPosCount = EnemySpawnPos.Length;
        int randompos = Random.Range(0, RandomPosCount);
        //EnemySpawnPos[randompos].position = EnemySpot.EnemySpotArrays[randompos];

        // 적을 인스턴트로 생성
        Instantiate(currentWave.enemyPrefab, EnemySpawnPos[randompos].position, EnemySpawnPos[randompos].rotation);
        enemycount++;
        //statmanager.enemycounts++;
    }

    // 적이 죽엇을떄
    public void OnEnemyDeath() {

        // 살아잇는적의 수를 감소시킴
        enemiesRemainingAlive--;

        // 만약 살아잇는 적의 수가 0과 같다면
        if (enemiesRemainingAlive == 0) {

            // 함수 호출
            waveNow = false;

			// 웨이브가 끝날시
            if (waveNow == false) {

                //statmanager.Save();
				//waveclearpanel.WaveClear();
                //ShopCanvas.SetActive(true);
            }

			// 웨이브 클리어 상태
            //waveClear = true;

			// 보스 웨이브 끝날시 카드 활성화
            if (currentWaveNumber == 5 && waveClear == true) {
				
                //CardPanel.SetActive(true);
                //unActive.SetActive(false);
            } else if(currentWaveNumber == 10 && waveClear == true) {
				
                //CardPanel.SetActive(true);
               //unActive.SetActive(false);
            } else if(currentWaveNumber == 15 && waveClear == true) {
				
                //CardPanel.SetActive(true);
                //unActive.SetActive(false);
            } else if(currentWaveNumber == 20 && waveClear == true) {

				// 스테이지 클리어
				StartCoroutine(StageClear_0());
            }
        }
    }

	// 다음 웨이브 시작 함수
    public void ClickReady() {

        NextWave();
        //statmanager.stage_waves++;
        //ShopCanvas.SetActive(false);
        NextEnemyCount = currentWave.enemyCount;
        //waveclearpanel.OnNewWave();
    }

    IEnumerator StageClear_0() {

        //Debug.Log("Clear!! 10s go  to lobby");
		//statmanager.Save();
        //statmanager.Load();
		//waveclearpanel.AllWaveClear();
        yield return new WaitForSeconds(10f);
        //statmanager.Save();
        //Application.LoadLevel("Result_0");
    }

    // wave 함수
    [System.Serializable]
    public class WaveSetting {

        public GameObject enemyPrefab;
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
