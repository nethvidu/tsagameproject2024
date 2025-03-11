using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public BoxCollider2D bearCollider;
    public Rigidbody2D Rb2D;
    bool player1;
    bool player2;
    bool triggered;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Player1"){
            player1 = true;
            
            
        } else if(collider.gameObject.name == "Player2"){
            player2 = true;
            
        } 
        if(player1 && player2 && !triggered){
            
            triggered = true;
            Debug.Log("how");
            Rb2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }

}
