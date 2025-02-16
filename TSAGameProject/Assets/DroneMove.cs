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
    
    public List<GameObject> locList = new List<GameObject>();
    void Start()
    {
        playerOfInterest = Random.Range(0,1);
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
    
        
    }
    void Move(Vector2 endLocation){
        
    }
    
}
