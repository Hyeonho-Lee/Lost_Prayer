using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// 배열 선언
	public enum AudioChannel {Master, Sfx, Music};

	// 최대볼륨의 퍼센트
	float masterVolumePercent = 100f;
	// 특수효과의 퍼센트
	float sfxVolumePercent = 100;
	// 배경음의 퍼센트
	float musicVolumePercent = 1f;

	// sfx음악
	AudioSource sfx2DSource;
	// 모든음악 배열
	AudioSource[] musicSources;
	// 음악 인덱스 변수
	int activeMusicSourceIndex;

	// 스크립트 변수
	public static AudioManager instance;

	// 오디오 프리펩
	Transform audioListener;
	// 플레이어 프리펩
	Transform playerT;

	// 스크립트 호출
	SoundController soundcontroller;

	// Start보다 먼저실행
	void Awake() {

		// 만약 instance가 존재할시
		if (instance != null) {

			// 오브젝트 삭제
			Destroy (gameObject);
		} else {

			// 스크립트 변수를 이것으로 지정
			instance = this;
			// 이 오브젝트를 삭재안함
			DontDestroyOnLoad (gameObject);

			// 사전 컴포넌트 등록
			soundcontroller = GetComponent<SoundController> ();

			// 배경음은 인덱스2임
			musicSources = new AudioSource[2];
			// 만약 2보다 작을시 반복
			for (int i = 0; i < 2; i++) {

				// 노래 오브젝트를 생성
				GameObject newMusicSource = new GameObject ("Music source " + (i + 1));
				// 컴포넌트 지정
				musicSources [i] = newMusicSource.AddComponent<AudioSource> ();
				// 노래오브젝트의 부모지정
				newMusicSource.transform.parent = transform;
			}

			// 노래 오브젝트를 생성
			GameObject newSfx2DSource = new GameObject ("2D sfx source");
			// 컴포넌트 지정
			sfx2DSource = newSfx2DSource.AddComponent<AudioSource> ();
			// 노래오브젝트의 부모지정
			newSfx2DSource.transform.parent = transform;

			// 오디오 프리펩을 찾아지정
			audioListener = FindObjectOfType<AudioListener> ().transform;
			// 플레이어를 찾아지정
			playerT = FindObjectOfType<Key>().transform;

			// 플레이어 프리팹 저장
			masterVolumePercent = PlayerPrefs.GetFloat("master vol", masterVolumePercent);
			sfxVolumePercent = PlayerPrefs.GetFloat ("sfx vol", sfxVolumePercent);
			musicVolumePercent = PlayerPrefs.GetFloat ("music vol", musicVolumePercent);
		}
	}

	// 변화값
	void Update() {

		// 플레이어가 잇을시
		if (playerT != null) {

			// 오디오위치를 플레이어위치로 지정
			audioListener.position = playerT.position;
		}
	}

	// 음악크기 함수
	public void SetVolume(float volumePercent, AudioChannel channel) {

		// 스위치문
		switch (channel) {
		// Master일 경우
		case AudioChannel.Master:
			// 볼륨퍼센트 지정
			masterVolumePercent = volumePercent;
			break;
			// Sfx일 경우
		case AudioChannel.Sfx:
			sfxVolumePercent = volumePercent;
			break;
			// Music일 경우
		case AudioChannel.Music:
			musicVolumePercent = volumePercent;
			break;
		}

		// 배열값 지정
		musicSources [0].volume = musicVolumePercent * masterVolumePercent;
		musicSources [1].volume = musicVolumePercent * masterVolumePercent;

		// 플레이어 프리팹 지정?
		PlayerPrefs.SetFloat("master vol", masterVolumePercent);
		PlayerPrefs.SetFloat ("sfx vol", sfxVolumePercent);
		PlayerPrefs.SetFloat ("music vol", musicVolumePercent);

	}

	// 음악재생 함수
	public void PlayMusic(AudioClip clip, float fadeDuration = 1) {

		//  인덱스지정
		activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		// 배경음의 인덱스를 저장
		musicSources[activeMusicSourceIndex].clip = clip;
		// 배경음 재생
		musicSources[activeMusicSourceIndex].Play();

		// 코루틴 실행
		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}

	// 3d 소리재생 함수
	public void PlaySound(AudioClip clip, Vector3 pos) {
		// 소리가있을시
		if (clip != null) {
			// 소리를 재생
			AudioSource.PlayClipAtPoint (clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	}

	// 3d 소리재생 함수
	public void PlaySound(string soundName, Vector3 pos) {

		// 소리를 재생
		PlaySound (soundcontroller.GetClipFromName (soundName), pos);
	}

	//2d 소리재생 함수
	public void PlaySound2D(string soundName) {

		// 소리재생
		sfx2DSource.PlayOneShot (soundcontroller.GetClipFromName (soundName), sfxVolumePercent * masterVolumePercent);
	}

	// 음악페이드 효과 코루틴
	IEnumerator AnimateMusicCrossfade(float duration) {

		// 퍼센트지정
		float percent = 0;
		// 퍼센트가 0보다 작을시 반복
		while (percent < 1) {

			// 퍼센트를 올림
			percent += Time.deltaTime * 1 / duration;
			// 배경음의 볼륨 지정
			musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
			musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
			// 무한반복 방지
			yield return null;
		}
	}
}
