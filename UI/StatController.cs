using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour {

    public Text AttackDamageLabel;
    public Text AttackSpeedLabel;
    public Text BlockDamageLabel;
    public Text HealthLabel;
    public Text WaveLabel;
    public Text CoinLabel;
    public Text ScoreLabel;

    private float AttackDamageStat;
    private float AttackSpeedstat;
    private float BlockDamageStat;
    private float HealthStat;
    private float WaveStat;
    private float CoinStat;
    private float ScoreStat;

    private Spawner spawner;
    private StatManager statmanager;
    private Key key;

    // 맨처음 시작시
    void Awake() {

        // 스크립트를 가져옴
        spawner = GameObject.FindObjectOfType<Spawner>();
        key = GameObject.FindObjectOfType<Key>();
        statmanager = GameObject.FindObjectOfType<StatManager>();
    }

    void Update() {

        AttackDamageStat = statmanager.tower_powers;
        AttackDamageLabel.text = AttackDamageStat.ToString();

        AttackSpeedstat = statmanager.tower_speeds;
        AttackSpeedLabel.text = AttackSpeedstat.ToString();

        BlockDamageStat = statmanager.block_powers;
        BlockDamageLabel.text = BlockDamageStat.ToString();

        HealthStat = key.health;
        HealthLabel.text = HealthStat.ToString();

        WaveStat = spawner.currentWaveNumber;
        WaveLabel.text = WaveStat.ToString();

        CoinStat = statmanager.coins;
        CoinLabel.text = CoinStat.ToString();

        ScoreStat = statmanager.scores;
        ScoreLabel.text = ScoreStat.ToString();
    }
}
