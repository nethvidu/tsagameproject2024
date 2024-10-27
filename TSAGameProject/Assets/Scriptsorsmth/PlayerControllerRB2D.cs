using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerRB2D : MonoBehaviour
{
    [field: SerializeField]
    public float Health { get; set; } 
    [field: SerializeField]
    public float Velocity { get; private set; } // Can only be set by this class

    private float speed = 1f;
    private float xvelocity;
    [field: SerializeField]
    private float Max_Speed;
    private float Accel = 2f;
    private float horizontalInput;
    private Rigidbody2D rb2D;
    private float JumpForce = 10f;
    [field: SerializeField]
    private bool isjumping;
    
    private bool isfalling;
    private string animationState = "AnimationState";
    private string animationSpeed = "AnimationSpeed";
    private Animator animator;
    
    enum CharStates {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Fall = 3
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Set reference to rigidbody
        animator = GetComponent<Animator>();
        Health = 100f; // Placeholder
        isjumping = false;
        isfalling = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get horizontal input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        float horizontalMove = horizontalInput * speed;
        // Creates the movement vector/sets velocity
        // Apply force if jump button is pressed
        if (Input.GetButton("Jump") && Mathf.Abs(rb2D.velocity.y) < 0.001f)
        {
            rb2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            isjumping = true;
            isfalling = false;
        }

        if (isjumping && rb2D.velocity.y < -0.00001f) {
            isjumping = false;
            isfalling = true;
        }
        if (isfalling && rb2D.velocity.y >= 0.00001f) {
            isfalling = false;
        }
        
        
        xvelocity = Mathf.Clamp((rb2D.velocity.x + horizontalMove), -Max_Speed, Max_Speed)*0.955f;
        rb2D.velocity = new Vector2(xvelocity, rb2D.velocity.y); // Set rigidbody velocity
        Velocity = Mathf.Sqrt(rb2D.velocity.x * rb2D.velocity.x + rb2D.velocity.y * rb2D.velocity.y); // Update velocity PROPERTY

        // print("isjumping: " + isjumping + " " + " isfalling: " + isfalling);
        UpdateAnimation((xvelocity/2)*horizontalInput, rb2D.velocity.y);

    }

    void UpdateAnimation(float animSpeed, float yvelocity)
    {
        if(horizontalInput == 0 && isfalling == false) {
            animator.SetInteger(animationState, (int)CharStates.Idle);
        }
        else if (isjumping) {
            animator.SetInteger(animationState, (int)CharStates.Jump);
        }
        else if (isfalling) {
            animator.SetInteger(animationState, (int)CharStates.Fall);
        } 
        else if (horizontalInput == 1) {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetInteger(animationState, (int)CharStates.Walk);
            
        }
        else if (horizontalInput == -1) {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetInteger(animationState, (int)CharStates.Walk);
            
        }
        
        
        animator.SetFloat(animationSpeed, Mathf.Abs(animSpeed));
    }
}
