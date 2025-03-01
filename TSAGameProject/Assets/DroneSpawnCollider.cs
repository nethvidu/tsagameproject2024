using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawnCollider : MonoBehaviour
{
    public BoxCollider2D collider;
    public DroneMove droneMove;
    public bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        droneMove.animator.Play("DroneInactive", -1, 0.0f);
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Player1" && once){
            droneMove = FindObjectOfType<DroneMove>();
            Debug.Log("bru");
            StartCoroutine(DroneAnim());
            once = false;
        }
    }
    IEnumerator DroneAnim()
    {
        yield return new WaitForSecondsRealtime(4f);
        droneMove.animator.Play("DroneActivate", -1, 0.0f);
        yield return new WaitForSecondsRealtime(1f);
        droneMove.enabled = true;
        droneMove.animator.enabled = false;
        this.enabled = false;

    }
}
