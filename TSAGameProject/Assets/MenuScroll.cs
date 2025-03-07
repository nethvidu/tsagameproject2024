using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScroll : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollModifier = 1f;
    public float smoothness = 0.2f;
    private Vector3 startPos;
    private Vector3 velocity;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, startPos + (Camera.main.ScreenToViewportPoint(Input.mousePosition) * scrollModifier), ref velocity, smoothness);
    }
}
