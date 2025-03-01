using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    //public GameController gcScript;
    public GameObject progressBase;
    public GameObject markerPivot;
    public GameObject pedalPivot;

    public float progressMax = 10f;
    public float progress;
    public float pedalStrength = 1.0f;
    public float pedalSpeed = 5f;
    public bool pedalACW = true;
    public float progressDrain = 0.4f;
    public float missedStrength = 1.0f;
    public float markerSize = 3f; // any size >= 3 is very easy
    public float humanPollutionDecrease = 20f;

    public bool minigameActive = false;
    public HookMove hmScript;
    //public PointHUD pointHUD;
    public Player playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (minigameActive)
        {
            progress -= Time.deltaTime * progressDrain;
            
            if (pedalACW)
            {
                pedalPivot.transform.localEulerAngles = new Vector3(0, 0, pedalPivot.transform.localEulerAngles.z + (pedalSpeed * Time.deltaTime));
            }
            else
            {
                pedalPivot.transform.localEulerAngles = new Vector3(0, 0, pedalPivot.transform.localEulerAngles.z - (pedalSpeed * Time.deltaTime));
            }
            
            UIProgressBar();

            if (progress <= 0f)
            {
                MinigameDone(false);
            }
            else if (progress >= progressMax)
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
        progress = progressMax * 0.5f;
    }

    public void SuccessfulHit()
    {
        pedalACW = !pedalACW;
        markerPivot.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        progress += pedalStrength;
    }
    public void MissedHit()
    {
        progress -= missedStrength;
    }

    void MinigameDone(bool successState)
    {
        minigameActive = false;
        if (hmScript != null)
        {
            hmScript.OnMinigameComplete(successState);
        }

        if (successState)
        {
            //pointHUD.AddPoint();
            playerScript.pollution -= humanPollutionDecrease;
            playerScript.pollution = Mathf.Max(playerScript.pollution, 0);
        }

        gameObject.SetActive(false);
    }

    void UIProgressBar()
    {
        Vector3 pls = progressBase.transform.localScale;
        progressBase.transform.localScale = new Vector3(pls.x, progress / progressMax, pls.z);
    }
    
    
}
