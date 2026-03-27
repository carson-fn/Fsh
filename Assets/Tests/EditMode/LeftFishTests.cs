using NUnit.Framework;
using UnityEngine;

public class LeftFishTests
{
    private GameObject fishObj;
    private LeftFishScript fish;

    [SetUp]
    public void Setup()
    {
        fishObj = new GameObject();
        fish = fishObj.AddComponent<LeftFishScript>();

        // Add collider (required)
        fishObj.AddComponent<BoxCollider2D>();

        // Set initial scale
        fishObj.transform.localScale = new Vector3(1, 1, 1);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(fishObj);
    }

    [Test]
    public void Collision_WithMagnet_DisablesCollider()
    {
        // Arrange
        GameObject magnet = new GameObject();
        magnet.tag = "Magnet";

        Collision2D collision = TestUtils.CreateCollision(magnet);

        // Act
        fish.OnCollisionEnter2D(collision);

        // Assert
        Assert.IsFalse(fishObj.GetComponent<Collider2D>().enabled);
    }

    [Test]
    public void Collision_WithMagnet_IncreasesScale()
    {
        // Arrange
        GameObject magnet = new GameObject();
        magnet.tag = "Magnet";

        Collision2D collision = TestUtils.CreateCollision(magnet);

        Vector3 before = fishObj.transform.localScale;

        // Act
        fish.OnCollisionEnter2D(collision);

        Vector3 after = fishObj.transform.localScale;

        // Assert
        Assert.Greater(after.x, before.x);
        Assert.Greater(after.y, before.y);
    }
}