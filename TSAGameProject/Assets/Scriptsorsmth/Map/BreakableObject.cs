using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MapObject
{
    public bool isBroken;
    public Material mat;
    public Vector2 startpos;
    public SpriteRenderer spr;
    void Update()
    {
        
    }

    void Start()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        startpos = spr.gameObject.transform.position;
    }

    public void Break(GameObject hitObject)
    {
        if (!isBroken) {
           
            isBroken = true;
            isCollidable = false;
            spr.enabled = true; 
            GetComponent<ParticleSystem>().Play();
            spr.enabled = false;
            FindObjectOfType<LevelScript>().triggerFlag(triggerFlag);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.name == "Player2"){
            Debug.Log("OnCollisionEnter2D");
            StartCoroutine(Tremble());
        }
    }
    private IEnumerator Tremble() {
           for ( int i = 0; i < 25; i++)
           {
               spr.gameObject.transform.position = startpos + new Vector2(Random.Range(-0.02f,0.02f),Random.Range(-0.02f,0.02f));
               yield return new WaitForSeconds(0.01f);
           }
    }
}
    