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
    public Animator transition;
    
    void Start()
    {
        game = FindObjectOfType<Game>();
        collider = GetComponent<BoxCollider2D>();
        transition = FindFirstObjectByType<Animator>();
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
            //StartCoroutine(LevelTransition());
            transition.Play("Crossfade_Start", -1, 0.0f);
            game.levelClear();
            game.loadNewLevel(Level);
            player1 = false;
            player2 = false;
        }
    }
    IEnumerator LevelTransition()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        transition.Play("Crossfade_Start");
        yield return new WaitForSecondsRealtime(1f);
        game.levelClear();
        Debug.Log("Ended Coroutine at timestamp : " + Time.time);
        game.loadNewLevel(Level);

    }
}
