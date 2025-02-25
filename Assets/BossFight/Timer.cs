using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public float startingTime = 60f;
    [SerializeField] Text countdownText;
    public LevelManager lmScript;
    void Start()
    {
        currentTime = startingTime;
        //countdownText.gameObject.SetActive(false);
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("00");
            
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            lmScript.ChangeSceneOnLose();
        }
    }
    
    public void StartTimer() 
    {
        countdownText.gameObject.SetActive(true);
    }
}