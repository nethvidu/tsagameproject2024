using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class DroneMove : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerControllerRB2D Player1;
    public PlayerControllerRB2D Player2;
    public LayerMask ground;
    public int playerOfInterest = 0;
    Vector2 location;
    public Vector2 finalLocation = new Vector2(0, 0);
    public float Speed = 0;
    [field: SerializeField]
    public float playerRadius { get; set; }
public Transform textLocation;
    public Vector2 textTargetLocation;

    public Spline dronePathSpline;
    private float currentSplineIndex = 0;
    void Start()
    {
        playerOfInterest = Random.Range(0,1);
        textLocation = GameObject.Find("DroneText").transform;
    }
    private GameObject currentNode;

    // Update is called once per frame
    /*
    void FixedUpdate()
    {

        CheckMovement();
        if(Player1.rb2D.velocity.magnitude < 1 && Player2.rb2D.velocity.magnitude < 1){
            transform.position += new Vector3(0, Mathf.Sin(Time.time*1f)*0.005f, 0);
        } else {
            Idle();
        }
        transform.position = Vector2.Lerp(transform.position, finalLocation, Mathf.SmoothStep(0.0f, Speed,Time.deltaTime));
        TextMove();
        
    }
    */
    void Update()
    {
        print(Physics2D.OverlapCircleAll(transform.position, 0.5f).ToString());
        splineCheck();
        Collider2D col = (from c in Physics2D.OverlapCircleAll(transform.position, 0.5f).ToList() where c.gameObject.GetComponent<DronePathNode>() != null select c).FirstOrDefault();
        if (col != null)
        {
            col.gameObject.GetComponent<DronePathNode>().AnimateText();
        }
        currentNode = col.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        if (collision.gameObject.GetComponent<DronePathNode>() != null)
        {
            collision.gameObject.GetComponent<DronePathNode>().AnimateText();
        }
    }
    void splineCheck()
    {
        if (!isPlayerWithinRadius())
        {
            GetComponent<SplineAnimate>().Pause();
            return;
        }
        try
        {
            if (!GetComponent<SplineAnimate>().IsPlaying && !currentNode.GetComponent<DronePathNode>().continueOnCondition)
            {
                GetComponent<SplineAnimate>().Play();
            }
            else if (currentNode.GetComponent<DronePathNode>().continueOnCondition)
            {
                if (FindObjectOfType<LevelScript>().checkFlag(currentNode.GetComponent<DronePathNode>().flagTriggeredBy))
                {
                    GetComponent<SplineAnimate>().Play();
                }
                else
                {
                    GetComponent<SplineAnimate>().Pause();
                }
            }
        }
        catch
        {
            GetComponent<SplineAnimate>().Play();
        }

    }
    bool isPlayerWithinRadius()
    {
        return Vector2.Distance(Player1.transform.position, transform.position) < playerRadius || Vector2.Distance(Player2.transform.position, transform.position) < playerRadius;
    }
    void CheckMovement()
    {
        if(Player1.rb2D.velocity.magnitude > 1){
            playerOfInterest = 0;
            Speed = Player1.rb2D.velocity.magnitude * 10;
            location = Player1.transform.position;
        } else if(Player2.rb2D.velocity.magnitude > 1){
            playerOfInterest = 1;
            location = Player2.transform.position;
            Speed = Player2.rb2D.velocity.magnitude * 10;
        } else {
            Speed = 10;
        }
    }
    void Idle() 
    {
        
        Vector2 point;
        float angle = Random.Range(0, 180);
        Vector2 angleVector = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        RaycastHit2D hit = Physics2D.Raycast(location, angleVector, 5, ground);
        if(Vector2.Distance(location, transform.position) >= 2){
            angle = Random.Range(0, 180);
            angleVector = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            hit = Physics2D.Raycast(location, angleVector, 5, ground);
            point = location + angleVector * 3;
        }
        if(hit){
            point = hit.point + hit.normal * 3;
        } else {
            point = location + angleVector * 3;
        }
        if(Random.Range(1, 30) == 1){
           finalLocation = point;
        }

    }
    void TextMove() 
    {
       
        Vector2 point;
        float angle = Random.Range(0, 360);
        Vector2 angleVector = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        RaycastHit2D hit = Physics2D.Raycast(finalLocation, angleVector, 2, ground);
        if(hit){
            point = hit.point + hit.normal * 2;
        } else {
            point = new Vector2(transform.position.x,transform.position.y) + angleVector * 2;
        }
        if(Physics2D.OverlapCircle(point, 3.0f, ground)){
            angle = Random.Range(0, 360);
        }
        textLocation.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint((Vector2.Lerp(transform.position, point, Mathf.SmoothStep(0.0f, Speed,Time.deltaTime) / 6f)) - new Vector2(0, 0.6f));


    }    
}
