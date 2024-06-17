using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int health;
    public int entropy;
    public int scene;
    public Vector3 playerPos;

    //unlockable stuff if needed
    public bool unlockable1;
    public bool unlockable2;
    public bool unlockable3;

    public GameData()
    {
        this.health = 100;
        this.entropy = 100;
        this.scene = 1;
        this.playerPos = new Vector3(0,0,0);
        this.unlockable1 = false;
        this.unlockable2 = false;
        this.unlockable3 = false;
    }
}
