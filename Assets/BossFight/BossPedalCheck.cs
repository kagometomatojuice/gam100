using System;
using UnityEngine;

public class BossPedalCheck : MonoBehaviour
{
    public BossMinigame fmScript;
    public bool inMarker;
    
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip missSFX;
    private AudioSource source;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
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
                if (hitSFX)
                {
                    source.PlayOneShot(hitSFX);
                }
            }
            else
            {
                fmScript.MissedHit();
                //Debug.LogError("Wow you suck :(");
                if (missSFX)
                {
                    source.PlayOneShot(missSFX);
                }
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
       // Debug.LogWarning(other.gameObject.tag + " + " + other.gameObject.name);
        if (other.gameObject.CompareTag("Marker"))
        {
            inMarker = false;
        }
    }
}