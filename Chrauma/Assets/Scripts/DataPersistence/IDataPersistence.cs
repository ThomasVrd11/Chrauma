/*
Interface for data persistence, defining methods for loading and saving game data.
Implemented for any class that needs to use or store persistent game data
*/
public interface IDataPersistence
{
    /*Load the game data into the class*/
    void LoadData(GameData data);
    /*Save the game data from the class*/
    void SaveData(GameData data);
}
