public interface IPlayerProfileStore
{
    PlayerProfileData LoadProfile(string userId);
    void SaveProfile(PlayerProfileData profile);
}