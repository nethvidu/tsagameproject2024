using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Levels.Level Level = Levels.Level.id01; //Placeholder
    private LevelManager lvlMgr;
    public PlayerControllerRB2D[] players;

    void Start()
    {
        lvlMgr = FindObjectOfType<LevelManager>(); 
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Level));
        this.startLevel(lvlMgr);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void startLevel(LevelManager mgr)
    {
        StartCoroutine(transform.parent.GetComponentInChildren<LevelScript>().TickLevel());
    }
}
 