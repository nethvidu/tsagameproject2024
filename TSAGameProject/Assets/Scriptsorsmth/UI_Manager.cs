using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public T getElementByName<T>(string name)
    {
        return transform.Find(name).gameObject.GetComponent<T>();
    }

    public GameObject getElementGameObjectByName(string name)
    {
        return transform.Find(name).gameObject;
    }
}
