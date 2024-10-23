using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetConfiner : MonoBehaviour
{
    // Start is called before the first frame update
    public Map CurrentMap { get; private set; }
    private CinemachineConfiner Confiner;
    public Collider2D BoundingShape;
    void Start()
    {
        //Sets the confiner property to the boundingshape variable
        Confiner.m_BoundingShape2D = BoundingShape;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
