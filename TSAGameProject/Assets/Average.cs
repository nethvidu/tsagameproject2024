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
    public float distance;
    [field: SerializeField] 
    public CinemachineVirtualCamera Camera;
    [field: SerializeField] 
    public float currentSize;
    void Start() {
    }
    void Update()
    {
        if (objectA != null && objectB != null)
        {
            // Calculate the midpoint
            Vector3 averagePosition = (objectA.position + objectB.position) / 2;
            distance = Vector2.Distance(objectA.position, objectB.position);

            // Place the target object at the midpoint
            transform.position = averagePosition;
            if(distance > 8){
                Camera.m_Lens.OrthographicSize = Mathf.Lerp(Camera.m_Lens.OrthographicSize, (distance-currentSize), Time.deltaTime * 2);
                if(distance > 15) {
                    Camera.m_Lens.OrthographicSize = Mathf.Lerp(Camera.m_Lens.OrthographicSize, currentSize, Time.deltaTime * 2);
                }
            } 
            else {
                Camera.m_Lens.OrthographicSize = Mathf.Lerp(Camera.m_Lens.OrthographicSize, currentSize, Time.deltaTime * 2);
            }
        }
    }
}