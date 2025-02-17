using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject controls;  
    //public Dialogue dialogue;
    private bool isPaused = true;
    
    void Start()
    {
        controls.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            controls.SetActive(false);
            
            // if (dialogue != null)
            // {
            //     dialogue.gameObject.SetActive(true); 
            //     dialogue.StartDialogue(); 
            // }
            //
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
