using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapObject : MonoBehaviour // ADD THIS COMPONENT TO EACH OBJECT WITHIN THE MAP HIERARCHY
{
    [SerializeField]
    private bool isCollidableEditor = true;

    private Vector2 originalColliderSize;
    
    [field : SerializeField]
    public bool isActive {  get; set; }
    public Collider2D Confinercollider;
    [field: SerializeField]
    private bool isConfiner { get; set; }
    //Checks if an object is a cameraconfiner
   
    [field: SerializeField]
    public bool isInteractable { get; set; }
    [field: SerializeField]
    public KeyCode interactButton {  get; set; }
    [SerializeField]

    public bool isCollidable
    {   
        set
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
            if (value)
            {
                GetComponent<BoxCollider2D>().size = originalColliderSize;
            }
            
        }
    }
    void Start()
    {
        originalColliderSize = GetComponent<BoxCollider2D>().size;
        isCollidable = true; 
        //Setting the confiner collider variable here
        //How do I get this variable to the one in the script called GetConfiner in Player1Cam?
        //I'll admit I don't fully understand how this level system works, and I don't want to break it
        if (isConfiner == true) {
            Confinercollider = GetComponent<CompositeCollider2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        isCollidable = isCollidableEditor;
        
    }
}
