using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelManager lmScript;
    private void Awake()
    {
        int screenW = 1920;
        int screenH = 1080;
        bool isFullScreen = true;
        
        Screen.SetResolution(screenW, screenH, isFullScreen);
    }
    public void PlayGame()
    {
        lmScript.ChangeSceneOnWin();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

 
}
