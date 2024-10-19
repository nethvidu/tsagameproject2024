using System.Collections;
using System.Collections.Generic;
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

    public void loadMap(Map map)
    {
        foreach (GameObject gameObject in map.mapObjects)
        {
            GameObject newGameObject = Instantiate(gameObject);
            newGameObject.transform.parent = transform;
            newGameObject.GetComponent<MapObject>().isActive = true;

        }
    }
}
