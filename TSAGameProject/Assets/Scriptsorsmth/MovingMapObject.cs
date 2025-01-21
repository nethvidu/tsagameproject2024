using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MovingMapObject : MapObject
{
    new public bool isMoving = true;
    public enum MovementType
    {
        Linear,
        LinearSmoothed,
        Circular,
        Sinusoidal,
        None
    }
    [field: SerializeField]
    public MovementType movementType { get; set; }

    [field: SerializeField]
    public float movementSpeed { get; set; }
    [field: SerializeField]
    public float movementCooldown { get; set; }

    [field: SerializeField]
    public GameObject movePoint { get; set; }

    [field: SerializeField]
    public bool repeat { get; set; }
    [field: SerializeField]
    public bool isTriggered = false;
    [ConditionalField("isTriggered", false)]
    public string flagTriggeredBy;
    [field: SerializeField]
    public Vector2 Vel { get; set; }

    private Vector2 originalPos;
    private bool isMovingBack = false;
    public override void onStart()
    {
        originalPos = transform.position;
    }
    private Vector3 prevPos;
    public void FixedUpdate()
    {
        print(FindObjectOfType<LevelScript>().checkFlag(flagTriggeredBy));
        if (isTriggered && !FindObjectOfType<LevelScript>().checkFlag(flagTriggeredBy))
        {
            return;
        }
        prevPos = transform.position;

        if (movementType == MovementType.Linear)
        {
            if (Vector2.Distance(transform.position, movePoint.transform.position) > 0.001f && !isMovingBack)
            {
                transform.position = Vector2.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.fixedDeltaTime);
            }
            else if (isMovingBack   )
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPos, movementSpeed * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, originalPos) < 0.001f)
                {
                    isMovingBack = false;
                }
            }
            else
            {

                StartCoroutine(MoveCooldown());
            }
        }
        else if (movementType == MovementType.LinearSmoothed)
        {
            if (Vector2.Distance(transform.position, movePoint.transform.position) > 0.005f && !isMovingBack)
            {
                transform.position = Vector2.Lerp(transform.position, movePoint.transform.position, movementSpeed * Time.fixedDeltaTime);
            }
            else if (isMovingBack)
            {
                transform.position = Vector2.Lerp(transform.position, originalPos, movementSpeed * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, originalPos) < 0.005f)
                {
                    isMovingBack = false;
                }
            }
            else
            {
                StartCoroutine(MoveCooldown());
            }
        }
         Vel = (transform.position - prevPos) / Time.fixedDeltaTime;        
    }

    public IEnumerator MoveCooldown()
    {
        if (repeat)
        {
            isMovingBack = !isMovingBack;
        }
        yield return new WaitForSeconds(movementCooldown);

    }


}
