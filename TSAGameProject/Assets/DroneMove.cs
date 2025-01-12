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

        CheckMovement();
        if(Player1.rb2D.velocity.magnitude < 1 && Player2.rb2D.velocity.magnitude < 1){
            transform.position += new Vector3(0, Mathf.Sin(Time.time*1f)*0.005f, 0);
        } else {
            Idle();
        }
        transform.position = Vector2.Lerp(transform.position, finalLocation, Mathf.SmoothStep(0.0f, Speed,Time.deltaTime));
        
    }
    void CheckMovement()
    {
        if(Player1.rb2D.velocity.magnitude > 1){
            playerOfInterest = 0;
            Speed = Player1.rb2D.velocity.magnitude * 10;
        } else if(Player2.rb2D.velocity.magnitude > 1){
            playerOfInterest = 1;
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
        if(Vector2.Distance(location, transform.position) <= 2){
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
        if(playerOfInterest == 0){
            location = Player1.transform.position;
        } else if(playerOfInterest == 1){
            location = Player2.transform.position;
        }
        if(Random.Range(1, 30) == 1){
           finalLocation = point;
        }

    }
}
