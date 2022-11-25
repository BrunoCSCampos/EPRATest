using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collector : MonoBehaviour
{

    public FixedJoystick collectorJoystick;
    Vector3 movementVelocity;

    public float movementSpeed;
    public float interactRange;

    public int collectorHealth = 3;

    public bool canMove = true;
    public bool canInteract = true;

    //Makes sure player is only interacting with ammo on pick-up attempt
    public LayerMask objectMask;

    public Game_Manager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        collectorJoystick = GameObject.FindGameObjectWithTag("Collector_Joystick").GetComponent<FixedJoystick>();
        gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LimitMovement();
    }

    //Moves at set speed each second
    public void Movement()
    {
        if (canMove == true)
        {
            movementVelocity = new Vector3(collectorJoystick.Horizontal, 0f, collectorJoystick.Vertical);
            Vector3 movementInput = new Vector3(movementVelocity.x, 0f, movementVelocity.z);
            Vector3 movementDirection = movementInput.normalized * movementSpeed;
            transform.Translate(movementDirection * Time.deltaTime);
        }
        
    }

    //Repositions player if he attempts to leave stage's bounds
    public void LimitMovement()
    {
        if (transform.position.x <= -2.5f)
        {
            transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
        }
        else if(transform.position.x >= 2.5f)
        {
            transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
        }

        if(transform.position.z >= 4.6f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 4.6f);
        }

        else if(transform.position.z <= -4.7f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -4.7f);
        }
    }

    //Attempts to pick-up any ammo in range;
    public void PickObjectUp()
    {
        if (canInteract == true)
        {
            Collider[] inRangeObjects = Physics.OverlapSphere(transform.position, interactRange, objectMask);

            foreach (Collider interactable in inRangeObjects)
            {
                Ammo ammoScript = interactable.transform.GetComponent<Ammo>();
                ammoScript.isPickedUp = true;
                Debug.Log("Collector has picked an object up.");
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    //Receives damage from Game_Manager
    //Deducts collectorHealth accordingly
    //Sends a gameOver request to Game_Manager depending on collectorHealth
    public void Damage(int damage)
    {
        if (collectorHealth > 1)
        {
            collectorHealth = collectorHealth - damage;
        }
        else if(collectorHealth <= 1)
        {
            collectorHealth = collectorHealth - damage;
            gameManager.GameOver();
        }
    }
}
