using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float[] depthsSpawn;
    [SerializeField] private float[] maxTimeSpawn;
    [SerializeField] private float[] minTimeSpawn;
    public int depthCounter = 0;
    private int wasDepthCounter = 0;
    [SerializeField] private float harvestSpawnChance = 50f;
    [SerializeField] private float spawnX = 2.8f;
    [SerializeField] private float spawnYShift = 10f;
    [SerializeField] private float timeSpawn;
    [HideInInspector] public float timerSpawn;
    [HideInInspector] public bool isRandomedTimer;
    private float harvestChance;
    private bool isRandomedChance;
    private Player player;
    private Crystal tempCrystal;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Application.targetFrameRate != 60)
            Application.targetFrameRate = 60;

        if (player.depth < depthsSpawn[0])
        {
            depthCounter = 0;
        }
        else if (player.depth >= depthsSpawn[0] && player.depth < depthsSpawn[1])
        {
            depthCounter = 1;
        }
        else if (player.depth >= depthsSpawn[1] && player.depth < depthsSpawn[2])
        {
            depthCounter = 2;
        }
        else if (player.depth >= depthsSpawn[2])
        {
            depthCounter = 3;
        }

        if (depthCounter != wasDepthCounter)
            isRandomedTimer = false;

        if (!isRandomedTimer)
        {
            timeSpawn = Random.Range(minTimeSpawn[depthCounter], maxTimeSpawn[depthCounter]);
            isRandomedTimer = true;
        }
        else
        {
            if (timerSpawn < timeSpawn)
                timerSpawn += Time.deltaTime;
            else
                SpawnEnemy();
        }

        if (player.isHarvesting)
        {
            if (!isRandomedChance)
            {
                harvestChance = Random.Range(0f, 100f);
                isRandomedChance = true;
            }
            else if (tempCrystal != player.crystal)
            {
                tempCrystal = player.crystal;
                if (harvestChance >= harvestSpawnChance)
                    SpawnEnemy();
            }
        }

        wasDepthCounter = depthCounter;
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<Player>().TakeDamage(1000f, false);
    }

    public void SpawnEnemy()
    {
        timerSpawn = 0f;
        isRandomedTimer = false;
        Vector2 spawnPos = new Vector2(spawnX, player.transform.position.y - spawnYShift); 
        GameObject enemy = Instantiate(enemyPrefab[depthCounter], spawnPos, Quaternion.identity);
    }
}
