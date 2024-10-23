using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Levels.Level Level = Levels.Level.id01; //Placeholder
    private LevelManager lvlMgr;

    // Start is called before the first frame update
    void Start()
    {
        lvlMgr = FindObjectOfType<LevelManager>(); 
        //compositecollider = colliderObject.GetComponent<CompositeCollider2D>();
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Level));
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
 