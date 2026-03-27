using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LeftFishSpawnerTests
{
    private GameObject spawnerObj;
    private LeftFishSpawnerScript spawner;

    [SetUp]
    public void Setup()
    {
        spawnerObj = new GameObject();
        spawner = spawnerObj.AddComponent<LeftFishSpawnerScript>();

        // Initialize list manually (IMPORTANT)
        spawner.fishes = new List<GameObject>();

        // Create dummy fish prefabs
        spawner.leftFish1 = new GameObject("Fish1");
        spawner.leftFish2 = new GameObject("Fish2");
        spawner.leftFish3 = new GameObject("Fish3");
        spawner.leftFish4 = new GameObject("Fish4");

        // Set level so index is valid
        LogicScript.setLevel(2);

        // Call Start manually
        spawner.Start();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(spawnerObj);
    }

    [Test]
    public void Start_AddsAllFishToList()
    {
        Assert.AreEqual(4, spawner.fishes.Count);
    }

    [Test]
    public void SpawnLeftFish_DoesNotCrash_WithValidLevel()
    {
        // This indirectly tests index safety
        Assert.DoesNotThrow(() =>
        {
            spawner.SendMessage("spawnLeftFish");
        });
    }
}