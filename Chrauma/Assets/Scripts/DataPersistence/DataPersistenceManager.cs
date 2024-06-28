using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File storage config")]
    /* Name of the file where game data is stored*/
    [SerializeField] private string fileName;
    /* Object to hold the game data */
    private GameData gameData;
    /* List of object that use the IDataPersistence interface */
    private List<IDataPersistence> dataPersistencesObjects;
    /* Handler for file operations */
    private FileDataHandler dataHandler;
    /* Singleton instance */
    public static DataPersistenceManager instance { get; private set; }
    /* Flag to check if the game has loaded something */
    public bool isLoading = false;
    /* Position of the player that was saved */
    public Vector3 loadedPlayerPos;


    private void Awake()
    {
        /* Singleton patter */
        if (instance != null)
        {
            Debug.LogError("More than one Data Persistence Manager in the scene");
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        /*
        initialize the FileDataHandler with the specified file name
        Find all objects that implement the IDataPersistence interface
        */
        this.dataHandler = new FileDataHandler(Application.dataPath, fileName);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
		Debug.Log(dataPersistencesObjects);

    }
    public void NewGame()
    {
        /* initialize a new GameData object when starting a new game*/
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        /*
        Load the game data from the file and updates all data persistence objects
        Retrieve the saved player position and change scene to the saved scene ID
        */
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            return;
        }
        isLoading = true;
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        loadedPlayerPos = gameData.playerPos;
        GameManager.instance.SwitchScene(gameData.scene);
    }

    public void SaveGame()
    {
        /*
        Retrieve the data from all data persistence object, and scene id
        Save the current game data to the file
        */
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        gameData.scene = SceneManager.GetActiveScene().buildIndex;
        dataHandler.Save(gameData);
    }
    public bool CheckIfSave()
    {
        /*
        Checks if there is a saved game data file
        return true if save exist,otherwise false
        */
        this.gameData = dataHandler.Load();
        if (this.gameData != null)
        {
            return true;
        }
        else return false;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        /*
        Finds all objects in the scene that implement the IDataPersistence interface
        return list of IDataPersistence obects
        */
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnEnable()
    {
        /* Subscribe to the sceneLoaded event*/
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        /* Unsubscribe to the sceneLoaded event*/
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*
        Called when a new scene is loaded
        Refresh the list of data persistence objects
        scene: the scene that was loaded
        mode: the mode in which the scene was loaded
        */
        RetrieveDataPersistencesObjects();
		//Debug.Log(dataPersistencesObjects);
    }

    public void DebugList()
    {
        /*
        Debugging method
        list all data persistence object in console
        */
        Debug.Log("Debuging");
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            Debug.Log("obj:");
            Debug.Log(dataPersistenceObj.ToString());
        }
    }
	public void RetrieveDataPersistencesObjects()
	{
		this.dataPersistencesObjects = FindAllDataPersistenceObjects();
	}
}
