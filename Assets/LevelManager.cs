using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string winSceneName;
    public string loseSceneName; 
    
    public void ChangeSceneOnWin()
    {
        SceneManager.LoadScene(winSceneName);
    }
    
    public void ChangeSceneOnLose()
    {
        SceneManager.LoadScene(loseSceneName);
    }
}
