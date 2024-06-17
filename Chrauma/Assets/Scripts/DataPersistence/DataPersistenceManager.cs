using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File storage config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance {get; private set;}
    public bool isLoading = false;
    public Vector3 loadedPlayerPos;

    private void Awake() {
        if(instance != null)
        {
            Debug.LogError("More than one Data Persistence Manager in the scene");
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Start() {
        this.dataHandler = new FileDataHandler(Application.dataPath,fileName);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            return;
        }
        isLoading = true;
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        loadedPlayerPos = gameData.playerPos;
        GameManager.instance.SwitchScene(gameData.scene);
    }
    public void SaveGame()
    {
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        gameData.scene =  SceneManager.GetActiveScene().buildIndex;
        dataHandler.Save(gameData);
    }
    public bool CheckIfSave()
    {
        this.gameData = dataHandler.Load();
        if(this.gameData != null)
        {
            return true;
        }
        else return false;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
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
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
    }

    public void DebugList()
    {
        Debug.Log("Debuging");
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            Debug.Log("obj:");
            Debug.Log(dataPersistenceObj.ToString());
        }
    }
}
