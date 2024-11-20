using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelScript
{
    public override IEnumerator LevelEnd()
    {
        print("Level 1 ended");
        yield return 0;
    }

    public override IEnumerator TickLevel()
    {
        while (!GameOver)
        {
            print("Ticked");
            yield return 0;
        }

    }
}
