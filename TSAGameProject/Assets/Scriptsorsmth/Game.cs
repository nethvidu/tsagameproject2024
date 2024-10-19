using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Levels.Level Level = Levels.Level.id01; //Placeholder
    private LevelManager lvlMgr;
    private int c = 0;
    // Start is called before the first frame update
    void Start()
    {
        lvlMgr = FindObjectOfType<LevelManager>(); 
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Level));
    }

    // Update is called once per frame
    void Update()
    {
        lvlMgr.destroyMap();
        c = (c + 1) % 2;
        if (c == 0)
        {
            lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Levels.Level.id01));
        }
        else
        {
            lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Levels.Level.id00_tutorial));
        }


    }
}
 