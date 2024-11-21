using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelScript
{
    [field: SerializeField]
    public override string[] flags { get; set; }
    public override IEnumerator LevelEnd()
    {
        print("Level 1 ended");
        yield return 0;
    }

    public override IEnumerator TickLevel()
    {
        while (!GameOver)
        {
            if (checkFlag("TestFlag"))
            {
                print("Flag tripped");
            }
            yield return 0;
        }

    }
}
