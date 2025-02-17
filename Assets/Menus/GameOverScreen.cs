using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public GameObject Background;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
