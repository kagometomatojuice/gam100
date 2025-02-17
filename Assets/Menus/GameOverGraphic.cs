using System.Collections;
using UnityEngine;

public class GameOverGraphic : MonoBehaviour
{
    public GameOverScreen gosScript;
    //public bool isGraphicActive = false;

    // private void Awake()
    // {
    //     int screenW = 1920;
    //     int screenH = 1080;
    //     bool isFullScreen = false;
    //     
    //     Screen.SetResolution(screenW, screenH, isFullScreen);
    // }

    void Start()
    {
        gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransitionToGameOverScreen();
        }
    }
    
    // public void ShowGraphic()
    // {
    //     gameObject.SetActive(true); 
    //     isGraphicActive = true;
    // }
    
    private void TransitionToGameOverScreen()
    {
        //isGraphicActive = false;
        gameObject.SetActive(false); 
        gosScript.gameObject.SetActive(true); 
    }
}
