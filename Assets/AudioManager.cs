using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    public AudioClip background;
    
    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        UpdateVolumes();
        
        if (background != null)
        {
            musicSource.clip = background;
            musicSource.Play();
        }
    }

    public void UpdateVolumes()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicDB = PlayerPrefs.GetFloat("musicVolume");
            audioMixer.SetFloat("musicVolume", musicDB);
            musicSource.volume = DBToLinear(musicDB);
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            float sfxDB = PlayerPrefs.GetFloat("sfxVolume");
            audioMixer.SetFloat("sfxVolume", sfxDB);
            sfxSource.volume = DBToLinear(sfxDB);
        }
    }

    private float DBToLinear(float dB)
    {
        return Mathf.Pow(10f, dB / 20f);
    }
}