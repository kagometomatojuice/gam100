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
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
