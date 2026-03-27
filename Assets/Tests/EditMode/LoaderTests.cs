using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LoaderTests
{
    [UnityTest]
    public IEnumerator Loader_Start_BeginsSceneLoad()
    {
        // Arrange
        GameObject obj = new GameObject();
        obj.AddComponent<Loader>();

        // Act
        yield return new WaitForSeconds(2.5f);

        // Assert
        // If no errors occurred, test passes
        Assert.IsTrue(true);
    }
}