using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public static PlayerProfileManager Instance { get; private set; }

    public PlayerProfileData CurrentProfile { get; private set; }

    private IPlayerProfileStore store;

    private const float BaseHookSpeed = 4.5f;
    private const float HookSpeedPerLevel = 0.5f;

    private const float BaseMoveSpeed = 3f;
    private const float MoveSpeedPerLevel = 0.4f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        store = new LocalPlayerProfileStore();
    }

    public void InitializeForUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            Debug.LogError("InitializeForUser called with empty userId.");
            return;
        }

        CurrentProfile = store.LoadProfile(userId);

        if (CurrentProfile == null)
        {
            CurrentProfile = CreateDefaultProfile(userId);
        }

        if (string.IsNullOrWhiteSpace(CurrentProfile.userId))
        {
            CurrentProfile.userId = userId;
        }

        if (string.IsNullOrWhiteSpace(CurrentProfile.equippedBuddy))
        {
            CurrentProfile.equippedBuddy = "none";
        }

        SaveProfile();
    }

    private PlayerProfileData CreateDefaultProfile(string userId)
    {
        return new PlayerProfileData
        {
            userId = userId,
            coins = 0,
            equippedBuddy = "none",
            hookSpeedLevel = 0,
            moveSpeedLevel = 0,
            highestLevelReached = 0
        };
    }

    public void SaveProfile()
    {
        if (CurrentProfile == null)
        {
            Debug.LogWarning("SaveProfile called with no current profile.");
            return;
        }

        store.SaveProfile(CurrentProfile);
    }

    public int GetCoins()
    {
        return CurrentProfile?.coins ?? 0;
    }

    public float GetHookSpeed()
    {
        int level = CurrentProfile?.hookSpeedLevel ?? 0;
        return BaseHookSpeed + (level * HookSpeedPerLevel);
    }

    public float GetMoveSpeed()
    {
        int level = CurrentProfile?.moveSpeedLevel ?? 0;
        return BaseMoveSpeed + (level * MoveSpeedPerLevel);
    }

    public string GetEquippedBuddy()
    {
        return CurrentProfile?.equippedBuddy ?? "none";
    }

    public bool HasBuddyEquipped(string buddyId)
    {
        return CurrentProfile != null && CurrentProfile.equippedBuddy == buddyId;
    }

    public bool TrySpendCoins(int cost)
    {
        if (CurrentProfile == null)
        {
            return false;
        }

        if (cost < 0)
        {
            Debug.LogWarning("TrySpendCoins called with negative cost.");
            return false;
        }

        if (CurrentProfile.coins < cost)
        {
            return false;
        }

        CurrentProfile.coins -= cost;
        SaveProfile();
        return true;
    }

    public void AddCoins(int amount)
    {
        if (CurrentProfile == null || amount <= 0)
        {
            return;
        }

        CurrentProfile.coins += amount;
        SaveProfile();
    }

    public void UpgradeHookSpeed()
    {
        if (CurrentProfile == null)
        {
            return;
        }

        CurrentProfile.hookSpeedLevel++;
        SaveProfile();
    }

    public void UpgradeMoveSpeed()
    {
        if (CurrentProfile == null)
        {
            return;
        }

        CurrentProfile.moveSpeedLevel++;
        SaveProfile();
    }

    public bool IsBuddyUnlocked(string buddyId)
    {
        if (CurrentProfile == null)
        {
            return false;
        }

        switch (buddyId)
        {
            case "none":
                return true;
            case "liam":
                return CurrentProfile.highestLevelReached >= 3;
            case "harold":
                return CurrentProfile.highestLevelReached >= 6;
            case "carson":
                return CurrentProfile.highestLevelReached >= 9;
            case "garbage":
                return CurrentProfile.highestLevelReached >= 12;
            default:
                return false;
        }
    }

    public int GetBuddyUnlockLevel(string buddyId)
    {
        switch (buddyId)
        {
            case "liam":
                return 3;
            case "harold":
                return 6;
            case "carson":
                return 9;
            case "garbage":
                return 12;
            default:
                return 0;
        }
    }

    public void EquipBuddy(string buddyId)
    {
        if (CurrentProfile == null)
        {
            return;
        }

        if (!IsBuddyUnlocked(buddyId))
        {
            Debug.LogWarning($"Tried to equip locked or unknown buddy: {buddyId}");
            return;
        }

        CurrentProfile.equippedBuddy = buddyId;
        SaveProfile();
    }

    public void UpdateHighestLevelReached(int level)
    {
        if (CurrentProfile == null)
        {
            return;
        }

        if (level > CurrentProfile.highestLevelReached)
        {
            CurrentProfile.highestLevelReached = level;
            SaveProfile();
        }
    }
}