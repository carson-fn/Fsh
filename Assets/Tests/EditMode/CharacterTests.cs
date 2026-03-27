using NUnit.Framework;

public class CharacterTests
{
    [Test]
    public void GetLeftOutOfBoundX_ReturnsCorrectValue()
    {
        float result = CharacterScript.getLeftOutOfBoundX();

        Assert.AreEqual(-9.5f, result);
    }

    [Test]
    public void GetRightOutOfBoundX_ReturnsCorrectValue()
    {
        float result = CharacterScript.getRightOutOfBoundX();

        Assert.AreEqual(13f, result);
    }

    [Test]
    public void LeftBound_IsLessThan_RightBound()
    {
        float left = CharacterScript.getLeftOutOfBoundX();
        float right = CharacterScript.getRightOutOfBoundX();

        Assert.Less(left, right);
    }
}