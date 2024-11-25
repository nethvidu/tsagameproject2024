using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MapObject
{
    public bool isBroken;
    public Material mat;
    void Update()
    {
        
    }

    void Start()
    {

    }

    public void Break()
    {
        if (!isBroken) {
            isBroken = true;
            isCollidable = false;
            GetComponent<ParticleSystemRenderer>().material = mat;
            GetComponent<ParticleSystem>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
    