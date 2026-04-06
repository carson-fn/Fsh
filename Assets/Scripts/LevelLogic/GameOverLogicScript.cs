using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;





public class GameOverLogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameObject GameOverScreen;
    public GameObject nextButton;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private float BOSS_LVL = -1;
    private float PERCENT_1STAR = 0.6f;
    private float PERCENT_2STAR = 0.75f;
    private float PERCENT_3STAR = 0.88f;

    private int num_stars_achieved = 0; 

    private List<GameObject> stars = new List<GameObject>();
    private static GameOverLogicScript Instance;
 
    void Awake()
    {
        Debug.Log($"XXX Awake/Start running: {this.GetType().Name}");
        Instance = this;
        GameObject star1 = GameObject.FindGameObjectWithTag("Star1");
        GameObject star2 = GameObject.FindGameObjectWithTag("Star2");
        GameObject star3 = GameObject.FindGameObjectWithTag("Star3");
        stars.Add(star1);
        stars.Add(star2);
        stars.Add(star3);
        for (int i = 0; i < 3; i++)
        {
            stars[i].SetActive(false);
        }
        BOSS_LVL = LevelScreenLogic.getBOSS_LVL();
        
    }
    void Start()
    {
        GameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
        //finalScoreText = TextMeshPro.FindGameObjectWithTag("FinalScoreText");
        Instance = this; // was commented out ? ..
        GameOverScreen.SetActive(false); 
        Debug.Log("GAME OVER SCREEN SET TO FALSE\n");
    }
    public static GameOverLogicScript getInstance()
    {
        return Instance;
    }
    public int getNumStarsAchieved()
    {
        return num_stars_achieved;
    }

    public void removePlayAgainImage()
    {
        GameObject playAgainImg = GameObject.FindGameObjectWithTag("PlayAgainImage");
        playAgainImg.SetActive(false);
    }

    private void resetGame()
    {
        LogicScript.setDead(false);
        
        CatScript.setPlayerToStart();
        GameOverScreen.SetActive(false);
        LogicScript.setScore(0);
        LogicScript.resetTimer(60f);     

        // BossSpriteChangerScript spriteChanger = FindObjectOfType<BossSpriteChangerScript>();
        // if (spriteChanger != null)
        // {
        //     spriteChanger.changeSprite(LogicScript.getBiome());
        // }
        //BossImgChanger.changeBossImage(LogicScript.getBiome());
        Debug.Log($"IN RESET LVL, BIOME IS {LogicScript.getBiome()}");


    }

    public void goToLevel(int lvl) // mm idk abt putting this here ngl but idk 
    {
        LogicScript.setLevel(lvl);
        Debug.Log("IN GO to lVEL FUNC \n");
        resetGame();
        if (lvl == BOSS_LVL)
        {
            Debug.Log($"GOING TO BOSS LVL:{lvl}\n");
            LogicScript.setStartGame(false);
            SceneManager.LoadScene("Biome1BossLvl");

        }
        else
        {
            Debug.Log($"GOING TO LVL:{lvl}\n");
            LogicScript.setStartGame(true);
            SceneManager.LoadScene("Biome1GameLvl");
        }     
    }

    public void restartLevel()
    {
        goToLevel(LogicScript.getLevel());
        
    }

    public void nextLevel()
    {
        goToLevel(LogicScript.getLevel() + 1);
    }

    public void goHome()
    {
        SceneManager.LoadScene("TestStartScreen");
        resetGame();
    }

    private void displayStars()
    {
        for(int i = 0; i < num_stars_achieved; i++)
        {
           stars[i].SetActive(true); 
        }
        

    }

    public static void openGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }

    public void gameOver()
    {
 
        openGameOverScreen();

        Debug.Log("IN GAME OVER SCREEN DISPLAYED \n");
        finalScoreText.text = $"Score: {LogicScript.getScore()}";
        Debug.Log("SCORE DISPLAYED\n");

        float percentTrashCollected = (float) TrashSpawnerScript.getNumTrashCollected() / TrashSpawnerScript.getTotalNumTrashSpawned();
        // try displaying the stars one at a time idk ... need to implememnt still 
        Debug.Log($"percent trash collected: {percentTrashCollected}\n");
        
        if (percentTrashCollected >= PERCENT_3STAR)
        {
            Debug.Log("3 STAR\n");
            num_stars_achieved = 3;
        }
        else if (percentTrashCollected >= PERCENT_2STAR)
        {
            Debug.Log("2 STAR\n");
            num_stars_achieved = 2;
        }
        else if (percentTrashCollected >= PERCENT_1STAR)
        {
            Debug.Log("1 STAR\n");
            num_stars_achieved = 1;
        }
        Debug.Log($"NUM STARS ACHIEVED: {num_stars_achieved}\n");
        displayStars();

        LogicScript.setStartGame(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.getDead() && (!GameOverScreen.activeSelf) && (LogicScript.getLevel() != BOSS_LVL))
        {
            gameOver();
        }
        
    }
}
