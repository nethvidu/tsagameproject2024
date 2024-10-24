using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Levels.Level Level = Levels.Level.id01; //Placeholder
    private LevelManager lvlMgr;
<<<<<<< Updated upstream
  
=======
    public PlayerControllerRB2D[] players;

>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        lvlMgr = FindObjectOfType<LevelManager>(); 
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Level));
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
 