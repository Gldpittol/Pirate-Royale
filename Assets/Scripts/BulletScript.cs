using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;

    private GameControllerScript gc;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        if (gameObject.CompareTag("Bullet"))
        {
            bulletSpeed = gc.playerBulletSpeed;
        }
        if (gameObject.CompareTag("EnemyBullet"))
        {
            bulletSpeed = gc.enemyBulletSpeed;
        }
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;

        Destroy(this.gameObject, 3);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
