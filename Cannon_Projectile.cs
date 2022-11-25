using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Projectile : MonoBehaviour
{
    public float moveSpeed;

    //projectileId = 0(organic); projectileId = 1(metal); projectileId = 2(plastic); projectileId = 3(boss)
    public int projectileId;
   
    // Update is called once per frame
    void Update()
    {
        Movement();
        LimitPosition();
    }

    //Moves at set speed each second
    public void Movement()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    //Destroys gameObject if it leaves stage's bounds
    public void LimitPosition()
    {
        if(transform.position.z >= 7 || transform.position.x >= 4 || transform.position.x <= -4)
        {
            SelfDestruct();
        }
    }


}
