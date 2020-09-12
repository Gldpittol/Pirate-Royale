using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowSpawn : MonoBehaviour
{
    public int collisionCount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chaser") || collision.gameObject.CompareTag("Shooter"))
        {
            collisionCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Chaser") || collision.gameObject.CompareTag("Shooter"))
        {
            collisionCount--;
        }
    }


}
