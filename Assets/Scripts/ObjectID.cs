using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    public int id;

    //  I want to objects to be kept in the specific area in the game.
    //They should be stay between the walls (the up left corner is = (x = 356, z = -1023), the bottom right corner is = (x = 420, z = -1170))

    void Update()
    {
        if(transform.position.x < 356)
        {
            transform.position = new Vector3(356, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 420)
        {
            transform.position = new Vector3(420, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -1170)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1170);
        }
        if (transform.position.z > -1023)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1023);
        }
    }
}
