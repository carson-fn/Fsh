using UnityEngine;

public class LocalPlayerProfileStore : IPlayerProfileStore
{
    private string GetProfileKey(string userId)
    {
        return $"player_profile_{userId}";
    }

    public PlayerProfileData LoadProfile(string userId)
    {
        string key = GetProfileKey(userId);

        if (!PlayerPrefs.HasKey(key))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(key);

        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        return JsonUtility.FromJson<PlayerProfileData>(json);
    }

    public void SaveProfile(PlayerProfileData profile)
    {
        if (profile == null || string.IsNullOrWhiteSpace(profile.userId))
        {
            Debug.LogError("Tried to save an invalid profile.");
            return;
        }

        string key = GetProfileKey(profile.userId);
        string json = JsonUtility.ToJson(profile);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
}