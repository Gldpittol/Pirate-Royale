using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private int controlScheme;

    public bool frontalFireAvailable = true;
    public bool sideFireAvailable = true;

    public GameObject cannonFront;
    public GameObject cannonSide1_1;
    public GameObject cannonSide1_2;
    public GameObject cannonSide1_3;
    public GameObject cannonSide2_1;
    public GameObject cannonSide2_2;
    public GameObject cannonSide2_3;

    private GameControllerScript gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        controlScheme = StaticVariables.controlScheme;
    }

    void Update()
    {
        if(!gc.gameOver)
        {
            if(controlScheme == 0)
            {
                if (Input.GetButtonDown("Fire1") && frontalFireAvailable)
                {
                    frontalFireAvailable = false;
                    StartCoroutine(FrontalFire());
                }

                if (Input.GetButtonDown("Fire2") && sideFireAvailable)
                {
                    sideFireAvailable = false;
                    StartCoroutine(SideFire());
                }
            }
            if (controlScheme == 1)
            {
                if (Input.GetKeyDown(KeyCode.Z) && frontalFireAvailable)
                {
                    frontalFireAvailable = false;
                    StartCoroutine(FrontalFire());
                }

                if (Input.GetKeyDown(KeyCode.X) && sideFireAvailable)
                {
                    sideFireAvailable = false;
                    StartCoroutine(SideFire());
                }
            }      
        }
    }

    public IEnumerator FrontalFire()
    {
        Instantiate(gc.cannonBullet, cannonFront.transform.position, transform.rotation);
        gc.PlayAudio("Shoot", 0.3f);

        yield return new WaitForSeconds(gc.playerAttackFrontDelay);
        frontalFireAvailable = true;
    }

    public IEnumerator SideFire()
    {
        Instantiate(gc.cannonBullet, cannonSide1_1.transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90));
        Instantiate(gc.cannonBullet, cannonSide2_1.transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 90));
        gc.PlayAudio("Shoot", 0.3f);
        yield return new WaitForSeconds(0.05f);
        Instantiate(gc.cannonBullet, cannonSide2_3.transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 90));
        Instantiate(gc.cannonBullet, cannonSide1_3.transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90));
        gc.PlayAudio("Shoot", 0.3f);
        yield return new WaitForSeconds(0.2f);
        Instantiate(gc.cannonBullet, cannonSide1_2.transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90));
        Instantiate(gc.cannonBullet, cannonSide2_2.transform.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 90));
        gc.PlayAudio("Shoot", 0.3f);

        yield return new WaitForSeconds(gc.playerAttackSideDelay);
        sideFireAvailable = true;
    }
}
