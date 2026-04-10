using UnityEngine;
using UnityEngine.InputSystem;

public class CatScript : MonoBehaviour
{
    private static CatScript Instance;

    public static Sprite buddyLiam;
    public static Sprite buddyHarold;
    public static Sprite buddyCarson;

    private static float startX = -5.3f;
    private static float startY = 3f;
    private static float startZ = 0f;
    
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

        myRigidBody = GetComponent<Rigidbody2D>();
        leftOutOfBoundX = CharacterScript.getLeftOutOfBoundX();
        rightOutOfBoundX = CharacterScript.getRightOutOfBoundX();
        
        if (PlayerProfileManager.Instance != null)
        {
            movementSpeed = PlayerProfileManager.Instance.GetMoveSpeed();
        }
    }
    public static void setPlayerToStart()
    {
        Instance.transform.position = new Vector3(startX, startY, startZ);
    }

    public static CatScript getInstance()
    {
        return Instance;
    }

    // Update is called once per frame
    void Update()
    {
        fishing = MagnetScript.getInstance().getFishing();

        if(LogicScript.getDead())
        {
            movementDirection = new Vector2(0, 0);
        }
        else
        {
            bool leftKeyPressed = Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed;
            bool rightKeyPressed = Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed;

            if (leftKeyPressed && !fishing)
            {
                movementDirection = new Vector2(-1, 0);
            }
            else if (rightKeyPressed && !fishing)
            {
                movementDirection = new Vector2(1, 0);
            }
            else if (fishing)
            {
                movementDirection = new Vector2(0, 0);
            }


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
        
        myRigidBody.linearVelocity = movementDirection * movementSpeed;

    }

}
