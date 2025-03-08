using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReformableObject : MapObject
{
    public bool isBroken;
    public bool reform;
    public Vector2 startpos;
    public Material mat;
    SpriteRenderer spr;

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
            GetComponent<ParticleSystemRenderer>().material = mat;
            StartCoroutine(Reform(hitObject));
        }
    }
    private IEnumerator Reform(GameObject player)
    {
        GetComponent<ParticleSystem>().Play();
        spr.enabled = false;
        Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);
        yield return new WaitForSeconds(0.6f);
        spr.enabled = true;
        Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), false);
        isBroken = false;
        yield break;
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
    