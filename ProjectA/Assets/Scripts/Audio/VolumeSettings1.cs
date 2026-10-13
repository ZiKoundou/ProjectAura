using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings1 : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("musicVolume")){
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

    }

    public void SetMusicVolume()
    {
        if (musicSlider != null)
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
        }

    }

    public void SetSFXVolume()
    {
        if (SFXSlider != null)
        {
            float volume = SFXSlider.value;
            myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    public void LoadVolume()
    {
        if (musicSlider == null) return;
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        if (SFXSlider == null) return;
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}
