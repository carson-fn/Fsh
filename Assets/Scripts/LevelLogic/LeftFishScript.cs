using UnityEngine;

public class LeftFishScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float deadZone = -10;

    [SerializeField] private float scaleMultiplier = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

        if ((transform.position.x < deadZone) || (LogicScript.getDead()))
        {
            DestroyFish();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Magnet")
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            Debug.Log("fish hit by magnet\n");
            Vector3 currentScale = transform.localScale;

            Vector3 newScale = new Vector3( currentScale.x * scaleMultiplier,
            currentScale.y * scaleMultiplier,
            currentScale.z);

            TrashSpawnerScript.decreaseNumTrashCollected(0.2f); 
            // additional penalty for hitting fish, since stars based off of % trash collected

            transform.localScale = newScale;
            Invoke("DestroyFish", 2f); // calls the destroyfish func after 2 seconds
            
            
        }
    }
    void DestroyFish()
    {
        Destroy(gameObject);
    }
    
}
