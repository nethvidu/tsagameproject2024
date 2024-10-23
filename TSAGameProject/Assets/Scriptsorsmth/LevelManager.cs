using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Map CurrentMap { get; private set; }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int loadMap(Map map)
    {
        try
        {
            foreach (GameObject gameObject in map.getAllMapObjects())
            {
                GameObject newGameObject = Instantiate(gameObject);
                newGameObject.transform.parent = transform;
                newGameObject.GetComponent<MapObject>().isActive = true;
                
                

            }
            return 1;
        }
        catch (Exception e)
        {
            print(e.GetBaseException());
        }
        return 0;

    }

    public int destroyMap()
    {
        try
        {
            foreach (Transform gameObject in transform)
            {
                Destroy(gameObject.gameObject);
            }
            return 1;
        }
        catch { }
        return 0;
    }
    public Map getReferenceToLevel(Enum e)
    {
        print("Levels/" + e.ToString());
        var a = Resources.Load<GameObject>("Levels/" + e.ToString()).GetComponent<Map>();
        a.transform.position = Vector3.zero;
        return a;
    }
}
