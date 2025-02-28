using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public Levels.Level Level = Levels.Level.id03; 
    public Game game;
    public bool test = false;
    private bool player1 = false;
    private bool player2 = false;
    public BoxCollider2D collider;
    
    void Start()
    {
        game = FindObjectOfType<Game>();
        collider = GetComponent<BoxCollider2D>();
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
        if(player1 && player2){
            game.levelClear();
            game.loadNewLevel(Level);
        }
    }
}
