//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatScript : MonoBehaviour
{
    private static CatScript Instance;
    
    private bool fishing;
    [SerializeField] private float movementSpeed = 3f;
    public Rigidbody2D myRigidBody;
    private Vector2 movementDirection;

    [SerializeField] private float leftOutOfBoundX;
    [SerializeField] private float rightOutOfBoundX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        fishing = false;
        gameObject.name = "cattt";
        myRigidBody = GetComponent<Rigidbody2D>();
        leftOutOfBoundX = CharacterScript.getLeftOutOfBoundX();
        rightOutOfBoundX = CharacterScript.getRightOutOfBoundX();
        
    }

    public static CatScript getInstance()
    {
        return Instance;
    }

    // Update is called once per frame
    void Update()
    {
        fishing = MagnetScript.getInstance().getFishing();
        if (Keyboard.current.aKey.isPressed && !fishing)
        {
            movementDirection = new Vector2(-1, 0);
        }

        else if (Keyboard.current.dKey.isPressed && !fishing)
        {
            movementDirection = new Vector2(1, 0);
        }
        else if (fishing)
        {
            movementDirection = new Vector2(0, 0);
        }
        // else if (Keyboard.current.fKey.isPressed)
        // {
        //     movementDirection = new Vector2(0, 0);
           
        // }

        if((transform.position.x < leftOutOfBoundX) && (myRigidBody.linearVelocityX < 0))
        {
            transform.position = new Vector3(rightOutOfBoundX, transform.position.y, transform.position.z);
            Debug.Log("OUT OF BOUNDS ON LEFT\n");
            movementDirection = new Vector2(-1, 0);
        }
        else if((transform.position.x > rightOutOfBoundX) && (myRigidBody.linearVelocityX > 0))
        {
            Debug.Log("OUT OF BOUNDS ON RIGHT\n");
            transform.position = new Vector3(leftOutOfBoundX, transform.position.y, transform.position.z);
            movementDirection = new Vector2(1, 0);
        }
        myRigidBody.linearVelocity = movementDirection * movementSpeed;

    }

    void FixedUpdate()
    {
        //myRigidBody.linearVelocity = movementDirection * movementSpeed;
    }
}
