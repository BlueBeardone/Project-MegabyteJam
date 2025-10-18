using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour



{
    public Rigidbody2D body;

 

    [SerializeField] PlayerStats stats;



    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");


        // For more fluid movement

        if (Mathf.Abs(xInput) > 0)
        {
            body.linearVelocity = new Vector2(xInput * stats.Speed, body.linearVelocity.y);
        }

        if (Mathf.Abs(yInput) > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, yInput * stats.Speed);
        }


        Vector2 direction = new Vector2(xInput, yInput).normalized;
        body.linearVelocity = direction * stats.Speed;



    }

    
    }

