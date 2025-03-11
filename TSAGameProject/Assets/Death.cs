using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D pc2d;
    public respawn[] RespawnPoints;
    void Start()
    {
        pc2d = GetComponent<BoxCollider2D>();
        RespawnPoints = GetComponentsInChildren<respawn>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.name == "Player2" || col.transform.name == "Player1"){
            col.transform.position = (Vector2)RespawnPoints[0].transform.position;
            StartCoroutine(Blink(col));
        }
    }
    public IEnumerator Blink(Collider2D col)
    {
        col.transform.GetComponent<PlayerControllerRB2D>().rb2D.velocity = Vector3.zero;
        col.transform.GetComponent<PlayerControllerRB2D>().enabled = false;
        col.GetComponentInChildren<Animator>().Play("Damage", -1, 0.0f);
        yield return new WaitForSeconds(1f);
        col.transform.GetComponent<PlayerControllerRB2D>().enabled = true;
        col.GetComponentInChildren<Animator>().Play("Idle", -1, 0.0f);
        yield break;
    }
}
