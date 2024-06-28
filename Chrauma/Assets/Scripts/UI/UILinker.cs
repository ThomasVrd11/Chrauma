using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILinker : MonoBehaviour
{
    public void Load()
    {
        DataPersistenceManager.instance.LoadGame();
    }
    public void Save()
    {
        DataPersistenceManager.instance.SaveGame();
    }
    public void Exit()
    {
        GameManager.instance.ExitGame();
    }
    public void Restart()
    {
        PlayerStats.instance.HideDeath();
        GameManager.instance.RestartGame();
    }

    public void SetVolMaster(float volume)
    {
        AudioManager.instance.SetVolumeMaster(volume);
    }
    public void SetVolMusic(float volume)
    {
        AudioManager.instance.SetVolumeMusic(volume);
    }
    public void SetVolSFX(float volume)
    {
        AudioManager.instance.SetVolumeSFX(volume);
    }

    public void SaveVolSettings()
    {
        AudioManager.instance.SaveVolumeSettings();
    }
}
