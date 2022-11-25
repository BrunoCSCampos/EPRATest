using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //ammoId = 0(organic); ammoId = 1(metal); ammoId = 2(plastic)
    public int ammoId;
    public bool isPickedUp = false;

    public Player_Collector playerCollector;
    public Game_Manager gameManager;

    //Handle for all bins currently in the Scene
    public GameObject[] recycleBins;

    // Start is called before the first frame update
    void Start()
    {
       gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    public void Update()
    {
        Movement(); 
    }

    //Changes its state when picked up by Player_Collector
    public void CollectorPickUp()
    {
        isPickedUp = true;
    }

    //Depending on its state
    //Changes position aoccrding to Player_Collector's
        public void Movement()
        {
        if (isPickedUp == true)
        {
            playerCollector = GameObject.FindGameObjectWithTag("Collector").GetComponent<Player_Collector>();
            transform.position = new Vector3(playerCollector.transform.position.x, 0.5f, playerCollector.transform.position.z);
        }
        }

    //Relays that ammo was delivered to each bin to Game_Manager
    //Self destructs when ammo is added
    public void OnTriggerEnter(Collider other)
    {
        
        if(other.transform.tag == "Recycle_Bin")
        {
            Recycle_Bin binScript = other.transform.GetComponent<Recycle_Bin>();

                  if (binScript != null && binScript.binId == ammoId)
                  {
                    Debug.Log("Gained ammo;");
                    if (ammoId == 0 && binScript.binId == 0)
                    {
                        gameManager.GainAmmo(0);
                     
                    }
                    else if (ammoId == 1 && binScript.binId == 1)
                    {
                        gameManager.GainAmmo(1);
                    }
                    else if (ammoId == 2 && binScript.binId == 2)
                    {
                        gameManager.GainAmmo(2);
                    }
                     Destroy(this.gameObject);
                  }
            

            

        }
        else if(other.transform.tag == "Collector")
        {
            Debug.Log("Ammo is in pickup range.");
            
        }
    }


}
