using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    /* Singleton reference */
    public static AudioManager instance;
    /* Ref to AudioMixer */
    public AudioMixer mixer;
    /* List of background music track */
    public List<AudioClip> bmgTracks;
    /* Audio source for background music */
    private AudioSource bgmPlayer;

    private void Awake()
    {
        /*
            Singleton pattern
            Get AudioSource component for background music
        */
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
        /* Load saved volume settings if they exist */
        if (PlayerPrefs.HasKey("volumeMasterPref"))
            SetVolumeMaster(PlayerPrefs.GetFloat("volumeMasterPref"));
        if (PlayerPrefs.HasKey("volumeMusicPref"))
            SetVolumeMusic(PlayerPrefs.GetFloat("volumeMusicPref"));
        if (PlayerPrefs.HasKey("volumeSFXPref"))
            SetVolumeSFX(PlayerPrefs.GetFloat("volumeSFXPref"));
    }

    public void SetVolumeMaster(float volumeMaster)
    {
        /*
        set the master volume
            volumeMaster: volume level for the master volume
        */
        mixer.SetFloat("VolumeMaster", Mathf.Log10(volumeMaster) * 20);
    }

    public void SetVolumeMusic(float volumeMusic)
    {
        /*
        set the music volume
            volumeMusic: volume level for the music volume
        */
        mixer.SetFloat("VolumeMusic", Mathf.Log10(volumeMusic) * 20);
    }
    public void SetVolumeSFX(float volumeSFX)
    {
        /*
        set the sound effects volume
            volumeSFX: volume level for the sound effects volume
        */
        mixer.SetFloat("VolumeSFX", Mathf.Log10(volumeSFX) * 20);
    }
    public void SaveVolumeSettings()
    {
        /*
        Save the current volume settings to Playerprefs
            Mixer to PlayerPrefs:
            VolumeMaster to volumeMasterPref
            VolumeMusic to volumeMusicPref
            VolumeSFX to volumeSFXrPref
        */
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
        /* Subscribe to the sceneLoaded event */
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        /* Unsubscribe to the sceneLoaded event */
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*
        Called when a new scene is loaded.
        Plays the corresponding backgroud music track for the scene according to its index
        scene: the scene that was loaded
        mode: the mode in wich the scene was loaded 
        */
        int sceneIndex = scene.buildIndex;

        if (sceneIndex < bmgTracks.Count && bmgTracks[sceneIndex] != null)
        {
            bgmPlayer.clip = bmgTracks[sceneIndex];
            bgmPlayer.Play();
        }
    }

}
