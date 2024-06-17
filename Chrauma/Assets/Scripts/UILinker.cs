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
}
