using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditorInternal;

enum LEVELS {
    LEVEL0 = 0,
    LEVEL1,
    LEVEL2,
    LEVEL3,
    LEVEL4
}
public class LogicScript : MonoBehaviour
{
    private static int level;
    private static float elapsedTime;
    [SerializeField] private static float startTimeSeconds = 60;
    private static float timeLeftSeconds;

    [SerializeField] private static bool gameOver = false;

    [SerializeField] private TextMeshProUGUI timerText;   
    [SerializeField] private TextMeshProUGUI scoreText;

    private CatScript CatInstance;
    private MagnetScript MagnetInstance;

    [SerializeField] private static int score = 0;

    private static LogicScript Instance;

    public static void decreaseTime(int seconds)
    {
        elapsedTime += seconds; 
    }
    public static void increaseScore(int points)
    {
        score += points;
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
    }

    public static void setLevel(int lvl)
    {
        level = lvl;
    }
    public static int getLevel()
    {
        return level;
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
            gameOver = true;
        }
    }
    private void displayScore()
    {
        scoreText.text = score.ToString();
    }

    private void displayTime()
    {
        int minutes = Mathf.FloorToInt(timeLeftSeconds / 60);
        int seconds = Mathf.FloorToInt(timeLeftSeconds % 60);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = $"{minutes}:{seconds:00}";
        
    }

    // Update is called once per frame
    void Update()
    {
        displayTime();
        displayScore();
        if (!gameOver)
        {
            countdownTimer();
        }
        else
        {
            // display game over scene ?? 
            Debug.Log("GAME OVER\n");
        }
        

        
    }
}
