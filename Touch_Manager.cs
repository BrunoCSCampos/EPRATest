using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Manager : MonoBehaviour
{
    //Handle for where in the screen player has touched most recently
    public Vector3 touchPressPosition;
    public Player_Cannon playerCannon;
    public Player_Collector playerCollector;

    // Start is called before the first frame update
    void Start()
    {
        playerCannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Player_Cannon>();
        playerCollector = GameObject.FindGameObjectWithTag("Collector").GetComponent<Player_Collector>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectTouch();
    }

    //Attempts to shoot Player_Cannon if player touches the left-side of the screen
    //Attempts to pick-up ammo nearby Player_Collector if player touches the right-side of the screen
    public void DetectTouch()
    {
      foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                touchPressPosition = touch.position;

                if (touchPressPosition.x > 0 && touchPressPosition.x <= 85 && touchPressPosition.y >= 60 && touchPressPosition.y <= 300)
                {
                    Debug.Log("Cannon has fired.");
                    playerCannon.ShootProjectile();
                }
                else if (touchPressPosition.x <= 165 && touchPressPosition.x > 85 && touchPressPosition.y >= 60 && touchPressPosition.y <= 300)
                {
                    Debug.Log("Attempting to pick object...");
                    playerCollector.PickObjectUp();
                }
            }
            
        }
    }
}
