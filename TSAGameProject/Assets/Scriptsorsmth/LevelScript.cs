using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class LevelScript : MonoBehaviour
{
    [property: SerializeField]
    public abstract string[] flags { get; set; }

    public Dictionary<string, bool> actualFlags = new Dictionary<string, bool>();
    public abstract IEnumerator LevelEnd();
    public abstract IEnumerator TickLevel();

    public void Start()
    {
        foreach (string flag in flags)
        {
            actualFlags.Add(flag, false);
        }
    }

    public bool checkFlag(string flag)
    {
        try { return actualFlags[flag]; } catch {  return false; }
        
    }
    
    public void triggerFlag(string flag)
    {
        try {
            actualFlags[flag] = true;
        }
        catch
        {
            print("Flag doesn't exist noob!!!");
        }
    }
    public void triggerFlag(string[] flag)
    {
        try
        {
            foreach(string f in flag)
            {
                actualFlags[f] = true;
            }
        }
        catch
        {
            print("Flag doesn't exist noob!!!");
        }
    }
    private bool gameOver;
    [property:SerializeField]
    public bool GameOver
    {
        get
        {
            return gameOver;
        }
        set
        {
            if (value)
            {
                StartCoroutine(LevelEnd());
            }
            gameOver = value;
        }
    }


}
