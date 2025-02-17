using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using TMPro;
public class DroneMove : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerControllerRB2D Player1;
    public PlayerControllerRB2D Player2;
    public LayerMask ground;
    public int playerOfInterest = 0;
    Vector2 location;
    public Vector2 finalLocation = new Vector2(0, 0);
    public float Speed = 0;
    [field: SerializeField]
    //public float playerRadius { get; set; }
    public TextMeshProUGUI text;
    public Vector2 textTargetLocation;
    
    public List<GameObject> locList = new List<GameObject>();
    
    void Start()
    {
        playerOfInterest = Random.Range(0,1);
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    
    void Update()
    {
        if(locList != null){
            Move(EvaluateMove());
        }
        switch (playerOfInterest)
        {
            case 0:
                finalLocation = Player1.transform.position;
                break;
            case 1:
                finalLocation = Player2.transform.position;
                break;
        }
        
    }
    Vector3 EvaluateMove()
    {
        Vector3 Closestpos = locList[0].transform.position;
        foreach(GameObject g in locList){
            if(Vector3.Distance(g.transform.position, finalLocation) > 150) continue;
            if(Vector3.Distance(Closestpos, finalLocation) > Vector3.Distance(g.transform.position, finalLocation)){
                Closestpos = g.transform.position;
                g.GetComponent<WaypointLogic>().AccessNode();
            }
        }
        return Closestpos;
    }
    void Move(Vector2 endLocation){
        transform.position = (Vector2.Lerp(transform.position, endLocation, Time.deltaTime));
    }
    
}
