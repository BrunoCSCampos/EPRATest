using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    //Canvas object to access its children without repeating the method twice.
    public GameObject canvasObject;
    public Game_Manager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvasObject = transform.GetChild(0).gameObject;
        gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    //Only changes the visual text regarding ammo count
    //Receives parameters according to Game_Manager
    public void UpdateAmmoCount(int ammoType)
    {
        
        if (ammoType == 0)
        {
            TMP_Text organicCounter = canvasObject.transform.GetChild(0).GetComponent<TMP_Text>();
            organicCounter.text = gameManager.currentAmmo[0].ToString();
        }
        else if (ammoType == 1)
        {
            TMP_Text metalCounter = canvasObject.transform.GetChild(1).GetComponent<TMP_Text>();
            metalCounter.text = gameManager.currentAmmo[1].ToString();
        }
        else if (ammoType == 2)
        {
            TMP_Text plasticCounter = canvasObject.transform.GetChild(2).GetComponent<TMP_Text>();
            plasticCounter.text = gameManager.currentAmmo[2].ToString();
        }
    }

    //Only changes the visual text regarding collector lives
    //Receives parameters according to Game_Manager
    public void UpdateCollectorLives(Player_Collector collector)
    {
        Text livesCounter = canvasObject.transform.GetChild(3).GetComponent<Text>();
        livesCounter.text = "LIVES: " + collector.collectorHealth.ToString();
    }

    //Only changes the visual text regarding wall lives
    //Receives parameters according to Game_Manager
    public void UpdateWallLives(Wall wall)
    {
        TMP_Text wallCounter = canvasObject.transform.GetChild(4).GetComponent<TMP_Text>();
        wallCounter.text = wall.wallHealth.ToString();

    }
  
}
