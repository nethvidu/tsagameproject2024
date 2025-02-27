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
    [field: SerializeField]
    void Start()
    {
        game = FindObjectOfType<Game>();
        players = game.players;
        if(PlayerToSpawn == Players.Player1){
            players[0].playerStart = transform.position;
        } else if(PlayerToSpawn == Players.Player2){
            players[1].playerStart = transform.position;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
