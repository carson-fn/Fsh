using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CharacterScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Rigidbody2D myRigidBody;
    private Vector2 movementDirection;

    [SerializeField] private static float leftOutOfBoundX = -9.5f;
    [SerializeField] private static float rightOutOfBoundX = 13f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "hewoo";
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public static float getLeftOutOfBoundX()
    {
        return leftOutOfBoundX;
    }

    public static float getRightOutOfBoundX()
    {
        return rightOutOfBoundX;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Keyboard.current.aKey.isPressed)
        // {
        //     movementDirection = new Vector2(-1, 0);
        //     //transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

        // }

        // else if (Keyboard.current.dKey.isPressed)
        // {
        //     movementDirection = new Vector2(1, 0);
        //     //transform.position = transform.position + Vector3.right * moveSpeed * Time.deltaTime;

        // }
        //myRigidBody.linearVelocity = movementDirection * moveSpeed;

    }
    void FixedUpdate()
    {
        //myRigidBody.linearVelocity = movementDirection * movementSpeed;
    }



}
