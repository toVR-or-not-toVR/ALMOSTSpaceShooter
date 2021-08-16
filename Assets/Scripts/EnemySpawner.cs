using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool isEnemy = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    private void Update()
    {
        StartCoroutine(WaitOneSecond());
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
        IsEnemeInScene();
        Debug.Log(isEnemy);
    }

    void IsEnemeInScene()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            isEnemy = true;
        }
        else
        {
            isEnemy = false;
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];

            yield return new WaitUntil(() => !isEnemy);
          
            StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        
         
        }
    }



    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            isEnemy = true;
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

       
    }

}
