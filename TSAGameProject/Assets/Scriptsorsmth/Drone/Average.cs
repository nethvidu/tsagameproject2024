using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Average: MonoBehaviour
{
    [field: SerializeField] 
    public Transform objectA; 
    [field: SerializeField] 

    public Transform objectB; 
    [field: SerializeField] 
    public Transform objectC; 
    [field: SerializeField] 
    public float distance;
    [field: SerializeField] 
    public Vector3 averagePosition;
    public CinemachineVirtualCamera Camera;
    [field: SerializeField] 
    public float currentSize;
    void Start() {
    }
    void Update()
    {
        if (objectA != null && objectB != null && objectC != null)
        {
            // Calculate the midpoint
            if(Vector2.Distance((objectA.position + objectB.position) / 2, objectC.position) < 5){
                averagePosition = (objectA.position + objectB.position + objectC.position) / 3;
                Camera.m_Lens.OrthographicSize = Mathf.Lerp(Camera.m_Lens.OrthographicSize, currentSize/6+(distance), Time.deltaTime * 2);
            } else {
                averagePosition = (objectA.position + objectB.position) / 2;
                Camera.m_Lens.OrthographicSize = Mathf.Lerp(Camera.m_Lens.OrthographicSize, currentSize/6+(distance), Time.deltaTime * 2);
            }
            distance = Vector2.Distance(objectA.position, objectB.position);

            // Place the target object at the midpoint
            transform.position = averagePosition + new Vector3(0,0.7f,0);
            
        }
    }
}