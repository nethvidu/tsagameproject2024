using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Map : MonoBehaviour
{
    public LevelScript LevelScript;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] getAllMapObjects()
    {
        List<GameObject> mapObjs = new List<GameObject>();
        Assert.AreNotEqual(GetComponentsInChildren<MapObject>(), null);
        foreach (MapObject gameObj in GetComponentsInChildren<MapObject>())
        {
            mapObjs.Add(gameObj.gameObject);
        }
        mapObjs.Add(LevelScript.gameObject);
        return mapObjs.ToArray();

    }
    
}
