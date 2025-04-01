using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
        
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSfxVolume();
        }
        else
        {
            SetSfxVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        UpdateAllAudioManagers();
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        UpdateAllAudioManagers();
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    private void LoadSfxVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSfxVolume();
    }

    private void UpdateAllAudioManagers()
    {
        AudioManager[] audioManagers = FindObjectsByType<AudioManager>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);
        
        foreach (var am in audioManagers)
        {
            am.UpdateVolumes();
        }
    }
}