using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] LoadOnStart;
    void Awake()
    {
        LoadOnStart.ForEach(a => a.SetActive(false));

        transform.Find("Start").GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(TransitionBlack(1, "Loading..."));
            FindObjectOfType<Game>().StartCour(FindObjectOfType<Game>().GetMap(), LoadOnStart);
            


        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TransitionBlack(float delay = 0, string text = null)
    {
        GameObject b = transform.Find("Black").gameObject;
        b.SetActive(true);
        b.transform.Find("Text").GetComponent<TMP_Text>().text = text;
        // lerp transform
        for (int i = 0; i < 50; i++)
        {
            b.transform.position = new Vector2(b.transform.position.x, Mathf.Lerp(b.transform.position.y, 0, 0.02f));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(delay);
        foreach (Transform t in transform)
        {
            if (t.name != "Black") t.gameObject.SetActive(false);
        }
        for (int i = 0; i < 100; i++)
        {
            b.transform.position = new Vector2(b.transform.position.x, Mathf.Lerp(b.transform.position.y, 2000, 0.05f));
            yield return new WaitForSeconds(0.01f);
        }
        b.SetActive(false);
       
    }
}
