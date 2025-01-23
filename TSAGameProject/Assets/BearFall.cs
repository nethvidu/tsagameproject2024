using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearFall : MonoBehaviour
{
    public string Flagtrigger;
    private Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("LevelScript(Clone)").GetComponent<LevelScript>().checkFlag(Flagtrigger)){
            animator.Play("level");
        }
    }
}
