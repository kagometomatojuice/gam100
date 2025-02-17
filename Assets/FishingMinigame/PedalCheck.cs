using System;
using UnityEngine;

public class PedalCheck : MonoBehaviour
{
    public FishingMinigame fmScript;
    public bool inMarker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space is pressed");
            if (inMarker)
            {
                fmScript.SuccessfulHit();
                //Debug.LogWarning("wow a hit!");
            }
            else
            {
                fmScript.MissedHit();
                //Debug.LogError("Wow you suck :(");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.tag + " + " + other.gameObject.name);
        if (other.gameObject.CompareTag("Marker"))
        {
            inMarker = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //Debug.LogWarning(other.gameObject.tag + " + " + other.gameObject.name);
        if (other.gameObject.CompareTag("Marker"))
        {
            inMarker = false;
        }
    }
}
