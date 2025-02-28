using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        GetComponent<DroneMove>().enabled = false;
        animator = GetComponent<Animator>();
        animator.Play("DroneWakeup");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
