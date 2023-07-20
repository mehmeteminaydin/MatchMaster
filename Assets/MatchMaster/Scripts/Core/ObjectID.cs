using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    // I have assigned the id's to the objects in the inspector. 
    //Therefore, if I change the name, I need to assign the ids again.
    public int id;

    // the up left corner is = (x = 356, z = -1023), the bottom right corner is = (x = 420, z = -1170)
    // object does not cross the plane which is at y = 526

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
        if (transform.position.y < 531)
        {
            transform.position = new Vector3(transform.position.x, 540, transform.position.z);
        }
    }
}
