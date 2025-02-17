using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PointHUD : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Text pointText;
    [SerializeField] LevelManager lmScript;
    
    private int currentPoints = 0;
    private int maxPoints = 5;

    private void Awake()
    {
        UpdateHUD();
    }
    
    public void AddPoint()
    {
        if (currentPoints < maxPoints)
        {
            currentPoints++;
            UpdateHUD();
            
            if (currentPoints >= maxPoints)
            {
                lmScript.ChangeSceneOnWin();
            }
        }
    }

    private void UpdateHUD()
    {
        pointText.text = $"{currentPoints}/{maxPoints}";
    }
}
