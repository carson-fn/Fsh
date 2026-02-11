using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class MagnetScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.5f;
    public Rigidbody2D myRigidBody;
    private Vector2 movementDirection;
    [SerializeField] private float localStartPositionX = 1.5f;

    // 1.4f
    [SerializeField] private float localStartPositionY = 1.5f;
    //-1.313f;

    [SerializeField] private float lowestPosition = -3.5f;
    

    private bool fishing = false;

    private bool returnStart = false;

    private static MagnetScript Instance;

    private static LogicScript LogicInstance;

    [SerializeField] private float leftOutOfBoundX;
    [SerializeField] private float rightOutOfBoundX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        // localStartPositionX = Instance.transform.localPosition.x;
        // localStartPositionY = Instance.transform.localPosition.y;
        LogicInstance = LogicScript.getInstance();
        myRigidBody = GetComponent<Rigidbody2D>();
        leftOutOfBoundX = CharacterScript.getLeftOutOfBoundX();
        rightOutOfBoundX = CharacterScript.getRightOutOfBoundX();
        
    }

    public static MagnetScript getInstance()
    {
        return Instance;
    }
    public bool getFishing()
    {
        return fishing;
    }

    // Update is called once per frame
    void Update()
    {
        
        // if (Keyboard.current.aKey.isPressed && !fishing)
        // {
        //     movementDirection = new Vector2(-1, 0);
        // }
        

        // else if (Keyboard.current.dKey.isPressed && !fishing)
        // {
        //     movementDirection = new Vector2(1, 0);
        // }
        if (Keyboard.current.fKey.isPressed && !fishing)
        {
            fishing = true;
            movementDirection = new Vector2(0, -1);
        }
        else if (!fishing)
        {
            float PlayerPositionX = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.x;
        float PlayerPositionY = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.y;
            transform.position = new Vector3(PlayerPositionX + localStartPositionX, 
            PlayerPositionY - localStartPositionY, 
            transform.position.z);
            
        }

        if (myRigidBody.linearVelocityY > 0)
        {
            if ((transform.localPosition.y >= localStartPositionY))
            {
                movementDirection = new Vector2(0, 0);
                fishing = false;
                returnStart = false;
                Debug.Log("MAGNET RETURNED TO STARTING POSTION\n");
            }
        }
 
        else if ((transform.localPosition.y <= lowestPosition))
        {
            Debug.Log("TRYING TO RETURN TO START\n");
            returnStart = true;
            returnToStart();
        }

        if (returnStart)
        {
            returnToStart();
            // if ((transform.localPosition.y >= localStartPositionY) && 
            // (transform.localPosition.x == localStartPositionX))
            // {
            //     returnStart = false;
            //     fishing = false;
            //     movementDirection = new Vector2(0, 0);
            // }
        }
        myRigidBody.linearVelocity = movementDirection * movementSpeed;

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
        
    }
    void FixedUpdate()
    {
        //myRigidBody.linearVelocity = movementDirection * movementSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            Debug.Log("HIT A FISH\n");
            LogicScript.decreaseTime(10); // decrease time by 10 seconds 
            returnStart = true;
            returnToStart();
            float timeLeft = LogicScript.getTimeLeftSeconds();
            if (timeLeft <= 10) {
                LogicScript.decreaseTime((int) timeLeft);
            }
        }
        else if (collision.gameObject.tag == "Trash")
        {
            Debug.Log("fish hit trash\n");
        }
        else
        {
            Debug.Log("HIT SMTH ??? \n");
            //movementDirection = new Vector2(-1, 1); // hehe 
        }
        
    }

    private void returnToStart()
    {
        float PlayerPositionX = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.x;
        float PlayerPositionY = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.y;
        //Debug.Log(" GOT PLAYER POSITION\n");
        movementDirection = new Vector2((PlayerPositionX + localStartPositionX - transform.position.x)/movementSpeed, 
        (PlayerPositionY + localStartPositionY - transform.position.y)/movementSpeed);

        //movementDirection = new Vector2((1.4f - transform.localPosition.x)/movementSpeed, (-1.3f - transform.localPosition.y)/movementSpeed);
    }
}
