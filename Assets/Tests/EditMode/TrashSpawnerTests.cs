using NUnit.Framework;
using UnityEngine;

public class TrashSpawnerTests
{
    private GameObject obj;
    private TrashSpawnerScript spawner;

    [SetUp]
    public void Setup()
    {
        obj = new GameObject();
        spawner = obj.AddComponent<TrashSpawnerScript>();

        // Reset static values
        SetPrivateStaticField("numTrashCollected", 0);
        SetPrivateStaticField("totalNumTrash", 0);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(obj);
    }

    // Helper to reset private static fields
    private void SetPrivateStaticField(string fieldName, int value)
    {
        var field = typeof(TrashSpawnerScript).GetField(fieldName,
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        field.SetValue(null, value);
    }

    [Test]
    public void IncreaseNumTrashCollected_IncrementsCorrectly()
    {
        TrashSpawnerScript.increaseNumTrashCollected(2);

        Assert.AreEqual(2, TrashSpawnerScript.getNumTrashCollected());
    }

    [Test]
    public void GetNumTrashCollected_ReturnsCorrectValue()
    {
        TrashSpawnerScript.increaseNumTrashCollected(1);

        Assert.AreEqual(1, TrashSpawnerScript.getNumTrashCollected());
    }

    [Test]
    public void SpawnTrash_IncreasesTotalCount()
    {
        // Arrange
        spawner.Trash1 = new GameObject();

        int before = TrashSpawnerScript.getTotalNumTrash();

        // Act
        // call private method using reflection
        var method = typeof(TrashSpawnerScript).GetMethod("spawnTrash",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        method.Invoke(spawner, null);

        int after = TrashSpawnerScript.getTotalNumTrash();

        // Assert
        Assert.AreEqual(before + 1, after);
    }
}