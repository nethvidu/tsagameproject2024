using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerRB2D : MonoBehaviour
{
    [field: Header("Stats")]
    [field: SerializeField]
    public float Health { get; set; }
    [field: SerializeField]
    public bool isDashing;
    [field: Header("GroundCheck Settings")]
    [field: SerializeField]
    public LayerMask groundLayer;
    [field: SerializeField]  
    private float groundCheckRadius;
    [field: SerializeField] 
    private float groundCheckOffset;
    [field: Header("Player Config")]
    [field: SerializeField] 
    private float Max_Speed = 0.1f;
    [field: SerializeField]
    private float JumpForce = 10f;
    [field: SerializeField]
    private float DashForce = 20f;
    [field: SerializeField]
    private float DashPeriod = 1f; // Max time the dash force will ignore player velocity clamp check
    [field: Header("Debug")]
    [field: SerializeField] 
    private float speed = 1f;
    [field: SerializeField] 
    private float horizontalInput;
    private Rigidbody2D rb2D;
    public Animator animator;
    [field: SerializeField] 
    public bool fall;
    [field: SerializeField] 
    private bool Jump;
    [field: SerializeField]  
    private bool isGrounded;
    [field: SerializeField]
    public float Velocity { get; private set; } // Can only be set by this class
    public GameObject Avatar;
    public ParticleSystem dust;
    [field: SerializeField]
    public string LeftRight;
    [field: SerializeField]
    public string JumpControl;

    // Flags for dashing
    [field: SerializeField]
    private bool press1 = false;
    [field: SerializeField]
    private int direction;
    [field: SerializeField]
    private float press1Time;
    [field: SerializeField]
    private bool isLetGo;
    private float dashTime;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Set reference to rigidbody
        Health = 100f; // Placeholder
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if dash expires
        if (dashTime + DashPeriod < Time.time)
        {
            isDashing = false;
        }
        // Get horizontal input
        horizontalInput = Input.GetAxisRaw(LeftRight);
        // Dash logic
        if (direction != (int)Mathf.Sign(horizontalInput))
        {
            isLetGo = false;
            press1 = false;
        }
        if (horizontalInput != 0 && !press1)
        {
            press1 = true;
            press1Time = Time.time;
            direction = (int)Mathf.Sign(horizontalInput);
        }
        if (press1Time + PlayerConfig.dashInputWindow >= Time.time && !(horizontalInput != 0f))
        {
            isLetGo = true;
        }
        if (press1 && (int)Mathf.Sign(horizontalInput) == direction && press1Time + PlayerConfig.dashInputWindow >= Time.time && isLetGo && !isDashing)
        {
            rb2D.AddForce(new Vector2(DashForce * direction, 0), ForceMode2D.Impulse);
            print("Dash!!!!");
            dashTime = Time.time;
            isDashing = true;
            press1 = false;
            isLetGo=false;
        }
        else if (press1Time + PlayerConfig.dashInputWindow < Time.time)
        {
            isLetGo = false;
            press1 = false;
        }
        float horizontalMove = horizontalInput * speed;       
    

        // Creates the movement vector/sets velocity
        // Apply force if jump button is pressed
        if (Input.GetButton(JumpControl) && isGrounded && !Jump)
        {
            rb2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            Jump = true;
            CreateDust();
        }
        if(rb2D.velocity.y < 0.001f)
        {
            fall = true;
            Jump = false;
        }
        if(isGrounded && !Jump)
        {
            Jump = false;
            fall = false;
        }
        rb2D.velocity = isDashing ? 
            new Vector2(Mathf.Clamp(rb2D.velocity.x + horizontalMove, -Max_Speed - DashForce, Max_Speed + DashForce) * 0.955f, rb2D.velocity.y):
            new Vector2(Mathf.Clamp(rb2D.velocity.x + horizontalMove, -Max_Speed, Max_Speed)*0.955f, rb2D.velocity.y); // Set rigidbody velocity
        Velocity = Mathf.Sqrt(rb2D.velocity.x * rb2D.velocity.x + rb2D.velocity.y * rb2D.velocity.y); // Update velocity PROPERTY
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(0, groundCheckOffset), groundCheckRadius, groundLayer);
        AnimateAvatar();
    }
    void AnimateAvatar()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput * rb2D.velocity.x)); ;
        animator.SetBool("Jump", Jump);
        animator.SetBool("Fall", fall);
        if(horizontalInput > 0) {
            Avatar.transform.localScale = new Vector3(2,2,2);
            
        }
        else if(horizontalInput < 0) {
            Avatar.transform.localScale = new Vector3(-2,2,2);
            
        }
    }
    void CreateDust()
    {
        dust.Play();
    }
}
