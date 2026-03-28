using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4.5f;
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    [SerializeField] private Sprite noBuddySprite;
    [SerializeField] private Sprite frogBuddySprite;
    [SerializeField] private Sprite crabBuddySprite;
    [SerializeField] private Sprite penguinBuddySprite;

    private Vector2 movementDirection;

    [SerializeField] private float localStartPositionX = 1.5f;
    [SerializeField] private float localStartPositionY = 1.5f;
    [SerializeField] private float lowestPosition = -3.5f;

    private bool fishing = false;
    private bool returnStart = false;

    private static MagnetScript Instance;
    private static LogicScript LogicInstance;

    [SerializeField] private float leftOutOfBoundX;
    [SerializeField] private float rightOutOfBoundX;

    void Start()
    {
        Instance = this;
        LogicInstance = LogicScript.getInstance();

        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }

        if (mySpriteRenderer == null)
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        leftOutOfBoundX = CharacterScript.getLeftOutOfBoundX();
        rightOutOfBoundX = CharacterScript.getRightOutOfBoundX();

        if (PlayerProfileManager.Instance != null)
        {
            movementSpeed = PlayerProfileManager.Instance.GetHookSpeed();
        }

        RefreshBuddySprite();
    }

    public static MagnetScript getInstance()
    {
        return Instance;
    }

    public bool getFishing()
    {
        return fishing;
    }

    public void RefreshBuddySprite()
    {
        if (mySpriteRenderer == null)
        {
            return;
        }

        if (PlayerProfileManager.Instance == null || PlayerProfileManager.Instance.CurrentProfile == null)
        {
            mySpriteRenderer.sprite = noBuddySprite;
            return;
        }

        string equippedBuddy = PlayerProfileManager.Instance.GetEquippedBuddy();

        switch (equippedBuddy)
        {
            case "frog":
                mySpriteRenderer.sprite = frogBuddySprite;
                break;
            case "crab":
                mySpriteRenderer.sprite = crabBuddySprite;
                break;
            case "penguin":
                mySpriteRenderer.sprite = penguinBuddySprite;
                break;
            default:
                mySpriteRenderer.sprite = noBuddySprite;
                break;
        }
    }

    void Update()
    {
        
        if(!LogicScript.getDead())
        {
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            {
                fishing = true;
                returnStart = false;
                movementDirection = new Vector2(0f, -1f);
            }

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            {
                movementDirection = new Vector2(0, 1);
            }
            else if (!fishing)
            {
                GameObject catPlayer = GameObject.FindGameObjectWithTag("CatPlayer");

                if (catPlayer != null)
                {
                    float playerPositionX = catPlayer.transform.position.x;
                    float playerPositionY = catPlayer.transform.position.y;

                    transform.position = new Vector3(
                        playerPositionX + localStartPositionX,
                        playerPositionY - localStartPositionY,
                        transform.position.z
                    );
                }

                movementDirection = Vector2.zero;
            }
        }

        if (myRigidBody.linearVelocity.y > 0f)
        {
            if (transform.localPosition.y >= localStartPositionY)
            {
                movementDirection = Vector2.zero;
                fishing = false;
                returnStart = false;
            }
        }
        else if (transform.localPosition.y <= lowestPosition)
        {
            returnStart = true;
        }

        if (returnStart)
        {
            ReturnToStart();
        }

        myRigidBody.linearVelocity = movementDirection * movementSpeed;

        if ((transform.position.x < leftOutOfBoundX) && (myRigidBody.linearVelocity.x < 0f))
        {
            transform.position = new Vector3(rightOutOfBoundX, transform.position.y, transform.position.z);

            movementDirection = new Vector2(-1, 0);
        }
        else if ((transform.position.x > rightOutOfBoundX) && (myRigidBody.linearVelocity.x > 0f))
        {
            transform.position = new Vector3(leftOutOfBoundX, transform.position.y, transform.position.z);
            movementDirection = new Vector2(1f, 0f);
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Debug.Log("HIT A FISH");
            LogicScript.decreaseTime(10);

            returnStart = true;
            ReturnToStart();

            float timeLeft = LogicScript.getTimeLeftSeconds();
            if (timeLeft <= 10f)
            {
                LogicScript.decreaseTime((int)timeLeft);
            }
        }
        else if (collision.gameObject.CompareTag("Trash"))
        {
            TrashSpawnerScript.increaseNumTrashCollected(1);
            Debug.Log("FISH HIT TRASH");
        }
        else
        {
            //Debug.Log("fish hit trash\n");
        }
    }

    private void ReturnToStart()
    {
        GameObject catPlayer = GameObject.FindGameObjectWithTag("CatPlayer");
    }
}