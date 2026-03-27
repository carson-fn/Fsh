using UnityEngine;

public class BossPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject bossStartScreenObj;
    [SerializeField] private GameObject bossEndScreenObj;
    public static GameObject BossStartScreen;
    public static GameObject BossEndScreen;

    private static bool openGOScreen = false;

    private static bool endScreenOpened = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        BossStartScreen = bossStartScreenObj;
        BossEndScreen = bossEndScreenObj;
        endScreenOpened = false; 
        openGOScreen = false;

    }

    void Start()
    {
        Debug.Log("BOSS PANEL START RUNNING");

        BossEndScreen.SetActive(false);
        BossStartScreen.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        // BossStartScreen = GameObject.FindGameObjectWithTag("BossStartPanel");
        // Debug.Log($"BOSS START SCREEN: {BossStartScreen}\n\n\nbanana\n\n\n");
        // BossStartScreen.SetActive(true);
        if (LogicScript.getDead() && !endScreenOpened)
        {
            Debug.Log("TRYING TO OPEN END BOSS SCREEN! \n\n");
            openEndScreen();
        }
        
        
    }

    public static bool getOpenGOScreen()
    {
        return openGOScreen;
    }

    public static void openBossStartScreen()
    {
        Debug.Log("OPENED START SCREEN !!! \n");
        Debug.Log($"BossStartScreen active: {BossStartScreen.activeSelf}");
        Debug.Log($"BossStartScreen parent active: {BossStartScreen.transform.parent?.gameObject.activeSelf}");
        Debug.Log($"Canvas active: {BossStartScreen.transform.root.gameObject.activeSelf}");

        BossStartScreen.SetActive(true);
    }
    public static void closeStartScreen()
    {
        Debug.Log($"START SCREEN: {BossStartScreen}");
        Debug.Log("CLOSING START SCREEN \n\n\n");
        BossStartScreen.SetActive(false); 
        LogicScript.setStartGame(true);
        LogicScript.setTime(60);
    }

    public static void closeEndScreen()
    {
        Debug.Log("HELLOOOOOOOO\n\n\n\nLALALLALALAL\n\n\n\n\n\n");
        Debug.Log("IN CLOSE BOSS END SCREEN, OPEN GOVER SCREEN\n");
        openGOScreen = true;
        BossEndScreen.SetActive(false); 
        GameOverLogicScript.openGameOverScreen();
        //endScreenOpened = false;
        Debug.Log("IN CLOSE BOSS END SCREEN, OPEN GOVER SCREEN\n");
    }
    public static void openEndScreen()
    {
        endScreenOpened = true; 
        BossEndScreen.SetActive(true); 
    }
}
