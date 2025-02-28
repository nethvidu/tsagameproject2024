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

    [field: SerializeField]
    public Dictionary<string, bool> actualFlags = new Dictionary<string, bool>();
    public abstract IEnumerator LevelEnd();
    public abstract IEnumerator TickLevel();

    public abstract IEnumerator onLevelStart();


    public void LevelStart()
    {
        actualFlags.Clear();
        foreach (string flag in flags)
        {
            actualFlags.Add(flag, false);
        }
        StartCoroutine(onLevelStart());
    }

    public bool checkFlag(string flag)
    {
        try { return actualFlags[flag]; } catch {  return false; }
        
    }
    
    public void triggerFlag(string flag)
    {
        try {
            actualFlags[flag] = !actualFlags[flag];
        }
        catch
        {
            actualFlags.Add(flag, true);
        }
    }
    public void triggerFlag(string[] flag)
    {
        try
        {
            foreach(string f in flag)
            {
                actualFlags[f] = !actualFlags[f];
            }
        }
        catch
        {
            foreach (string f in flag)
            {
                actualFlags.Add(f, true);
            }
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
