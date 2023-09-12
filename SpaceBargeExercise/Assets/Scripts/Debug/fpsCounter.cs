using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fpsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsLabel;
    private float currentFps = 0;
    private int takesCount = 10;
    private int fpsTakes;
    
    private float averageFpsTakes = 0;
    private float averageFps = 60;

    private float topFps = 0;
    private float lowestFps = 600;

    public float updateInterval = 0.1f;
    private float intervalHit = 0;
    private void UpdateText() => fpsLabel.text = $"FPS: {currentFps:N0}\n\nAverage FPS: {averageFps:N0}\n\nTop FPS: {topFps:N0}\nLowest FPS: {lowestFps:N0}";

    [TextArea]public string otherControls = 
@"Controls:

Tap/Click on a flier, to lock.
WSAD/Joystick - Movement.
";

    private void Update()
    {
        UpdateFpsInterval();
    }
    private void UpdateFpsInterval()
    {
        if (intervalHit <= Time.time)
        {
            intervalHit = Time.time + updateInterval;
            UpdateCurrentFps();
        }
    }

    void UpdateCurrentFps() 
    {
        currentFps = 1.0f / Time.deltaTime;
        UpdateText();
        if (fpsTakes < takesCount)
        {
            averageFpsTakes += currentFps;
            fpsTakes++;
        }
        else UpdateAverageFps();
        fpsLabel.text += "\n" + otherControls;
    }

    void UpdateAverageFps() 
    {
        averageFps = averageFpsTakes / 10.0f;
        if (averageFps > topFps)
            topFps = averageFps;
        if (averageFps < lowestFps)
            lowestFps = averageFps;
        UpdateText();
        fpsTakes = 0;
        averageFpsTakes = 0;
    }
}
