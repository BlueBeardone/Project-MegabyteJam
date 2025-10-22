using JetBrains.Annotations;
using System;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour



{
    [SerializeField] PlayerStats stats;

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

        Vector2 direction = new Vector2(xInput, yInput).normalized;
        body.linearVelocity = direction * stats.Speed;
    }


    //Jumping Function

    public void Jumping() {

        Debug.Log(stats.hasJumped);

        if (Input.GetKeyDown(KeyCode.W) && !stats.hasJumped)
        {

            body.AddForce(Vector2.up * stats.JumpHeight, ForceMode2D.Impulse);
            stats.hasJumped = true;
        }
    

    
    }




    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Move();
        Jumping();

    }
      
    }


    

    








    
    

