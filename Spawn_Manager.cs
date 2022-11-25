using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject bossAlienPrefab;

    //Initializes a list of aliens added to scene by each wave
    public List<GameObject> spawnedAliens = new List<GameObject>();
    public Game_Manager gameManager;

    //Switch for when a wave should start
    //Prevents same wave starting twice
    public bool[] startWave;

    //alienPrefab[0] = organic; alienPrefab[1] = metal; alienPrefab[2] = plastic; alienPrefab[3] = organic
    //organic is present twice to increase its frequency
    public GameObject[] alienPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        StartCoroutine("SpawnWave0");
    }

    //Spawns a random alienPrefab at the top of the screen
    //Adds it to spawnedAliens list
    public void SpawnAliens()
    {
        GameObject summonedAlien = alienPrefab[Random.Range(0, 4)];
        Instantiate(summonedAlien, new Vector3(Random.Range(-2.5f, 3f), 0, 7), Quaternion.identity);
        spawnedAliens.Add(summonedAlien);
    }

    //Spawns the bossAlienPrefab at the top of the screen
    public void SpawnBossAlien()
    {
        if (gameManager.gameOver == false)
        {
            Instantiate(bossAlienPrefab, new Vector3(0, 0, 7), Quaternion.identity);
        }
       
    }

    //SpawnAliens up to 6 times in 4 seconds intervals
    //Waits 10 seconds before starting next wave
    public IEnumerator SpawnWave0()
    {
        if (startWave[0] == true && gameManager.gameOver == false)
        {
            startWave[0] = false;
            while (spawnedAliens.Count <= 5)
            {
                yield return new WaitForSeconds(4f);
                SpawnAliens();
            }

            yield return new WaitForSeconds(10f);
            startWave[1] = true;
            spawnedAliens.Clear();
            StopCoroutine("SpawnWave0");
            StartCoroutine("SpawnWave1");
        }
                
    }

    //SpawnAliens up to 6 times in 3 seconds intervals
    //Waits 10 seconds before starting next wave
    public IEnumerator SpawnWave1()
    {
       if(startWave[1] == true && gameManager.gameOver == false)
       {
                startWave[1] = false;

            while (spawnedAliens.Count <= 5)
            {
                yield return new WaitForSeconds(3f);
                SpawnAliens();
            }

            yield return new WaitForSeconds(10f);
            startWave[2] = true;
            spawnedAliens.Clear();
            StopCoroutine("SpawnWave1");
            StartCoroutine("SpawnWave2");

        }

    }

    //Waits 3 seconds before spawning bossAlien
    
    public IEnumerator SpawnWave2()
    {
        if(startWave[2] == true && gameManager.gameOver == false)
        {
            startWave[2] = false;
            yield return new WaitForSeconds(3f);
            SpawnBossAlien();
            StopCoroutine("SpawnWave2");
        }
    }
}
