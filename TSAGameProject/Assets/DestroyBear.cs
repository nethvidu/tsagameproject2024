using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBear : MonoBehaviour
{
    Average average;
    // Start is called before the first frame update
    void Start()
    {
        average = FindFirstObjectByType<Average>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -50){
            Debug.Log("this");
            average.objectC = GameObject.Find("Drone").transform;
            GameObject.Destroy(this.gameObject);
        }
    }
}
