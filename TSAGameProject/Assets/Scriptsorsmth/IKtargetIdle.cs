using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKtargetIdle : MonoBehaviour
{
    public Vector2 hitpos;
    [SerializeField]
    public float maxDistance;
    [SerializeField]
    public bool isActive;
    public Transform limbSolverTarget;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame\
    void Update()
    {
        if(isActive){
            CheckGround();
            time = 0;
            if(Vector2.Distance(transform.position, limbSolverTarget.position) > maxDistance)
            { 
                limbSolverTarget.position = Vector3.Lerp(limbSolverTarget.position, limbSolverTarget.position + new Vector3(0,1,0), 1/Time.deltaTime*10);
                limbSolverTarget.position = Vector3.Lerp(limbSolverTarget.position + new Vector3(0,1,0), transform.position, 1/Time.deltaTime*10);
            }
        } 

    }
    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if(hit)
        {
            hitpos = hit.point;
            hitpos.y += 0.1f;
            transform.position = hitpos;
        }
    }
}
