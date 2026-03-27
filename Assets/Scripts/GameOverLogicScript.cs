using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;





public class GameOverLogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameObject GameOverScreen;
    public GameObject nextButton;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private static GameOverLogicScript Instance;
 
    void Awake()
    {
        Instance = this;
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

    public int CalculateCoinsEarned(int score, float trashPercent)
    {
        float scorePart = score / 10f;
        float trashMultiplier = 1f + (trashPercent / 100f);
        int coinsEarned = Mathf.RoundToInt(scorePart * trashMultiplier);

        return Mathf.Max(1, coinsEarned);
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

        float percentTrashCollected = (float) TrashSpawnerScript.getNumTrashCollected() / TrashSpawnerScript.getTotalNumTrash();
        // try displaying the stars one at a time idk ... need to implememnt still 
        Debug.Log($"percent trash collected: {percentTrashCollected}\n");
        if (percentTrashCollected >= 0.5)
        {
            Debug.Log("1 STAR\n");
        }
        if (percentTrashCollected >= 0.75)
        {
            Debug.Log("2 STAR\n");
        }
        if (percentTrashCollected >= 0.92)
        {
            Debug.Log("3 STAR\n");
        }

        int coinsEarned = CalculateCoinsEarned( LogicScript.getScore(), percentTrashCollected);
        PlayerProfileManager.Instance.AddCoins(coinsEarned);
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
