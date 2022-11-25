using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public bool gameOver = false;
    public UI_Manager uiManager;
    public Player_Collector playerCollector;
    public Player_Cannon playerCannon;
    public Wall playerWall;

    //currentAmmo[0] = organic; currentAmmo[1] = metal; currentAmmo[2] = plastic
    public int[] currentAmmo;
    //Handle for all alien enemies currently in the Scene
    public GameObject[] enemyAliens;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UI_Manager").GetComponent<UI_Manager>();
    }

    //Receives ammoType from Player_Cannon
    //Deducts available ammo and updates UI accordingly
    public void SpendAmmo(int ammoType)
    {
        currentAmmo[ammoType] = currentAmmo[ammoType] - 1;
        uiManager.UpdateAmmoCount(ammoType);
    }

    //Receives ammoType from Ammo
    //Adds available ammo and updates UI accordingly
    public void GainAmmo(int ammoType)
    {
        currentAmmo[ammoType] = currentAmmo[ammoType] + 1;
        uiManager.UpdateAmmoCount(ammoType);
    }

    //Receives both alien and collector from Enemy_Alien on collision
    //Relays the damage to Player_Collector depending on alien type
    //Updates the UI accordingly
    public void CollectorLife(GameObject collector, GameObject alien)
    {
        Player_Collector collectorScript = collector.transform.GetComponent<Player_Collector>();
        Enemy_Alien enemyScript = alien.transform.GetComponent<Enemy_Alien>();
        if (enemyScript.enemyId < 2)
        {
            collectorScript.Damage(1);
        }
        else if(enemyScript.enemyId == 3)
        {
            collectorScript.Damage(3);
        }
        uiManager.UpdateCollectorLives(collectorScript);
    }

    //Receives both alien and wall from Enemy_Alien on collision
    //Relays the damage to Wall depending on alien type
    //Updates the UI accordingly
    public void WallLife(GameObject wall, GameObject alien)
    {
        Wall wallScript = wall.transform.GetComponent<Wall>();
        Enemy_Alien enemyScript = alien.transform.GetComponent<Enemy_Alien>();

        if (enemyScript.enemyId < 2)
        {
            wallScript.Damage(1);
        }
        else if (enemyScript.enemyId == 3)
        {
            wallScript.Damage(5);
        }
        uiManager.UpdateWallLives(wallScript);
    }
    //Receives both alien and projectile from Enemy_Alien on collision
    //Relays the damage to Enemy_Alien depending on projectile type
    //Destroys the projectile if damage was dealt to alien
    public void ProjectileBehavior(GameObject projectile, GameObject alien)
    {
        Cannon_Projectile projectileScript = projectile.transform.GetComponent<Cannon_Projectile>();
        Enemy_Alien enemyScript = alien.transform.GetComponent<Enemy_Alien>();

        if (projectileScript.projectileId == 0)
        {
            if (enemyScript.enemyId == 1 || enemyScript.enemyId == 2)
            {
                enemyScript.Damage(1);
                projectileScript.SelfDestruct();
            }
            else if (enemyScript.enemyId == 3)
            {
                enemyScript.Damage(1);
                projectileScript.SelfDestruct();
            }
        }

        else if (projectileScript.projectileId == 1 || projectileScript.projectileId == 2)
        {
            if (enemyScript.enemyId == 0)
            {
                enemyScript.Damage(1);
                projectileScript.SelfDestruct();
            }
            else if (enemyScript.enemyId == 3)
            {
                enemyScript.Damage(1);
                projectileScript.SelfDestruct();
            }
        }
        
    }

    //Accesses all objects and stops any interactions among them
    //Sets all Spawn Waves to false and stop spawning new aliens
    //Starts the MainMenu Routine
    public void GameOver()
    {
        gameOver = true;

        playerCollector = GameObject.FindGameObjectWithTag("Collector").GetComponent<Player_Collector>();
        if(playerCollector != null)
        {
            playerCollector.canMove = false;
            playerCollector.canInteract = false;
        }

        playerCannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Player_Cannon>();
        if (playerCannon != null)
        {
            playerCannon.canMove = false;
            playerCannon.canInteract = false;
        }

        enemyAliens = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemyAliens)
        {
            Enemy_Alien enemyScript = enemy.GetComponent<Enemy_Alien>();
            if (enemyScript != null)
            {
                enemyScript.canMove = false;
                enemyScript.canDropAmmo = false;
            }
        }

        Spawn_Manager spawnManager = GameObject.FindGameObjectWithTag("Spawn_Manager").GetComponent<Spawn_Manager>();
        spawnManager.StopAllCoroutines();
        for(int i = 0; i < spawnManager.startWave.Length; i++)
        {
            spawnManager.startWave[i] = false;
        }
        StartCoroutine("LoadMainMenu");   
    }

    //Automatically closes Stage1Scene
    //Sends players back to Main Menu after 1 second
    public IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        StopCoroutine("LoadMainMenu");
    }
   
     

}
