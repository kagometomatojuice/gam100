using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossMinigame : MonoBehaviour
{
    //public GameController gcScript;
    public GameObject markerPivot;
    public GameObject pedalPivot;
    
    public float pedalSpeed = 400f;
    public bool pedalACW = true;
    public float markerSize = 3f; // any size >= 3 is very easy

    public bool minigameActive = false;
    public BossHookMove hmScript;
    public BossHealthBar bhScript;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (minigameActive)
        {
            if (pedalACW)
            {
                pedalPivot.transform.localEulerAngles = new Vector3(0, 0, pedalPivot.transform.localEulerAngles.z + (pedalSpeed * Time.deltaTime));
            }
            else
            {
                pedalPivot.transform.localEulerAngles = new Vector3(0, 0, pedalPivot.transform.localEulerAngles.z - (pedalSpeed * Time.deltaTime));
            }
            
            if (bhScript && bhScript.healthMax <= 0f)
            {
                MinigameDone(true); 
            }
        }
    }

    public void RestartMinigame()
    {
        if (minigameActive == true)
        {
            return;
        }
        gameObject.SetActive(true);
        minigameActive = true; 
    }

    public void SuccessfulHit()
    {
        pedalACW = !pedalACW;
        markerPivot.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        
        if (bhScript)
        {
            bhScript.takeDamage(10); // reduce boss health per hit
        }
    }
    public void MissedHit()
    {
        if (bhScript)
        {
            bhScript.heal(5); // increase boss health on miss
        }
    }

    void MinigameDone(bool successState)
    {
        minigameActive = false;
        if (hmScript)
        {
            hmScript.OnMinigameComplete(successState);
        }

        if (successState)
        {
            //next level cutscene
            //Debug.Log("Boss defeated!");
        }
        else
        {
            // defeat cutscene
            //Debug.Log("You lost!");
        }

        Destroy(gameObject);
    }
}
