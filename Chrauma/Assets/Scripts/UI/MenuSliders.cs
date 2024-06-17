using UnityEngine;
using UnityEngine.UI;

public class MenuSliders : MonoBehaviour
{
    /* Reference to UI sliders for volume control*/
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    void Start()
    {
        /* Load saved volume in player preference and set the corresponding slider value if it exist */
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
