using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.ShaderGraph;
using UnityEngine.Rendering.Universal;

public class WeatherSystem : MonoBehaviour
{
    public float countDownTimer = 300f;
    public Material shader;

    private float weatherTimer = 0f;
    
    private float lerpTimer = 0f;
    public float lerpDuration = 10f;

    public enum WeatherType
    {
        ClearSkies,
        PartlyCloudy,
        PartlyOvercast,
        Overcast
    }

    public WeatherType currentWeather = WeatherType.ClearSkies;
    public WeatherType nextWeather;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the weather timer
        weatherTimer += Time.deltaTime;

        // Check if it's time to change the weather
        if (weatherTimer >= countDownTimer)
        {
            // Reset the timer
            weatherTimer = 0f;

            // Change the weather
            ChangeWeather();
        }

        // Smoothly transition between weather types
        if (currentWeather != nextWeather)
        {
            if (lerpTimer < lerpDuration)
            {
                lerpTimer += Time.deltaTime;
                float t = lerpTimer / lerpDuration;
                t = Mathf.Clamp01(t);
                t = Mathf.SmoothStep(0, 1, t);  // use SmoothStep to generate a smooth curve for t

                float cloudPower = Mathf.Lerp(GetCloudPower(currentWeather), GetCloudPower(nextWeather), t);
                float cloudAlpha = Mathf.Lerp(GetCloudAlpha(currentWeather), GetCloudAlpha(nextWeather), t);
                shader.SetFloat("_CloudPower", cloudPower);
                shader.SetFloat("_CloudAlpha", cloudAlpha);
            }
            else
            {
                currentWeather = nextWeather;
                lerpTimer = 0f;
            }
        }
    }
    

     void ChangeWeather()
    {
        // Generate a random number between 0 and 3
        int randomNumber = Random.Range(0, 4);

        // Set the current weather based on the random number
        switch (randomNumber)
        {
            case 0:
                nextWeather  = WeatherType.ClearSkies;
                break;

            case 1:
                nextWeather  = WeatherType.PartlyCloudy;
                break;

            case 2:
                nextWeather  = WeatherType.PartlyOvercast;
                break;

            case 3:
                nextWeather  = WeatherType.Overcast;
                break;
        }
    }
    float GetCloudPower(WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.ClearSkies:
                return 0f;
            case WeatherType.PartlyCloudy:
                return 1.5f;
            case WeatherType.PartlyOvercast:
                return 0.7f;
            case WeatherType.Overcast:
                return 0.5f;
            default:
                return 0f;
        }
    }

     float GetCloudAlpha(WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.ClearSkies:
                return 0f;
            case WeatherType.PartlyCloudy:
                return 2f;
            case WeatherType.PartlyOvercast:
                return 2.23f;
            case WeatherType.Overcast:
                return 5f;
            default:
                return 0f;
        }
    }
}
