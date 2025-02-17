using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossDialogue : MonoBehaviour
{
    public Text textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public event Action OnDialogueComplete;
    public Timer timerScript;
    public BossHealthBar hbScript;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartBossDialogue()
    {
        gameObject.SetActive(true);
        textComponent.text = string.Empty;
        index = 0;
        Time.timeScale = 0f; 
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            OnDialogueComplete?.Invoke();
            Time.timeScale = 1f;
            if (timerScript != null)
            {
                timerScript.StartTimer(); 
            }
            
            if (hbScript != null)
            {
                hbScript.ShowBar(); 
            }
        }
    }
}