using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int wallHealth = 20;
    public Game_Manager gameManager;

    // Start is called before the first frame update
    void Start()
    {
     gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    //Receives damage from Game_Manager
    //Deducts wallHealth accordingly
    //Sends a gameOver request to Game_Manager depending on wallHealth
    public void Damage(int damage)
    {
        if (wallHealth > 1)
        {
            wallHealth = wallHealth - damage;
        }
        else if(wallHealth <= 1)
        {
            wallHealth = wallHealth - damage;
            gameManager.GameOver();
           
        }
    }
}
