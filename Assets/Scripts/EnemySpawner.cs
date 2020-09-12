using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int whereToSpawn;
    private int shipToSpawn;
    private int maxIterations;

    public GameObject[] spawnLocations;
    private GameObject tempShip;
    private GameObject tempHealthBar;

    private GameControllerScript gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        StartCoroutine(SpawnEnemy());
    }



    public IEnumerator SpawnEnemy()
    {
        shipToSpawn = Random.Range(0, gc.enemy.Length);
        maxIterations = 0;

        do
        {
            maxIterations += 1;
            whereToSpawn = Random.Range(0, spawnLocations.Length);
        } 
        while ((spawnLocations[whereToSpawn].GetComponent<MeshRenderer>().isVisible || spawnLocations[whereToSpawn].GetComponent<AllowSpawn>().collisionCount != 0) && maxIterations < 30);

        if (maxIterations < 30)
        {
            tempShip = Instantiate(gc.enemy[shipToSpawn], spawnLocations[whereToSpawn].transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, Random.Range(0, 360)));
            tempHealthBar = Instantiate(gc.enemyHealthBar[shipToSpawn], transform.position, Quaternion.Euler(0, 0, 90));
            tempHealthBar.GetComponent<HealthBarScript>().AttachHealthBar(tempShip.gameObject);

            if (tempShip.gameObject.CompareTag("Chaser"))
            {
                tempShip.GetComponent<ChaserScript>().HealthBar = tempHealthBar;
            }
            if (tempShip.gameObject.CompareTag("Shooter"))
            {
                tempShip.GetComponent<ShooterScript>().HealthBar = tempHealthBar;
            }
        }
        
        yield return new WaitForSeconds(gc.spawnDelay);
        StartCoroutine(SpawnEnemy());

    }
}
