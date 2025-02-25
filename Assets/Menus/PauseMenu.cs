using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private GameObject bossHealthBar;

    [Obsolete("Obsolete")]
    private void Start()
    {
        if (bossHealthBar == null)
        {
            BossHealthBar bhb = FindObjectOfType<BossHealthBar>();
            if (bhb != null)
            {
                bossHealthBar = bhb.gameObject;
            }
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false); 
        }
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }
} 
