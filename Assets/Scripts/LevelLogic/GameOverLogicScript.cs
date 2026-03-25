using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;





public class GameOverLogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameObject GameOverScreen;
    public GameObject nextButton;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private float PERCENT_1STAR = 0.6f;
    private float PERCENT_2STAR = 0.75f;
    private float PERCENT_3STAR = 0.92f;

    private float num_stars_achieved = 0; 

    private List<GameObject> stars = new List<GameObject>();
    private static GameOverLogicScript Instance;
 
    void Awake()
    {
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
        
    }
    void Start()
    {
       GameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
       //finalScoreText = TextMeshPro.FindGameObjectWithTag("FinalScoreText");
        //Instance = this;
       GameOverScreen.SetActive(false); 
    }
    public static GameOverLogicScript getInstance()
    {
        return Instance;
    }

    private void resetGame()
    {
        LogicScript.setDead(false);

        GameOverScreen.SetActive(false);
        LogicScript.setScore(0);
        LogicScript.resetTimer(60f);
        Debug.Log("RESET THINGS AT END OF GAME !!! \n");
    }

    public void goToLevel(int lvl) // mm idk abt putting this here ngl but idk 
    {
        SceneManager.LoadScene("Game");
        LogicScript.setLevel(lvl);
        Debug.Log("IN GO to lVEL FUNC \n");
        resetGame();
    }

    public void restartLevel()
    {
        goToLevel(LogicScript.getLevel());
        resetGame();
        
    }

    public void nextLevel()
    {
        Debug.Log("INSIDE NEXT LVL \n");
        goToLevel(LogicScript.getLevel() + 1);
        resetGame();
        Debug.Log("SWITCHed TO NEXT LVL \n");
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

    public void gameOver()
    {
        GameOverScreen.SetActive(true);
        // if its at last level, disable the next button!!
        if(LogicScript.getLevel() >= 3) 
        {
            // GameObject nextButton = GameObject.FindGameObjectWithTag("NextButton");
            nextButton.SetActive(false);
        }
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

    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.getDead() && !GameOverScreen.activeSelf)
        {
            gameOver();
        }
    }
}
