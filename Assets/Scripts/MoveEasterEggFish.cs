using UnityEngine;

public class MoveEasterEggFish : MonoBehaviour {

    [Header("Bobbing")]
    public float bobHeight = 10f;
    public float bobSpeed = 2f;

    private float startY;

    void Start() {
        startY = transform.position.y;
    }

    void Update() {
        float newY = startY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}