using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] int monstersToSpawn = 3;
    private int enemyCount;

    [SerializeField] GameObject victoryPopup, losePopup;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        SpawnMonsters();
    }

    private void SpawnMonsters() {
        for (int i = 0; i < monstersToSpawn; i++) {
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition() {
        float x = Random.Range(-13f, 13f);
        float y = Random.Range(-7f, 7f);
        return new Vector3(x, y, 0);
    }

    public void RegisterEnemy() {
        enemyCount++;
    }

    public void EnemyKilled() {
        enemyCount--;
        if (enemyCount <= 0) WinGame();
    }

    public void PlayerDied() {
        LoseGame();
    }

    private void WinGame() {
        victoryPopup.SetActive(true);
    }

    private void LoseGame() {
        losePopup.SetActive(true);
    }

    public void NextScene() {
        SceneManager.LoadScene(0);
    }
}

