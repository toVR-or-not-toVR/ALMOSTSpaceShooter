using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField]  float respawnTime;
    private Vector2 screenBounds;
    [SerializeField] float minTimeSpawn = 1f;
    [SerializeField] float maxTimeSpawn = 10f;

    // Start is called before the first frame update
    void Start()
    {
        respawnTime = Random.Range(minTimeSpawn, maxTimeSpawn);
        float check = Random.Range(1, 10);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        
    }

    private void spawnEnemy()
    {
        GameObject asteroid = Instantiate(asteroidPrefab) as GameObject;
        asteroid.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y *2);
    }

    IEnumerator asteroidWave(float respawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndSpawn();
    }

    private void CountDownAndSpawn()
    {
        respawnTime -= Time.deltaTime;
        if (respawnTime <= 0f)
        {
            spawnEnemy();
            respawnTime = Random.Range(minTimeSpawn, maxTimeSpawn);
        }
    }
}
