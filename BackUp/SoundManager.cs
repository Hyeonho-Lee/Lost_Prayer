using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	
	public AudioClip hitSound;
	public AudioClip groundSound;
	public AudioClip enemyFireGolemAttackSound;
	public AudioClip enemyGolemSpawnSound;
	public AudioClip enemySlaimWalkSound;

	public static SoundManager instance;

	AudioSource audiosource;

	void Awake() {

		if(SoundManager.instance == null) {

			SoundManager.instance = this;
		}
	}

	void Start() {

		audiosource = GetComponent<AudioSource>();
	}

	// 사운드 크기 설정
	public void SetVolume(float value) {

		AudioListener.volume = value;
		value = 0.5f;
	}

	public void HitSound() {

		audiosource.PlayOneShot(hitSound);
	}

	public void GroundSound() {

		audiosource.PlayOneShot(hitSound);
	}

	public void EnemyFireGolemAttackSound() {

		audiosource.PlayOneShot(enemyFireGolemAttackSound);
	}

	public void EnemyGolemSpawnSound() {

		audiosource.PlayOneShot(enemyGolemSpawnSound);
	}

	public void EnemySlaimWalkSound() {

		audiosource.PlayOneShot(enemySlaimWalkSound);
	}

}
