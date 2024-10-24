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
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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
        
    }

    // Update is called once per frame
    void Update()
    {
        isCollidable = isCollidableEditor;
        
    }
}
