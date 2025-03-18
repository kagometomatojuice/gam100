using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    public AudioClip background;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }
    
    // private void Awake()
    // {
    //     int screenW = 1920;
    //     int screenH = 1080;
    //     bool isFullScreen = false;
    //     
    //     Screen.SetResolution(screenW, screenH, isFullScreen);
    // }
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
