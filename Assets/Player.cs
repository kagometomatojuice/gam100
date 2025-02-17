using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image pollutionBar;
    public Gradient gradient; 
    public float pollution, maxPollution;
    public float pollutionIncrease = 1f;
    // public GameOverScreen gosScript;
    // public GameOverGraphic gogScript;
    public LevelManager lmScript;
    
    public bool gameOverTriggered = false;
    
    void Start()
    {
        //gosScript.gameObject.SetActive(false);
        pollution = 50f;
        SetMaxPollution((int)maxPollution);
    }
    
    void Update()
    {
        if (gameOverTriggered)
        {
            return;
        }
        
        pollution += pollutionIncrease * Time.deltaTime;

        if (pollution > maxPollution)
        {
            pollution = maxPollution;
            TriggerGameOver();
        }
        
        SetPollution((int)pollution);
    }
    
    public void SetMaxPollution(int pollution)
    {
        pollutionBar.fillAmount = 1f;
        UpdateFillColor();
    }
    
    public void SetPollution(int pollution)
    {
        pollutionBar.fillAmount = (float)pollution / maxPollution;
        UpdateFillColor();
    }
    
    private void UpdateFillColor()
    {
        pollutionBar.color = gradient.Evaluate(pollutionBar.fillAmount);
    }
    
    private void TriggerGameOver()
    {
        if (gameOverTriggered)
        {
            return;
        }
        
        gameOverTriggered = true;
        lmScript.ChangeSceneOnLose();
    }
}