using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

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

    public int loadMap(Map map, bool overridePlayerCamBounds = true)
    {
        GameObject boundsTransform = Instantiate(map.GetComponentInChildren<CompositeCollider2D>().gameObject);
        boundsTransform.transform.SetParent(transform, true);
        if (overridePlayerCamBounds)
        {
            GameObject.Find("Player1Cam").GetComponent<CinemachineConfiner>().m_BoundingShape2D = boundsTransform.GetComponent<CompositeCollider2D>();
        }
        try
        {
            foreach (GameObject gameObject in map.getAllMapObjects())
            {
                GameObject newGameObject = Instantiate(gameObject);
                newGameObject.transform.SetParent(boundsTransform.transform, true);
                try
                {
                    newGameObject.GetComponent<MapObject>().isActive = true;
                }
                catch { }

            }
            boundsTransform.GetComponent<CompositeCollider2D>().GenerateGeometry(); // VERY IMPORTANT!!!! Regenerates composite collider after all children are present
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
