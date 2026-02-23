using UnityEngine;
using TMPro;


public class GameOverLogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private TextMeshProUGUI finalScoreText;
 
    void Start()
    {
       //GameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
       //finalScoreText = GameObject.FindGameObjectWithTag("GameOverScreen");

       GameOverScreen.SetActive(false); 
    }

    private void gameOver()
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
