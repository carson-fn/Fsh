using NUnit.Framework;

public class LogicTests
{
    [SetUp]
    public void Setup()
    {
        LogicScript.setScore(0);
        LogicScript.setLevel(0);
        LogicScript.setDead(false);
        LogicScript.resetTimer(60f);
    }

    [Test]
    public void SetScore_UpdatesScore()
    {
        LogicScript.setScore(10);
        Assert.AreEqual(10, LogicScript.getScore());
    }

    [Test]
    public void IncreaseScore_AddsPoints()
    {
        LogicScript.setScore(5);

        LogicScript.increaseScore(3);

        Assert.AreEqual(8, LogicScript.getScore());
    }

    [Test]
    public void SetLevel_UpdatesLevel()
    {
        LogicScript.setLevel(2);
        Assert.AreEqual(2, LogicScript.getLevel());
    }

    [Test]
    public void SetDead_UpdatesDeadState()
    {
        LogicScript.setDead(true);
        Assert.IsTrue(LogicScript.getDead());
    }

    [Test]
    public void ResetTimer_SetsCorrectTime()
    {
        LogicScript.resetTimer(45f);

        Assert.AreEqual(45f, LogicScript.getTimeLeftSeconds());
    }
}