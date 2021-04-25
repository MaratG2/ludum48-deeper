using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxTimeSpawn = 50f;
    [SerializeField] private float minTimeSpawn = 10f;
    [SerializeField] private float harvestSpawnChance = 50f;
    [SerializeField] private float spawnX = 2.8f;
    [SerializeField] private float spawnYShift = 10f;
    [SerializeField] private float timeSpawn;
    private float timerSpawn;
    private float harvestChance;
    private bool isRandomedTimer;
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

        if (!isRandomedTimer)
        {
            timeSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);
            isRandomedTimer = true;
        }
        else
        {
            if (timerSpawn < timeSpawn)
                timerSpawn += Time.deltaTime;
            else
                SpawnEnemy(); 
        }

        if(player.isHarvesting)
        {
            if(!isRandomedChance)
            {
                harvestChance = Random.Range(0f, 100f);
                isRandomedChance = true;
            }
            else if(tempCrystal != player.crystal)
            {
                tempCrystal = player.crystal;
                if (harvestChance >= harvestSpawnChance)
                    SpawnEnemy();
            }
        }
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
        FindObjectOfType<Player>().TakeDamage(1000f);
    }

    public void SpawnEnemy()
    {
        timerSpawn = 0f;
        isRandomedTimer = false;
        Vector2 spawnPos = new Vector2(spawnX, player.transform.position.y - spawnYShift); 
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
