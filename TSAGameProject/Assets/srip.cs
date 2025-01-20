using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srip : MonoBehaviour
{

    public Game game;
    public Levels.Level Level;
    
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
