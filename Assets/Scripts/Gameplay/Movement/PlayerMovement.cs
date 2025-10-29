using JetBrains.Annotations;
using System;
using System.Dynamic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour



{
    [SerializeField] PlayerStats stats;

    //Initialized Variables

    public Rigidbody2D body;

    public float xInput; 
    public float yInput;
    public bool Grounded;
    public BoxCollider2D GroundCheck;
    public LayerMask groundMask;

    //Checking if player is touching the ground
    public void CheckGround()
    {
        Grounded = Physics2D.OverlapAreaAll(GroundCheck.bounds.min, GroundCheck.bounds.max, groundMask).Length > 0;
    }

    //Movement Function

    public void Move()
    {
        //This is wrong do over
        if (Mathf.Abs(xInput) > 0)
        {
            body.linearVelocity = new Vector2(xInput * stats.Speed, body.linearVelocity.y);
        }

    }

    //Jumping Function

    public void Jumping() {

        Debug.Log(stats.hasJumped);

        if (Input.GetKeyDown(KeyCode.W) && !stats.hasJumped || Input.GetKeyDown(KeyCode.UpArrow) && !stats.hasJumped || Input.GetKeyDown(KeyCode.Space) && !stats.hasJumped)
        {

            body.AddForce(Vector2.up * stats.JumpHeight, ForceMode2D.Impulse);
            stats.hasJumped = true;
   
        }
    }

    // Testing Jump
    public void ResetJump() 
    { 
        if (Input.GetKeyDown(KeyCode.R))
        {
            stats.hasJumped = false;
        }
    }

    // Adding Air Resistance

    public void IsJumping()
    {
        if (!Grounded)
        {
            stats.Speed = 5;
        }
        else
        {
            stats.Speed = 10;
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Move();
        Jumping();
        ResetJump();
        CheckGround();
        IsJumping();

    }
      
    }

