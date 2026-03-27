using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class MagnetScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4.5f;
    [SerializeField] private Rigidbody2D myRigidBody;

    [SerializeField] private SpriteRenderer mySpriteRenderer;

    public Sprite jellyfishSprite;
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
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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
        
        if(!(LogicScript.getDead()))
        {
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            {
                fishing = true;
                returnStart = false;
                movementDirection = new Vector2(0, -1);
            }
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            {
                movementDirection = new Vector2(0, 1);
            }

            else if (!fishing)
            {
                float PlayerPositionX = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.x;
                float PlayerPositionY = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.y;
                transform.position = new Vector3(PlayerPositionX + localStartPositionX, 
                PlayerPositionY - localStartPositionY, 
                transform.position.z);
                
            }
                
        }
       
        

        if (myRigidBody.linearVelocityY > 0)
        {
            if ((transform.localPosition.y >= localStartPositionY))
            {
                movementDirection = new Vector2(0, 0);
                fishing = false;
                returnStart = false;
            }
        }
 
        else if ((transform.localPosition.y <= lowestPosition))
        {
            returnStart = true;
            returnToStart();
        }

        if (returnStart)
        {
            returnToStart();
        }
        myRigidBody.linearVelocity = movementDirection * movementSpeed;

        if((transform.position.x < leftOutOfBoundX) && (myRigidBody.linearVelocityX < 0))
        {
            transform.position = new Vector3(rightOutOfBoundX, transform.position.y, transform.position.z);
            movementDirection = new Vector2(-1, 0);
        }
        else if((transform.position.x > rightOutOfBoundX) && (myRigidBody.linearVelocityX > 0))
        {
            transform.position = new Vector3(leftOutOfBoundX, transform.position.y, transform.position.z);
            movementDirection = new Vector2(1, 0);
        }
        
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
            TrashSpawnerScript.increaseNumTrashCollected(1);
            //Debug.Log("fish hit trash\n");
        }
        
    }

    private void returnToStart()
    {
        float PlayerPositionX = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.x;
        float PlayerPositionY = GameObject.FindGameObjectWithTag("CatPlayer").transform.position.y;
        //Debug.Log(" GOT PLAYER POSITION\n");
        movementDirection = new Vector2((PlayerPositionX + localStartPositionX - transform.position.x)/movementSpeed, 
        (PlayerPositionY + localStartPositionY - transform.position.y)/movementSpeed);

    }
}
