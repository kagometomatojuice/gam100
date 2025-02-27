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
    public SpriteRenderer backgroundImage;
    public Sprite bg0, bg25, bg50, bg75;
    
    public bool gameOverTriggered = false;
    
    void Start()
    {
        //gosScript.gameObject.SetActive(false);
        pollution = 50f;
        SetMaxPollution((int)maxPollution);
        UpdateBackground(); 
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
        UpdateBackground(); 
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
    private void UpdateBackground()
    {
        if (pollution <= 25)
        {
            backgroundImage.sprite = bg0;
        }
        else if (pollution > 25 && pollution <= 50)
        {
            backgroundImage.sprite = bg25;
        }
        else if (pollution > 50 && pollution <= 75)
        {
            backgroundImage.sprite = bg50;
        }
        else
        {
            backgroundImage.sprite = bg75;
        }
    }
}