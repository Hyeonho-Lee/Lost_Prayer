using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	
	// 배열 선언
	public SoundGroup[] soundGroups;

	// 사전에 등록
	Dictionary<string, AudioClip[]>  groupDictionary = new Dictionary<string, AudioClip[]>();

	// Start보다 먼저 실행
	void Awake() {

		// for문의 동생 좀더 조사해야함
		foreach (SoundGroup soundGroup in soundGroups) {

			// 그룹에 추가함
			groupDictionary.Add (soundGroup.groupID, soundGroup.group);
		}

	}

	// 음악이름 함수
	public AudioClip GetClipFromName(string name) {

		// 사전에 이름이 잇을시
		if (groupDictionary.ContainsKey (name)) {

			// 배열 지정
			AudioClip[] sounds = groupDictionary [name];
			// 배열을 랜덤 반환
			return sounds [Random.Range(0, sounds.Length)];
		}

		// 아무것도 없음을 반환
		return null;
	}

	// 음악을 묶어둠
	[System.Serializable]
	public class SoundGroup {

		// 문자 변수
		public string groupID;
		// 오디오 배열
		public AudioClip[] group;
	}
}
