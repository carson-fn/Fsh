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


    }
}
