using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSliders : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("volumeMasterPref"))
        {
            float savedMasterVolume = PlayerPrefs.GetFloat("volumeMasterPref");
            masterSlider.value = savedMasterVolume;
        }
        if (PlayerPrefs.HasKey("volumeMusicPref"))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat("volumeMusicPref");
            musicSlider.value = savedMusicVolume;
        }
        if (PlayerPrefs.HasKey("volumeSFXPref"))
        {
            float savedSFXVolume = PlayerPrefs.GetFloat("volumeSFXPref");
            sfxSlider.value = savedSFXVolume;
        }
    }

}
