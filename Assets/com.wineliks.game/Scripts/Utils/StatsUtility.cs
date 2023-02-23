using UnityEngine;

public static class StatsUtility
{
    public static int Level
    {
        get => PlayerPrefs.GetInt("level", 1);
        set => PlayerPrefs.SetInt("level", value);
    }
}
