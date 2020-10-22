using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 2f;
    
    public Transform[] spawnPoints;

    public GameObject enemyPrefab;

    public float dificultyRate = 10f;
    void Start()
    {
        if(Time.time % spawnRate == 0)
        {
            StartCoroutine(SpawnEnemy());
        }

        StartCoroutine(Dif());
        
    }
    IEnumerator Dif()
    {

        yield return new WaitForSeconds(dificultyRate);
        if(spawnRate > 0.5f)
            spawnRate -= 0.5f;
        StartCoroutine(Dif());
    }
    IEnumerator SpawnEnemy()
    {
        int index = Random.Range(0,spawnPoints.Length-1);
        GameObject clone = Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
        clone.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player").transform;
        clone.GetComponent<Enemy>().firePoint = clone.GetComponentInChildren<Transform>();
        
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnEnemy());
    }
}
