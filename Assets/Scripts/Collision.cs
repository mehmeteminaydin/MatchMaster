using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("cube"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
