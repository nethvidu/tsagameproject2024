using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update\
    public PlayerControllerRB2D[] players;
    public enum Players
    {
        Player1,
        Player2

    }
    public Players PlayerToSpawn;
    public Game game;
    void Start()
    {
        game = FindObjectOfType<Game>();
        players = game.players;
        if(PlayerToSpawn == Players.Player1){
            players[0].transform.position = transform.position;
            players[0].fall = true;
            players[0].animator.Play("Fall");
        } else if(PlayerToSpawn == Players.Player2){
            players[1].transform.position = transform.position;
            players[1].fall = true;
            players[1].animator.Play("Fall");
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
