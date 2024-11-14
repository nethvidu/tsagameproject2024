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
    [field: SerializeField]
    public GameObject Avatar;
    private float speed = 1f;
    private float Max_Speed = 12f;
    private float Accel = 2f;
    private float horizontalInput;
    private Rigidbody2D rb2D;
    private float JumpForce = 10f;
    public Animator animator;
    public bool fall;
    private bool Jump;
    public ParticleSystem dust;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Set reference to rigidbody
        Health = 100f; // Placeholder
        
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
            Jump = true;
            CreateDust();
        }
        else if(rb2D.velocity.y < 0.001f)
        {
            fall = true;
            Jump = false;
        }
        if(Mathf.Abs(rb2D.velocity.y) < 0.0001f)
        {
            Jump = false;
            fall = false;
        }
        rb2D.velocity = new Vector2(Mathf.Clamp((rb2D.velocity.x + horizontalMove), -Max_Speed, Max_Speed)*0.955f, rb2D.velocity.y); // Set rigidbody velocity
        Velocity = Mathf.Sqrt(rb2D.velocity.x * rb2D.velocity.x + rb2D.velocity.y * rb2D.velocity.y); // Update velocity PROPERTY
        AnimateAvatar();
    }
    void AnimateAvatar()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));
        animator.SetBool("Jump", Jump);
        animator.SetBool("Fall", fall);
        if(horizontalInput > 0) {
            Avatar.transform.localScale = new Vector3(2,2,2);
            CreateDust();
        }
        else if(horizontalInput < 0) {
            Avatar.transform.localScale = new Vector3(-2,2,2);
            CreateDust();
        }
    }
    void CreateDust()
    {
        dust.Play();
    }
}
