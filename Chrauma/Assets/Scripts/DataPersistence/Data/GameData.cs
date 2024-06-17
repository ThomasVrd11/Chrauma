using UnityEngine;

[System.Serializable]
public class GameData
{
    /* Player stats*/
    public int health;
    public int entropy;

    /* Current scene index */
    public int scene;

    /* Player position in game world */
    public Vector3 playerPos;

    /* unlockable items if needed */
    public bool unlockable1;
    public bool unlockable2;
    public bool unlockable3;

    public GameData()
    {
        /* Constructor to initialize default game data*/
        this.health = 100;
        this.entropy = 100;
        this.scene = 1;
        this.playerPos = new Vector3(0, 0, 0);
        this.unlockable1 = false;
        this.unlockable2 = false;
        this.unlockable3 = false;
    }
}
