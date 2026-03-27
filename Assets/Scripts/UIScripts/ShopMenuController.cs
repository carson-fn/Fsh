using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    [Header("Root")]
    [SerializeField] private GameObject shopRoot;

    [Header("Main Text")]
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text hookText;
    [SerializeField] private TMP_Text moveText;
    [SerializeField] private TMP_Text equippedBuddyText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text progressText;

    [Header("Buddy Status Texts")]
    [SerializeField] private TMP_Text frogStatusText;
    [SerializeField] private TMP_Text crabStatusText;
    [SerializeField] private TMP_Text penguinStatusText;

    [Header("Buttons")]
    [SerializeField] private Button buyHookButton;
    [SerializeField] private Button buyMoveButton;
    [SerializeField] private Button equipFrogButton;
    [SerializeField] private Button equipCrabButton;
    [SerializeField] private Button equipPenguinButton;
    [SerializeField] private Button unequipButton;

    private PlayerProfileData Profile => PlayerProfileManager.Instance != null
        ? PlayerProfileManager.Instance.CurrentProfile
        : null;

    private void Start()
    {
        if (shopRoot == null)
        {
            shopRoot = gameObject;
        }

        RefreshUI();
    }

    public void OpenShop()
    {
        if (shopRoot != null)
        {
            shopRoot.SetActive(true);
        }

        RefreshUI();
    }

    public void CloseShop()
    {
        if (shopRoot != null)
        {
            shopRoot.SetActive(false);
        }
    }

    public void RefreshUI()
    {
        Debug.Log("RefreshUI running");
        if (PlayerProfileManager.Instance == null || Profile == null)
        {
            SetTextSafe(coinsText, "Coins: -");
            SetTextSafe(hookText, "Hook Speed Lv. -");
            SetTextSafe(moveText, "Move Speed Lv. -");
            SetTextSafe(equippedBuddyText, "Equipped Buddy: none");
            SetTextSafe(progressText, "Highest Level Reached: -");
            SetTextSafe(feedbackText, "No profile loaded.");
            SetTextSafe(frogStatusText, "Locked");
            SetTextSafe(crabStatusText, "Locked");
            SetTextSafe(penguinStatusText, "Locked");
            SetButtonsInteractable(false);
            return;
        }

        int hookCost = GetHookUpgradeCost();
        int moveCost = GetMoveUpgradeCost();

        SetTextSafe(coinsText, $"Coins: {Profile.coins}");
        SetTextSafe(hookText, $"Hook Speed Lv. {Profile.hookSpeedLevel} (Cost: {hookCost})");
        SetTextSafe(moveText, $"Move Speed Lv. {Profile.moveSpeedLevel} (Cost: {moveCost})");
        SetTextSafe(equippedBuddyText, $"Equipped Buddy: {FormatBuddyName(Profile.equippedBuddy)}");
        SetTextSafe(progressText, $"Highest Level Reached: {Profile.highestLevelReached}");

        UpdateBuddyStatus("frog", frogStatusText);
        UpdateBuddyStatus("crab", crabStatusText);
        UpdateBuddyStatus("penguin", penguinStatusText);

        if (buyHookButton != null)
        {
            buyHookButton.interactable = Profile.coins >= hookCost;
        }

        if (buyMoveButton != null)
        {
            buyMoveButton.interactable = Profile.coins >= moveCost;
        }

        if (equipFrogButton != null)
        {
            equipFrogButton.interactable = PlayerProfileManager.Instance.IsBuddyUnlocked("frog");
        }

        if (equipCrabButton != null)
        {
            equipCrabButton.interactable = PlayerProfileManager.Instance.IsBuddyUnlocked("crab");
        }

        if (equipPenguinButton != null)
        {
            equipPenguinButton.interactable = PlayerProfileManager.Instance.IsBuddyUnlocked("penguin");
        }

        if (unequipButton != null)
        {
            unequipButton.interactable = true;
        }

        if (feedbackText != null && string.IsNullOrWhiteSpace(feedbackText.text))
        {
            feedbackText.text = "Welcome to the shop.";
        }
    }

    public void BuyHookSpeed()
    {
        if (Profile == null) return;

        int cost = GetHookUpgradeCost();

        if (!PlayerProfileManager.Instance.TrySpendCoins(cost))
        {
            SetTextSafe(feedbackText, $"Not enough coins. Need {cost}.");
            return;
        }

        PlayerProfileManager.Instance.UpgradeHookSpeed();
        SetTextSafe(feedbackText, $"Bought Hook Speed upgrade for {cost} coins.");
        RefreshUI();
    }

    public void BuyMoveSpeed()
    {
        if (Profile == null) return;

        int cost = GetMoveUpgradeCost();

        if (!PlayerProfileManager.Instance.TrySpendCoins(cost))
        {
            SetTextSafe(feedbackText, $"Not enough coins. Need {cost}.");
            return;
        }

        PlayerProfileManager.Instance.UpgradeMoveSpeed();
        SetTextSafe(feedbackText, $"Bought Move Speed upgrade for {cost} coins.");
        RefreshUI();
    }

    public void EquipFrogBuddy()
    {
        EquipBuddy("frog");
    }

    public void EquipCrabBuddy()
    {
        EquipBuddy("crab");
    }

    public void EquipPenguinBuddy()
    {
        EquipBuddy("penguin");
    }

    public void UnequipBuddy()
    {
        if (PlayerProfileManager.Instance == null) return;

        PlayerProfileManager.Instance.EquipBuddy("none");
        SetTextSafe(feedbackText, "Buddy unequipped.");
        RefreshUI();
    }

    private void EquipBuddy(string buddyId)
    {
        if (PlayerProfileManager.Instance == null) return;

        if (!PlayerProfileManager.Instance.IsBuddyUnlocked(buddyId))
        {
            int unlockLevel = PlayerProfileManager.Instance.GetBuddyUnlockLevel(buddyId);
            SetTextSafe(feedbackText, $"{FormatBuddyName(buddyId)} unlocks at level {unlockLevel}.");
            return;
        }

        PlayerProfileManager.Instance.EquipBuddy(buddyId);
        SetTextSafe(feedbackText, $"{FormatBuddyName(buddyId)} equipped.");
        RefreshUI();
    }

    private void UpdateBuddyStatus(string buddyId, TMP_Text statusText)
    {
        if (statusText == null || PlayerProfileManager.Instance == null || Profile == null)
        {
            return;
        }

        if (!PlayerProfileManager.Instance.IsBuddyUnlocked(buddyId))
        {
            int unlockLevel = PlayerProfileManager.Instance.GetBuddyUnlockLevel(buddyId);
            statusText.text = $"Locked - Level {unlockLevel}";
            return;
        }

        if (PlayerProfileManager.Instance.HasBuddyEquipped(buddyId))
        {
            statusText.text = "Equipped";
            return;
        }

        statusText.text = "Unlocked";
    }

    private int GetHookUpgradeCost()
    {
        if (Profile == null) return 10;
        return 10 + (Profile.hookSpeedLevel * 5);
    }

    private int GetMoveUpgradeCost()
    {
        if (Profile == null) return 10;
        return 10 + (Profile.moveSpeedLevel * 5);
    }

    private void SetButtonsInteractable(bool value)
    {
        if (buyHookButton != null) buyHookButton.interactable = value;
        if (buyMoveButton != null) buyMoveButton.interactable = value;
        if (equipFrogButton != null) equipFrogButton.interactable = value;
        if (equipCrabButton != null) equipCrabButton.interactable = value;
        if (equipPenguinButton != null) equipPenguinButton.interactable = value;
        if (unequipButton != null) unequipButton.interactable = value;
    }

    private void SetTextSafe(TMP_Text textField, string value)
    {
        if (textField != null)
        {
            textField.text = value;
        }
    }

    private string FormatBuddyName(string buddyId)
    {
        if (string.IsNullOrWhiteSpace(buddyId) || buddyId == "none")
        {
            return "None";
        }

        return char.ToUpper(buddyId[0]) + buddyId.Substring(1);
    }
}