using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {

	public GameObject bossDieEffect;
	public Vector3 RandomizeIntensity = new Vector3(0, 0, 0);
    public int randomPos;
    private Transform spawnPoint;

	// 보스 패텀 함수
    public void BossPatten_0() {

		// 랜덤 위치 지정
        randomPos = Random.Range(0, 25);
		// 스폰위치 랜덤 저장
        spawnPoint = EnemySpot.EnemySpotArrays[randomPos];
		// 위치를 랜덤으로 지정
        this.transform.position = spawnPoint.position;
    }

	// 보스 죽는 함수
	public void BossDie_0() {

		// 이펙트를 인스턴트로 생성
		GameObject effectdielns = (GameObject)Instantiate(bossDieEffect, transform.position, transform.rotation);
		effectdielns.transform.localPosition += new Vector3(0, 5, 0);
		effectdielns.transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
			Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
			Random.Range(-RandomizeIntensity.y, RandomizeIntensity.z));
		// n초뒤 이펙트 삭제
		Destroy(effectdielns, 2f);
	}
}
