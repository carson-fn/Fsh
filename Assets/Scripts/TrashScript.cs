using UnityEngine;

public class TrashScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Magnet")
        {
            LogicScript.increaseScore(1);
            Debug.Log("trash hit by magnet\n");
            Destroy(gameObject);
            
        }
    }
}
