using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public void HandleCollision(GameObject other)
    {
        if (other.tag == "Magnet")
        {
            LogicScript.increaseScore(1);
            Debug.Log("trash hit by magnet\n");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }
}

using UnityEngine;

public static class TestUtils
{
    public static Collision2D CreateCollision(GameObject obj)
    {
        var collision = (Collision2D)System.Activator.CreateInstance(typeof(Collision2D), true);

        var field = typeof(Collision2D).GetField("m_GameObject",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        field.SetValue(collision, obj);

        return collision;
    }
}