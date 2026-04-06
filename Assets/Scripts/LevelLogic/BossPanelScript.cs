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

        Debug.Log("BOSS PANEL START RUNNING");

        BossEndScreen.SetActive(false);
        BossStartScreen.SetActive(true);

    }

    void Start()
    {
        Debug.Log("BOSS PANEL START RUNNING");
        LeftFishSpawnerScript.setAvgSpawnRate(6);
        TrashSpawnerScript.setAvgSpawnRate(1.8f);

        // FIX !!!!
        // BossImgChanger.changeBossImage(2); // input param should be biome (1-3)
        // BossEndScreen.SetActive(false);
        // BossStartScreen.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        bool passedBoss = (GameOverLogicScript.getInstance().getNumStarsAchieved() == 3);

        if (LogicScript.getDead() && !endScreenOpened && passedBoss)
        {
            Debug.Log("TRYING TO OPEN END BOSS SCREEN! \n\n");
            openEndScreen();
            GameOverLogicScript.getInstance().removePlayAgainImage();

        }
        else if (LogicScript.getDead())
        {
            
            GameOverLogicScript.getInstance().gameOver();
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
        BossEndScreen.SetActive(false); 
        GameOverLogicScript.getInstance().gameOver();
        Debug.Log("IN CLOSE BOSS END SCREEN, OPEN GOVER SCREEN\n");
    }
    public static void openEndScreen()
    {
        Debug.Log("blue - TRYING TO SET BOSS END SCREEN ACTIVE!!! \n\n\n\n");
        //BossEndScreen = GameObject.FindGameObjectWithTag("BossEndPanel");
        Debug.Log($"BOSS END SCREEN IS NULL? : {BossEndScreen == null}\n");
        BossEndScreen.SetActive(true); 
        endScreenOpened = true; 
        
    }
}
