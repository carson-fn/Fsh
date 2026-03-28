using UnityEngine;

public class FishMove2 : MonoBehaviour {
    public float speed2 = 2f;
    public float leftBound2 = 0f;
    public float rightBound2 = 1920f;

    private int direction2 = -1; // 1 = right, -1 = left

    void Update2() {
        transform.Translate(Vector2.right * direction2 * speed2 * Time.deltaTime);

        // Turn around at screen edges
        if (transform.position.x > rightBound2) {
            direction2 = -1;
            Flip2();
        }
        else if (transform.position.x < leftBound2) {
            direction2 = 1;
            Flip2();
        }
    }

    void Flip2() {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // flip sprite
        transform.localScale = scale;
    }
}