using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public GameControllerScript gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    private void Update()
    {
        if(gc.timeAvailable > 0)
        {
            gc.timeAvailable -= Time.deltaTime;
            gc.timeText.text = "Time Remaining: " + gc.timeAvailable.ToString("F0") + "s";
        }
        else if(!gc.gameOver)
        {
            gc.gameOver = true;
            gc.GameOver();
        }
    }
}
