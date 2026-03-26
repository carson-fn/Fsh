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

    private const float BaseGarbageRadius = 0.75f;
    private const float GarbageRadiusPerLevel = 0.25f;

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
        SaveProfile();
    }

    public void SaveProfile()
    {
        if (CurrentProfile == null) return;
        store.SaveProfile(CurrentProfile);
    }

    public int GetCoins() => CurrentProfile?.coins ?? 0;

    public float GetHookSpeed()
    {
        int level = CurrentProfile?.hookSpeedLevel ?? 0;
        return BaseHookSpeed + level * HookSpeedPerLevel;
    }

    public float GetMoveSpeed()
    {
        int level = CurrentProfile?.moveSpeedLevel ?? 0;
        return BaseMoveSpeed + level * MoveSpeedPerLevel;
    }

    public float GetGarbageBuddyHitboxRadius()
    {
        int level = CurrentProfile?.garbageBuddyHitboxLevel ?? 0;
        return BaseGarbageRadius + level * GarbageRadiusPerLevel;
    }

    public bool HasGarbageBuddyEquipped()
    {
        return CurrentProfile != null && CurrentProfile.equippedBuddy == "garbage";
    }

    public bool TrySpendCoins(int cost)
    {
        if (CurrentProfile == null) return false;
        if (CurrentProfile.coins < cost) return false;

        CurrentProfile.coins -= cost;
        SaveProfile();
        return true;
    }

    public void AddCoins(int amount)
    {
        if (CurrentProfile == null || amount <= 0) return;

        CurrentProfile.coins += amount;
        SaveProfile();
    }

    public void UpgradeHookSpeed()
    {
        if (CurrentProfile == null) return;
        CurrentProfile.hookSpeedLevel++;
        SaveProfile();
    }

    public void UpgradeMoveSpeed()
    {
        if (CurrentProfile == null) return;
        CurrentProfile.moveSpeedLevel++;
        SaveProfile();
    }

    public void UpgradeGarbageBuddyHitbox()
    {
        if (CurrentProfile == null) return;
        CurrentProfile.garbageBuddyHitboxLevel++;
        SaveProfile();
    }

    public void EquipBuddy(string buddyId)
    {
        if (CurrentProfile == null) return;
        CurrentProfile.equippedBuddy = buddyId;
        SaveProfile();
    }
}