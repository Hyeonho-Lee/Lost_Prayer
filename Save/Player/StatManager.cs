using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {

    public float stage_waves;
    public float healths;
    public float block_powers;
    public float tower_powers;
    public float tower_speeds;
    public float coins;
    public float enemycounts;
	public float scores;

    public int shopindex;

	//private AutoTower autotower;

	void Awake() {

		//autotower = GameObject.FindObjectOfType<AutoTower> ();
		//Load();
	}

    public void ScoreReset() {

        stage_waves = 0;
        enemycounts = 0;
        scores = 0;
        coins = 0;
		block_powers = 1;
		tower_powers = 1;
		tower_speeds = 2;
    }

    public void Save() {
		
        SaveLoadManager.SavePlayer(this);
    }

    public void Load() {
		
		//autotower.AttackDamages = tower_powers ;
		//autotower.fireRate = tower_speeds;
		//autotower.BlockDamages = block_powers;

        float[] loadedInfos = SaveLoadManager.LoadPlayer();

        stage_waves = loadedInfos[0];
        healths = loadedInfos[1];
        block_powers = loadedInfos[2];
        tower_powers = loadedInfos[3];
        tower_speeds = loadedInfos[4];
        coins = loadedInfos[5];
        enemycounts = loadedInfos[6];
		scores = loadedInfos [7];
    }
}
