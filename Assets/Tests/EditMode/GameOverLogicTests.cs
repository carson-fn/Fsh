using NUnit.Framework;
using UnityEngine;
using TMPro;

public class GameOverLogicTests
{
    private GameOverLogicScript script;
    private GameObject gameObject;
    private GameObject screen;
    private GameObject nextButton;
    private TextMeshProUGUI scoreText;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        script = gameObject.AddComponent<GameOverLogicScript>();

        screen = new GameObject();
        GameOverLogicScript.GameOverScreen = screen;

        nextButton = new GameObject();
        script.nextButton = nextButton;

        scoreText = new GameObject().AddComponent<TextMeshProUGUI>();

        script.GetType()
              .GetField("finalScoreText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
              .SetValue(script, scoreText);
    }

    [Test]
    public void GameOver_ActivatesScreen()
    {
        script.gameOver();
        Assert.IsTrue(screen.activeSelf);
    }

    [Test]
    public void GameOver_Level3_DisablesNextButton()
    {
        LogicScript.setLevel(3);

        script.gameOver();

        Assert.IsFalse(nextButton.activeSelf);
    }

    [Test]
    public void GameOver_UpdatesScoreText()
    {
        LogicScript.setScore(42);

        script.gameOver();

        Assert.AreEqual("Score: 42", scoreText.text);
    }
}