using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Alien : MonoBehaviour
{
    //enemyId = 0(organic); enemyId = 1(metal); enemyId = 2(plastic); enemyId = 3(boss)
    public int enemyId;
    public int alienHP = 3;
    public float moveSpeed;
    public bool canDropAmmo = true;
    public bool canMove = true;

    public Game_Manager gameManager;

    //ammoPrefab[0] = organic; ammoPrefab[1] = metal; ammoPrefab[2] = plastic
    public GameObject[] ammoPrefab;

    //public bool nowChasing = false;
    //public Layer_Mask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DropAmmoRoutine");
        gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //ChasePlayer logic was left as a comment to ensure a better testing and showcase of the game
        //Uncomment nowChasing and playerMask handles 
        //Uncomment all logic contained in the Update and ChasePlayer methods 
        //Assign a Player layer to both Player_Collector and playerMask in the inspector to reintegrate it into the game

        //if(nowChasing == false)
        //{
        Movement();
        //}
        //else if(nowChasing == true)
        //{
        //ChasePlayer();
        //}
    }

    //public void ChasePlayer()
    //{
    // if (canMove == true)
    //        {
    //            Collider[] detectRange = Physics.OverlapSphere(transform.position, detectRadius, playerMask);
    //            foreach (Collider player in detectRange)
    //            {
    //                Debug.Log("Player has entered range.");
    //                var playerScript = player.GetComponent<Player_Ship>();
    //                if (playerScript != null)
    //                {
                          //nowChasing == true
    //                    transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, moveSpeed * Time.deltaTime);
    //                }   
    //                  
    //         }
    //}

    public void Movement()
    {
        if (canMove == true)
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
    }

    //Drops specific ammo type depending on enemy type when called
    public void DropAmmo(Vector3 dropPosition)
    {
        if (enemyId <= 2)
        {
            Instantiate(ammoPrefab[0], dropPosition, Quaternion.identity);
        }
        else if(enemyId == 3)
        {
            Instantiate(ammoPrefab[Random.Range(0, 3)], dropPosition, Quaternion.identity);
        }
    }

    //Call Drop ammo at different intervals
    //Depending if enemy is a boss or not
    public IEnumerator DropAmmoRoutine()
    {
        while (canDropAmmo == true)
        {
            if (enemyId <= 2)
            {
                yield return new WaitForSeconds(1.5f);
                DropAmmo(transform.position);
            }
            else if(enemyId == 3)
            {
                yield return new WaitForSeconds(1f);
                DropAmmo(transform.position);
            }
        }
        
    }

    //Relays both members of the collision to Game_Manager
    //Lets Game_Manager decide how to handle the interaction
    public void OnTriggerEnter(Collider other)
    {
        if (enemyId <= 2)
        {
            Debug.Log("Alien has collided with other:" + other.name);
        }
        else if(enemyId == 3)
        {
            Debug.Log("Boss has collided with other:" + other.name);
        }

        if (other.tag == "Collector")
        {
         gameManager.CollectorLife(other.gameObject, this.transform.gameObject);
        //SwallowCollectorRoutine;
        }
        else if (other.tag == "Wall")
        {
         gameManager.WallLife(other.gameObject, this.transform.gameObject);
         SelfDestruct();
        }
        else if (other.tag == "Projectile")
        {
         gameManager.ProjectileBehavior(other.gameObject, this.transform.gameObject);
        }
    }

    //Receives damage from Game_Manager
    //Deducts alienHP accordingly
    //Self Destructs depending on alienHealth
    public void Damage(int damage)
    {
        if (alienHP > 1)
        {
            alienHP = alienHP - damage;
        }
        else if(alienHP <= 1)
        {
            
            for (int i = 3; i >= 1; i--)
            {
                Vector3 ammoErupt = new Vector3((transform.position.x + Random.Range(-1, 1)), transform.position.y, (transform.position.z + Random.Range(-1, 1)));
                DropAmmo(ammoErupt);
            }
            SelfDestruct();
        }
    }

    //Stops dropping ammo while walking
    //Destroys the gameObject
    public void SelfDestruct()
    {

        StopCoroutine("DropAmmoRoutine");
        Destroy(this.gameObject);
    }
}
