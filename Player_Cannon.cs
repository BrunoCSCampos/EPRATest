using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cannon : MonoBehaviour
{
    public FixedJoystick cannonJoystick;

    //projectilePrefab[0] = organic; projectilePrefab[1] = metal; projectilePrefab[2] = plastic
    public GameObject[] projectilePrefab;
    public Game_Manager gameManager;

    //equippedAmmo[0] = organic; equippedAmmo[1] = metal; equippedAmmo[2] = plastic
    public bool[] equippedAmmo;
    public bool canMove = true;
    public bool canInteract = true;
    Vector3 rotationVelocity;


    // Start is called before the first frame update
    void Start()
    {
        cannonJoystick = GameObject.FindGameObjectWithTag("Cannon_Joystick").GetComponent<FixedJoystick>();
        gameManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        LimitRotation();
    }

    //Rotates cannon at set speed depending on joystick movement
    public void Rotation()
    {
        if (canMove == true)
        {
            rotationVelocity = new Vector3(cannonJoystick.Horizontal, 0f, cannonJoystick.Vertical);
            Vector3 rotationInput = new Vector3(rotationVelocity.x, 0f, rotationVelocity.z);
            Vector3 rotationDirection = transform.position + rotationInput;
            transform.LookAt(rotationDirection);
        }
    }

    //Repositions cannon rotation if it attempts to point backwards
    public void LimitRotation()
    {
        if(transform.eulerAngles.y >= 90 && transform.eulerAngles.y <=200)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        }

        else if(transform.eulerAngles.y <= 270 && transform.eulerAngles.y >= 90)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        }
    }

    //Receives ammoId from UI buttons
    //Sets all possible equippedAmmo to false
    //Equips ammo according to pressed button
    public void ChangeAmmo(int ammoId)
    {
        if (canInteract == true)
        {
            for (int i = 0; i < equippedAmmo.Length; i++)
            {
                equippedAmmo[i] = false;
            }

            if (ammoId == 0)
            {
                Debug.Log("Organic Ammo Equipped");
                equippedAmmo[0] = true;

            }
            if (ammoId == 1)
            {
                Debug.Log("Metal Ammo Equipped");
                equippedAmmo[1] = true;

            }
            if (ammoId == 2)
            {
                Debug.Log("Plastic Ammo Equipped");
                equippedAmmo[2] = true;
            }
        }
    }

    //Checks for equippedAmmo type
    //Checks if player has enough ammo of said type
    //Shoots projectile accordingly
    public void ShootProjectile()
    {
        if (canInteract == true)
        {
            if (equippedAmmo[0] == true && gameManager.currentAmmo[0] > 0)
            {
                Instantiate(projectilePrefab[0], transform.position, transform.rotation);
                gameManager.SpendAmmo(0);
            }
            else if (equippedAmmo[1] == true && gameManager.currentAmmo[1] > 0)
            {
                Instantiate(projectilePrefab[1], transform.position, transform.rotation);
                gameManager.SpendAmmo(1);
            }
            else if (equippedAmmo[2] == true && gameManager.currentAmmo[2] > 0)
            {
                Instantiate(projectilePrefab[2], transform.position, transform.rotation);
                gameManager.SpendAmmo(2);
            }
            else
            {
                Debug.Log("Out of ammo");

                //In case of a default shot being implemented
                //Make equippedAmmo[0] false on the inspector
                //Create a behavior for default ammo
            }
        }
    }
}
