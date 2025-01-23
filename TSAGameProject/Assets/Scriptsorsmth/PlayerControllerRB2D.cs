using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
// using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.InputSystem.Interactions;
public class PlayerControllerRB2D : MonoBehaviour
{
    public int playerNum;
    [field: Header("Stats")]
    [field: SerializeField]
    public float Health { get; set; }
    [field: SerializeField]
    public bool isDashing = false;
    [field: Header("GroundCheck Settings")]
    [field: SerializeField]
    public LayerMask ground;
    [field: SerializeField]  
    private float groundCheckRadius;
    [field: SerializeField] 
    private float groundCheckOffset;
    [field: Header("Player Config")]
    [field: SerializeField] 
    private float Max_Speed = 0.1f;
    [field: SerializeField]
    public Rigidbody2D rb2D;
    public Animator animator;
    [field: SerializeField] 
    public bool fall;
    [field: SerializeField] 
    private bool jump;
    [field: SerializeField]  
    private bool isGrounded;
    [field: SerializeField]
    public float Velocity { get; private set; } // Can only be set by this class
    public GameObject Avatar;
    public ParticleSystem dust;

    private int direction;
    [field: SerializeField]
    public float Accel = 5.0f;
    public float Decel = 0.57f;
    public Vector2 move;
    public string MoveInput;
    public string JumpInput;
    public float jumpHeight;
    InputAction jumpAction;
    InputAction moveAction;
    private string movePress = "";
    public float lastTime = 0f;
    public float sign;
    public int dashLimit;
    public float dashLength;
    public float dashForce;

    private int dashCount = 0;
    private int tapCount = 0;
    private float timeLastPress = 0f;
    public float dashForgiveness;
    public float dashGravity;
    public Vector2 lastMove = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Set reference to rigidbody
        moveAction = InputSystem.actions.FindAction(MoveInput);
        Health = 100f; // Placeholder
        jumpAction = InputSystem.actions.FindAction(JumpInput);
        moveAction.Enable();
        moveAction.started += context => {
            if (context.interaction is HoldInteraction)
            {
                movePress = "Hold";
            }
            else if (context.interaction is TapInteraction)
            {
                movePress = "Tap";
            }
        };
        moveAction.canceled += _ => movePress = "";
    }

    public void ResetPos()
    {
        transform.position = GameObject.Find($"_P{playerNum}Spawn(Clone)").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        move = moveAction.ReadValue<Vector2>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100, ground);
        Debug.DrawRay(transform.position, Vector2.down * 100, Color.red);
        if(move.magnitude > 0){
            lastMove = move;
        }
        if(rb2D.velocity.y < 0.001f)
        {
            fall = true;
            jump = false;
        }
        if(isGrounded && !jump)
        {
            jump = false;
            fall = false;
        }
        if (isDashing && Time.time-lastTime >= dashLength){ // 0.2 is dash length, fix later
            rb2D.velocity = rb2D.velocity * 0.2f; //Slow player at end of dash
            rb2D.gravityScale = 1; //reset gravity
            transform.eulerAngles = new Vector3(0, 0, 0); //reset rotation
            if(direction == 1) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,2);
            }
            else if(direction == -1) {
                transform.localScale = new Vector3(-(Mathf.Abs(transform.localScale.x)),transform.localScale.y,2);
            }
            isDashing = false;
            
        }
        if(hit.collider != null && (movePress != "") || (movePress != null)){
            if (movePress == "Tap" && !isDashing)
            {
                tapCount++;
                if(tapCount == 1){
                    timeLastPress = Time.time;
                }
                

                if (isGrounded && lastMove.y > 0  && !(jump))
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + jumpHeight/2);
                    jump = true;
                    dashCount = 0;
                    movePress = "";
                    tapCount = 0;
                } else if(dashCount <= dashLimit && tapCount > 1) {
                    Dash();
                }
                movePress = "";
            }
            else if (movePress == "Hold" && !isDashing)
            {
                 if (isGrounded && lastMove.y > 0  && !(jump))
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + jumpHeight);
                    jump = true;
                    dashCount = 0;
                    movePress = "";
                    tapCount = 0;
                }
                if (Mathf.Abs(rb2D.velocity.x) <= Max_Speed && Mathf.Abs(move.x) > 0)
                {
                    rb2D.AddForce(Vector3.ProjectOnPlane(Vector2.right, hit.normal) * (move.x * Accel), ForceMode2D.Impulse);
                }
                else
                {
                    rb2D.AddForce(-rb2D.velocity.x * Decel * Vector3.ProjectOnPlane(Vector2.right, hit.normal), ForceMode2D.Impulse);
                }
            }
        }
        if(Time.time - timeLastPress >= dashForgiveness && tapCount >= 1){
            tapCount = 0;
            dashCount = 0;
        } 
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(0, groundCheckOffset), groundCheckRadius, ground);
        AnimateAvatar();
    }

    void Update()
    {
        
        if (isDashing && transform.name == "Player2")
        {
 
            Collider2D[] a = Physics2D.OverlapBoxAll(transform.position + new Vector3(direction * 0.5f, 0f , 0f), new Vector2(0.5f, 1.2f), 35);
            a.ToList().ForEach(delegate(Collider2D a)
            {
                if (a.GetComponent<BreakableObject>() != null)
                {
                    a.GetComponent<BreakableObject>().Break();
                }
            });
        }
        if (transform.position.y < -50) ResetPos();
    }
    
    void AnimateAvatar()
    {
        animator.SetFloat("Speed", Mathf.Abs(move.x * rb2D.velocity.x)); 
        animator.SetBool("Jump", jump);
        animator.SetBool("Fall", fall);
        animator.SetBool("Dash", isDashing);
        if(move.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,2);
            direction = 1;
            
        }
        else if(move.x < 0) {
            transform.localScale = new Vector3(-(Mathf.Abs(transform.localScale.x)),transform.localScale.y,2);
            direction = -1;
            
        }
    }
    void CreateDust()
    {
        dust.Play();
    }
    void Dash()
    {
        isDashing = true;
        rb2D.velocity = new Vector2(0, 0);
        Debug.Log("Dash you fool");
        dashCount++;
        rb2D.gravityScale = dashGravity;
        sign = Vector2.Angle(Vector2.up, lastMove);
        rb2D.velocity = (rb2D.velocity + lastMove*dashForce);// * 10 is dash force, fix later
        lastMove = new Vector2(0, 0);
        lastTime = Time.time;
        movePress = "";
        tapCount = 0;
        
    }
}
