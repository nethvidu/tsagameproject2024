using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : LevelScript
{
    [field: SerializeField]
    public override string[] flags { get; set; }
    public override IEnumerator LevelEnd()
    {
        print("Level 1 ended");
        for (int i = 0; i < 100; i++)
        {
            FindObjectOfType<UI_Manager>().getElementGameObjectByName("LevelEnd").GetComponent<Image>().color = new Color(0, 0, 0, i / 120f);
            FindObjectOfType<UI_Manager>().getElementGameObjectByName("LevelEnd").GetComponentInChildren<TMP_Text>().color = new Color(1, 1, 1, i / 80f);
            yield return i;
        }
        Time.timeScale = 0;
        yield break;
    }

    public override IEnumerator StartLevel()
    {
        yield return 0;
    }

    public override IEnumerator TickLevel()
    {
        while (!GameOver)
        {
            print("boo");
            if (checkFlag("Lever1") && checkFlag("Lever2"))
            {
                GameObject.Find("Door1(Clone)").GetComponent<MapObject>().PublicInvoke(true);
            }

            if (checkFlag("Lever3"))
            {
                GameObject.Find("Door2(Clone)").GetComponent<MapObject>().PublicInvoke(true);
            }
            if (checkFlag("Lever4"))
            {
                GameObject.Find("Door3(Clone)").GetComponent<MapObject>().PublicInvoke(true);
            }
            if (checkFlag("Lever5"))
            {
                GameObject.Find("Door4(Clone)").GetComponent<MapObject>().PublicInvoke(true);
            }
            if (checkFlag("Lever6"))
            {
                GameObject.Find("Door5(Clone)").GetComponent<MapObject>().PublicInvoke(true);
            }
            if (checkFlag("Lever7"))
            {
                GameObject.Find("Door6(Clone)").GetComponent<MapObject>().PublicInvoke(true);
            }
            yield return 0;
        }

    }
}
