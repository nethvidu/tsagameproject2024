using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using MyBox;
using System.Linq;
using UnityEngine;

public class WaypointLogic : MonoBehaviour
{
    DroneMove Drone;
    public string newText;
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
        if(newText != "" || newText == null || newText == Drone.text.text){
            Drone.text.text = "";
            StartCoroutine(ChangeText());
        }
    }
    IEnumerator ChangeText(){
        foreach(char e in newText){
            Drone.text.text += e + "";
            yield return new WaitForSeconds(0.2f);
        }
    }
}
