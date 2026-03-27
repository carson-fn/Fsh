using UnityEngine;

public class LocalPlayerProfileStore : IPlayerProfileStore
{
    private const string Prefix = "player_profile_";

    public PlayerProfileData LoadProfile(string userId)
    {
        string key = Prefix + userId;

        if (!PlayerPrefs.HasKey(key))
        {
            return new PlayerProfileData
            {
                userId = userId,
                coins = 0,
                equippedBuddy = "none",
                hookSpeedLevel = 0,
                moveSpeedLevel = 0,
            };
        }

        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<PlayerProfileData>(json);
    }

    public void SaveProfile(PlayerProfileData profile)
    {
        if (profile == null || string.IsNullOrWhiteSpace(profile.userId))
            return;

        string key = Prefix + profile.userId;
        string json = JsonUtility.ToJson(profile);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
}