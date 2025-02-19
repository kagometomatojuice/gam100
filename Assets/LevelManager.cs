using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string winSceneName;
    public string loseSceneName;

    public Animator transition;
    
    public void ChangeSceneOnWin()
    {
        StartCoroutine(LoadLevel(winSceneName));
    }
    
    public void ChangeSceneOnLose()
    {
        StartCoroutine(LoadLevel(loseSceneName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }
}
