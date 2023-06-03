using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    private FirstPersonLook3 controlScript;
    [SerializeField] private TextMeshProUGUI sensText;
    [SerializeField] private Slider sensitivitySlider; // Reference to the slider
    private float turnSpeed;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        controlScript = player.GetComponent<FirstPersonLook3>();
        turnSpeed = 4.0f;
    }
    
    void Start()
    {
        // Set the initial value of the sensitivity slider
        sensitivitySlider.value = turnSpeed;

        // Add an event listener to the slider
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    void Update()
    {
        sensText.text = turnSpeed.ToString();
    }

    public void SetSensitivity(float newTurnSpeed)
    {
        turnSpeed = newTurnSpeed;
        sensText.text = turnSpeed.ToString();
        controlScript.turnSpeed = turnSpeed; // Update the turnSpeed field in the controlScript directly
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGraphicsLevel(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }
}