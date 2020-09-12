using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCannon : MonoBehaviour
{
    public float cannonRotation;

    private GameControllerScript gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        StartCoroutine(CannonFire());
    }

  

    public IEnumerator CannonFire()
    {
        if (this.GetComponent<MeshRenderer>().isVisible)
        {
            Instantiate(gc.enemyCannonBullet, transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, cannonRotation));
            gc.PlayAudio("Shoot", 0.1f);
        }

        yield return new WaitForSeconds(gc.fixedCannonBulletDelay);
        StartCoroutine(CannonFire());
    }

}
