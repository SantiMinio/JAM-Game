using System;

[Serializable]
public class SaveData
{
    public bool[] levelsClear = new bool[15];
    public int[] starsPerLevel = new int[15];
    public bool creditsPass = false;
}
