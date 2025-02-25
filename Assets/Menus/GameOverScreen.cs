using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public GameObject Background;
    public LevelManager lmScript;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        lmScript.ChangeSceneOnWin();
    }

    public void ExitButton()
    {
        lmScript.ChangeSceneOnLose();
    }
}
