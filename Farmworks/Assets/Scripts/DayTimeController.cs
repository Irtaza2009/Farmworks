using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;

    [SerializeField] Color nightLightColour;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColour = Color.white;
    [SerializeField] float timeScale = 60f;

    [SerializeField] Light2D globalLight;
    float time;

    private int days;

    public float Hours
    {
        get
        {
            return time / 3600f;
        }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColour, nightLightColour, v);

        globalLight.color = c;

        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
