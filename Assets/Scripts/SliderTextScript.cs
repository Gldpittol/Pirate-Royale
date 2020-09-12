using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextScript : MonoBehaviour
{
    public Text timeText;
    public Text spawnText;
    public Text audioText;


    public Slider timeSlider;
    public Slider spawnTimerSlider;
    public Slider audioSlider;

    private void Start()
    {
        if (StaticVariables.time == 0)
            StaticVariables.time = 90;
        if (StaticVariables.spawnDelay == 0)
            StaticVariables.spawnDelay = 3;
        if (StaticVariables.audioIntensity == 0)
            StaticVariables.audioIntensity = 1f;
        

        timeSlider.value = StaticVariables.time;
        spawnTimerSlider.value = StaticVariables.spawnDelay;
        audioSlider.value = StaticVariables.audioIntensity;
    }
    // Update is called once per frame
    void Update()
    {
        StaticVariables.time = timeSlider.value;
        StaticVariables.spawnDelay = spawnTimerSlider.value;
        StaticVariables.audioIntensity = audioSlider.value;

        timeText.text = "Session Time (Current: " + StaticVariables.time + "s)";
        spawnText.text = "Spawn Time (Current: " + StaticVariables.spawnDelay.ToString("F1") + "s)";
        audioText.text = "Audio Volume (Current: " + (StaticVariables.audioIntensity * 100).ToString("F0")  + "%)";
    }
}
