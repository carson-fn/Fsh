using System;
using UnityEditor.Rendering;

[Serializable]
public class PlayerProfileData
{
    public string userId;
    public int coins;

    public string equippedBuddy = "none";

    public int hookSpeedLevel;
    public int moveSpeedLevel;
    public int highestLevelReached;
}