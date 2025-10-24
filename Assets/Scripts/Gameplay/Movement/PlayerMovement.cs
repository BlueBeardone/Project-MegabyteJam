using JetBrains.Annotations;
using System;
using System.Dynamic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour



{
    public PlayerStats stats;

    //Initialized Variables

    public Rigidbody2D body;

    public float xInput; 
    public float yInput;

    

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
    


    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Move();
        Jumping();
        ResetJump();
        
    }
      
    }

