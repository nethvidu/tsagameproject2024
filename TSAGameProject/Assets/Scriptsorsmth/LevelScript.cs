using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class LevelScript : MonoBehaviour
{
    public abstract IEnumerator LevelEnd();
    public abstract IEnumerator TickLevel();

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
