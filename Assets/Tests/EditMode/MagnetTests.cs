using NUnit.Framework;
using UnityEngine;

public class MagnetTests
{
    private GameObject obj;
    private MagnetScript magnet;

    [SetUp]
    public void Setup()
    {
        obj = new GameObject();
        magnet = obj.AddComponent<MagnetScript>();

        LogicScript.resetTimer(60f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(obj);
    }

    [Test]
    public void GetFishing_Default_IsFalse()
    {
        Assert.IsFalse(magnet.getFishing());
    }

    [Test]
    public void Collision_WithFish_DecreasesTime()
    {
        // Arrange
        GameObject fish = new GameObject();
        fish.tag = "Fish";

        float before = LogicScript.getTimeLeftSeconds();

        Collision2D collision = TestUtils.CreateCollision(fish);

        // Act
        magnet.OnCollisionEnter2D(collision);

        float after = LogicScript.getTimeLeftSeconds();

        // Assert
        Assert.Less(after, before);
    }

    [Test]
    public void Collision_WithTrash_IncreasesTrashCollected()
    {
        // Arrange
        GameObject trash = new GameObject();
        trash.tag = "Trash";

        TrashSpawnerScript.setNumTrashCollected(0); // must exist

        Collision2D collision = TestUtils.CreateCollision(trash);

        // Act
        magnet.OnCollisionEnter2D(collision);

        // Assert
        Assert.AreEqual(1, TrashSpawnerScript.getNumTrashCollected());
    }
}