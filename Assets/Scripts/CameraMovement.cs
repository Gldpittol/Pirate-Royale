using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    private GameControllerScript gc;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        player = GameObject.FindGameObjectWithTag("Player");

        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gc.gameOver)
            transform.position = player.transform.position + offset;
    }
}
