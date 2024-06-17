using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mixer;
    private AudioSource bgmPlayer;
    public List<AudioClip> bmgTracks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        bgmPlayer = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("volumeMasterPref"))
            SetVolumeMaster(PlayerPrefs.GetFloat("volumeMasterPref"));
        if (PlayerPrefs.HasKey("volumeMusicPref"))
            SetVolumeMusic(PlayerPrefs.GetFloat("volumeMusicPref"));
        if (PlayerPrefs.HasKey("volumeSFXPref"))
            SetVolumeSFX(PlayerPrefs.GetFloat("volumeSFXPref"));
    }

    public void SetVolumeMaster(float volumeMaster)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Log10(volumeMaster) * 20);
    }

    public void SetVolumeMusic(float volumeMusic)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Log10(volumeMusic) * 20);
    }
    public void SetVolumeSFX(float volumeSFX)
    {
        mixer.SetFloat("VolumeSFX", Mathf.Log10(volumeSFX) * 20);
    }
    public void SaveVolumeSettings()
    {
        float volumeMaster;
        if (mixer.GetFloat("VolumeMaster", out volumeMaster))
        {
            PlayerPrefs.SetFloat("volumeMasterPref", Mathf.Pow(10, volumeMaster / 20));
        }

        float volumeMusic;
        if (mixer.GetFloat("VolumeMusic", out volumeMusic))
        {
            PlayerPrefs.SetFloat("volumeMusicPref", Mathf.Pow(10, volumeMusic / 20));
        }

        float volumeSFX;
        if (mixer.GetFloat("VolumeSFX", out volumeSFX))
        {
            PlayerPrefs.SetFloat("volumeSFXPref", Mathf.Pow(10, volumeSFX / 20));
        }

        PlayerPrefs.Save();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        if (sceneIndex < bmgTracks.Count && bmgTracks[sceneIndex] != null)
        {
            bgmPlayer.clip = bmgTracks[sceneIndex];
            bgmPlayer.Play();
        }
    }



}
