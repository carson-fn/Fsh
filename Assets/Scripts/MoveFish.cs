using UnityEngine;

public class FishMove : MonoBehaviour {
    public float speed = 2f;
    public float leftBound = 0f;
    public float rightBound = 1920f;
    public int direction = 1;

    [Header("Bobbing")]
    public float bobHeight = 10f;
    public float bobSpeed = 2f;

    private float startY;

    void Start() {
        startY = transform.position.y;
    }

    void Update() {
        // Horizontal movement
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Vertical bobbing
        float newY = startY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (transform.position.x > rightBound && direction == 1) {
            direction = -1;
            Flip();
        }
        else if (transform.position.x < leftBound && direction == -1) {
            direction = 1;
            Flip();
        }
    }

    void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}