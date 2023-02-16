using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour {

	// 저장 함수
    public static void SavePlayer(StatManager player) {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(stream, data);
        stream.Close();
    }

	// 불러오는 함수
    public static float[] LoadPlayer(){

        if(File.Exists(Application.persistentDataPath + "/player.sav")) {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.infos;
        }else {

            Debug.LogError("File does not exits.");
            return new float[8];
        }
    }
}

// 플레이어 데이터
[Serializable]
public class PlayerData {

    public float[] infos;

    public PlayerData(StatManager player) {

        infos = new float[8];
        infos[0] = player.stage_waves;
        infos[1] = player.healths;
        infos[2] = player.block_powers;
        infos[3] = player.tower_powers;
        infos[4] = player.tower_speeds;
        infos[5] = player.coins;
        infos[6] = player.enemycounts;
		infos[7] = player.scores;
    }
}
