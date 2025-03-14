using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using MyBox;
using System.Linq;


public class WaypointLogic : MonoBehaviour
{
    DroneMove Drone;

    public int playerOfInterest = 1;
    public bool shouldUpdateText;
    public bool nodeAccessed;
    public string[] newText;
    // Start is called before the first frame update
    void Start()
    {
        Drone = FindObjectOfType<DroneMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector3.Dot(Camera.main.transform.forward,(myObject.position-transform.position).normalized)>0.5f){
            
        //}
        //FindObjectOfType<DroneMove>().locList.Add(this.gameObject);
        
    }
    public void AccessNode(){
        if(this.shouldUpdateText && !this.nodeAccessed){
            Drone.playerOfInterest = playerOfInterest;
            Drone.text.text = "";
            StartCoroutine(ChangeText());
            Debug.Log("dialogue");
        }
    }
    IEnumerator ChangeText(){
        this.nodeAccessed = true;
        float Speed = 0.05f;
        for(int i = 0; i < newText.Length; i++){
            foreach(char e in newText[i].ToCharArray()){
                Drone.text.text += e;
                yield return new WaitForSeconds(Speed);
            }
            yield return new WaitForSeconds(1f);
            Drone.text.text = "";
        }
        yield break;

    }
}
