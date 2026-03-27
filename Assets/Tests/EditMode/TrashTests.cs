using NUnit.Framework;
using UnityEngine;

public class TrashTests
{
    private GameObject trashObj;
    private TrashScript trash;

    [SetUp]
    public void Setup()
    {
        trashObj = new GameObject();
        trash = trashObj.AddComponent<TrashScript>();

        LogicScript.setScore(0);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(trashObj);
    }

    [Test]
    public void HandleCollision_WithMagnet_IncreasesScore()
    {
        // Arrange
        GameObject magnet = new GameObject();
        magnet.tag = "Magnet";

        // Act
        trash.HandleCollision(magnet);

        // Assert
        Assert.AreEqual(1, LogicScript.getScore());
    }

    [Test]
    public void HandleCollision_WithNonMagnet_DoesNotIncreaseScore()
    {
        // Arrange
        GameObject other = new GameObject();
        other.tag = "Player";

        // Act
        trash.HandleCollision(other);

        // Assert
        Assert.AreEqual(0, LogicScript.getScore());
    }
}