using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    /* Directory path where the data file is stored*/
    private string dataDirPath = "";
    /* Name of the data file */
    private string dataFileName = "";


    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        /*
        Constructor to initialize the FileDataHandler with specified directory path and file name
        dataDirPath: directory path where the file will be stored
        dataFileName: name of data file
        */
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        /*
        Load data from a JSON file
        Use Path.Combine to account for different OS having different path separator
        Load the serialized data from the file
        Deserialize from JSON back into C# object
        return: the loaded GameData object or null if fail
        */
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to load data from file:" + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        /*
        Save data to a JSON file
        Use Path.Combine to account for different OS having different path separator
        Create the directory the file will be written to if doesn't already exist
        Serialize the C# game data object into JSON
        write the serialized data to the file
        data: GameData object to be saved
        */
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occured when trying to save data to file:" + fullPath + "\n" + ex);
        }
    }
}
