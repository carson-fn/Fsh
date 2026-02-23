using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class GameOverLogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameObject GameOverScreen;
    [SerializeField] private static TextMeshProUGUI finalScoreText;
 
    void Start()
    {
       GameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
       //finalScoreText = GameObject.FindGameObjectWithTag("finalScoreText");

       GameOverScreen.SetActive(false); 
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

    public static void gameOver()
    {
        GameOverScreen.SetActive(true);
        finalScoreText.text = $"Score: {LogicScript.getScore()}";
    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.getDead())
        {
            gameOver();
        }
    }
}
