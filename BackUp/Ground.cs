using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    // 방향을 바꿀 포인트를 배열로 지정
    public static Transform[] GroundArrays;

    // Start보다 먼저 실행
    void Awake() {

        // 포인트의 자식을 배열로 지정
        GroundArrays = new Transform[transform.childCount];

        // 포인트 배열만큼 반복
        for (int i = 0; i < GroundArrays.Length; i++)
        {

            // 자식을 i로 지정
            GroundArrays[i] = transform.GetChild(i);
        }
    }
}