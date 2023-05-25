using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void OpenSettings()
    {
        // TODO: Implement settings menu
        Debug.Log("Opening settings menu...");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}