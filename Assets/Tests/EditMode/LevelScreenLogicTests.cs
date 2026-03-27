using NUnit.Framework;
using UnityEngine;

public class LevelScreenLogicTests
{
    private GameObject obj;
    private LevelScreenLogic script;

    [SetUp]
    public void Setup()
    {
        obj = new GameObject();
        script = obj.AddComponent<LevelScreenLogic>();

        LogicScript.setLevel(0);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(obj);
    }

    [Test]
    public void GoToLevel_SetsCorrectLevel()
    {
        // Act
        script.goToLevel(2);

        // Assert
        Assert.AreEqual(2, LogicScript.getLevel());
    }

    [Test]
    public void GoToLevel_ChangesLevelFromDifferentValue()
    {
        LogicScript.setLevel(1);

        script.goToLevel(3);

        Assert.AreEqual(3, LogicScript.getLevel());
    }
}