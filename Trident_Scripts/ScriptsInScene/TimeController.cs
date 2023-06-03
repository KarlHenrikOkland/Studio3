using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{

    [SerializeField]
    private float timeMultiplier;
    
    [SerializeField]
    private float startHour;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private DateTime currentTime;
    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private TimeSpan sunriseTime;

    [SerializeField]
    private TimeSpan sunsetTime;

    [SerializeField]
    private Color dayAL;

    [SerializeField]
    private Color nightAL;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonIntensity;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        LightSettings();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
       float sunRotation;

       if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime) 
       {
        TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
        TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

        double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

        sunRotation = Mathf.Lerp(0, 180, (float)percentage);
       }
       else
       {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunRotation = Mathf.Lerp(180, 360, (float)percentage);
       }

       sunLight.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
    }

    private void LightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientSkyColor = Color.Lerp(nightAL, dayAL, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }
}
