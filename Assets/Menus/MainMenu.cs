using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        int screenW = 1920;
        int screenH = 1080;
        bool isFullScreen = true;
        
        Screen.SetResolution(screenW, screenH, isFullScreen);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

 
}
