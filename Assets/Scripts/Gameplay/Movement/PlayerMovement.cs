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

        Vector2 direction = new Vector2(xInput, yInput).normalized;
        body.linearVelocity = direction * stats.Speed;
        
    }

    
}
