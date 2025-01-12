using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        playerOfInterest = Random.Range(0,1);
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, Mathf.Sin(Time.time*1f)*0.001f, 0);
        CheckMovement();
        Idle();
    }
    void CheckMovement()
    {
        if(Player1.rb2D.velocity.magnitude > 1){
            playerOfInterest = 0;
            Speed = Player1.rb2D.velocity.magnitude * 20;
        } else if(Player2.rb2D.velocity.magnitude > 1){
            playerOfInterest = 1;
            Speed = Player2.rb2D.velocity.magnitude * 20;
        } else {
            Speed = 0;
        }
    }
    void Idle() 
    {
        
        Vector2 point;
        float angle = Random.Range(0, 360);
        Vector2 angleVector = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        RaycastHit2D hit = Physics2D.Raycast(location, angleVector, 5, ground);
        if(hit){
            point = hit.point + hit.normal * 3;
        } else {
            point = location + angleVector * 3;
        }
        if(playerOfInterest == 0){
            location = Player1.transform.position;
        } else if(playerOfInterest == 1){
            location = Player2.transform.position;
        }
        if(Random.Range(1, 30) == 1){
           finalLocation = point;
        }
        transform.position = Vector2.Lerp(transform.position, finalLocation, Mathf.SmoothStep(0.0f, Speed,Time.deltaTime));

    }
}
