using TMPro;
using UnityEngine;

enum BIOME {
    BIOME1 = 1,
    BIOME2,
    BIOME3
}

enum LEVELS {
    LEVEL0 = 0,
    LEVEL1,
    LEVEL2,
    LEVEL3,
    LEVEL4
}
public class LogicScript : MonoBehaviour
{
    private static int level = 1;
    private static int biome = 2; // for now;
    private static float elapsedTime;
    [SerializeField] private static float startTimeSeconds = 60f;
    private static float timeLeftSeconds = 60f;

    private static bool startGame = false;

    [SerializeField] private static bool dead = false;

    [SerializeField] private TextMeshProUGUI timerText;   
    [SerializeField] private TextMeshProUGUI scoreText;

    private CatScript CatInstance;
    private MagnetScript MagnetInstance;
    private GameOverLogicScript GameOverLogicInstance;
    [SerializeField] private static int score = 0;

    private static LogicScript Instance;

    public static bool getStartGame()
    {
        return startGame;
    }

    public static void setStartGame(bool start)
    {
        startGame = start;
    }
    public static void setScore(int s)
    {
        score = s;
    }

    public static void setBiome(int b)
    {
        biome = b;
        //BossImgChanger.changeBossImage(biome);
        //BackgroundScript.changeBiome(biome);
    }
    public static int getBiome()
    {
        return biome;
    }

    public static void setTime( int seconds)
    {
     timeLeftSeconds = seconds;   
    }
    public static void decreaseTime(int seconds)
    {
        elapsedTime += seconds; 
    }
    public static void increaseScore(int points)
    {
        score += points;
    }
    public static int getScore()
    {
        return score;
    }
    public static float getTimeLeftSeconds()
    {
        return timeLeftSeconds;
    }

    void Start()
    {
        Instance = this;
        CatInstance = CatScript.getInstance();
        MagnetInstance = MagnetScript.getInstance();
        GameOverLogicInstance = GameOverLogicScript.getInstance();
    }

    public static void setLevel(int lvl)
    {
        level = lvl;
    }
    public static void setDead(bool d)
    {
        dead = d;
    }
    public static int getLevel()
    {
        return level;
    }

    public static bool getDead()
    {
        return dead;
    }

    public static LogicScript getInstance()
    {
        return Instance;
    }

    private void countdownTimer()
    {
        elapsedTime += Time.deltaTime; // how mcuh time elapses between each frame !!!
        timeLeftSeconds = startTimeSeconds - elapsedTime;
        if (timeLeftSeconds <= 0)
        {
            timeLeftSeconds = 0;
            dead = true;
        }
    }

    public static void resetTimer(float time)
    {
        elapsedTime = 0;
        timeLeftSeconds = time;
    }
    private void displayScore()
    {
        scoreText.text = score.ToString();
    }

    private void displayTime()
    {
        int minutes = Mathf.FloorToInt(timeLeftSeconds / 60);
        int seconds = Mathf.FloorToInt(timeLeftSeconds % 60);
        timerText.text = $"{minutes}:{seconds:00}";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            displayTime();
            displayScore();
            if (startGame) {
                countdownTimer();
            }
        }

    }
}
