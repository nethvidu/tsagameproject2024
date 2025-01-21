using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using MyBox;

public class MapObject : MonoBehaviour // ADD THIS COMPONENT TO EACH OBJECT WITHIN THE MAP HIERARCHY
{
    private readonly bool isMoving = false;
    public enum PlayerToInteract
    {
        Player1,
        Player2,
        Both
    }

    public enum InteractableType {
        Door,
        Lever,
        Button,
        PressurePlate,
        Freebody,
        Pickup,
        None
    }
    [SerializeField]
    private bool isCollidableEditor = true;

    private Vector2 originalColliderSize;

    private float currentInteractPercentage;

    private bool isInteracting;

    private PlayerControllerRB2D[] players;

    private GameObject RadialProgress;
    [field : SerializeField]
    public bool isActive {  get; set; }
    [field: SerializeField]
    public bool isInteractable { get; set; }
    [field: SerializeField]
    public InteractableType interactableType;
    [Tooltip("Only this player can use this interactable")]
    [field: SerializeField]
    public PlayerToInteract interactPlayer { get; set; }
    [field: SerializeField]
    public KeyCode interactButton {  get; set; }
    [field: SerializeField]
    public float interactTime { get; set; }
    [field: SerializeField]
    public bool isSingleUse { get; set; }
    [field: SerializeField]
    public float interactRange { get; set; }
    [Tooltip("Set to -1 to trigger nothing; otherwise will trigger flags in level")]
    [field: SerializeField]
    public string[] triggerFlag { get; set; }

    private object[] internalFlags = new object[16];

    [Header("Settings")]

    [ConditionalField("interactableType", false, InteractableType.PressurePlate)]
    public float PressurePlateActuationMass = 0;


    [ConditionalField("interactableType", false, InteractableType.Freebody)]
    public float FreebodyMass = 0;


    [ConditionalField("interactableType", false, InteractableType.Pickup)]
    public GameObject PickupObject;



    [field: SerializeField]
    public bool isCollidable
    {   
        set
        {
            try
            {
                GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
                if (value)
                {
                    GetComponent<BoxCollider2D>().size = originalColliderSize;

                }
            }
            catch
            {
                GetComponent<TilemapCollider2D>().isTrigger = true;
                if (value)
                {
                    GetComponent<TilemapCollider2D>().isTrigger = false;
                }
            }

            
        }
    }

    public void Interact(PlayerControllerRB2D player)
    {
        isInteracting = true;
        if (!isInteractable)
        {
            print("Object is not interactable");
            return;
        }
        if (interactTime != 0f && currentInteractPercentage <= 1f)
        {
            currentInteractPercentage += Time.deltaTime / interactTime;
        }
        else 
        {
            Invoke(player);
            return;
        }

        

    }
    // Invoke action once is interacted with
    private void Invoke(PlayerControllerRB2D player)
    {
        if (isSingleUse)
        {
            isInteractable = false;
            RadialProgress.transform.localScale = Vector3.zero;
            Destroy(GetComponentInChildren<Light2D>());
        }
        FindObjectOfType<LevelScript>().triggerFlag(triggerFlag);
        switch (interactableType) { 
            case InteractableType.None:
                break;
            case InteractableType.Lever:
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
                break;

        }
    }

    public void PublicInvoke(bool onlyOnce = false)
    {
        if (internalFlags[15] == null)
        {
            internalFlags[15] = false;
        }
        if (!(bool)internalFlags[15])
        {
            switch (interactableType)
            {
                case InteractableType.Door:
                    if (internalFlags[0] == null)
                    {
                        internalFlags[0] = false;
                    }
                    internalFlags[0] = !(bool)internalFlags[0];
                    internalFlags[1] = transform.position.y;
                    print(internalFlags[1]);
                    break;
            }
        }
        if (onlyOnce)
        {
            internalFlags[15] = true;
        }

    }

    void FixedUpdate()
    {
        currentInteractPercentage = Mathf.Clamp(currentInteractPercentage, 0f, 1f);
        if (!isInteracting && isInteractable && interactTime != 0)
        {
            currentInteractPercentage -= Time.fixedDeltaTime / interactTime * 2f; // Decays twice as quick
        }   
    }

    void Start()
    {
        players = FindObjectOfType<Game>().players;
        try {
            originalColliderSize = GetComponent<BoxCollider2D>().size;
        }
        catch { }
        isCollidable = true; 
        if (isInteractable)
        {
            RadialProgress = Instantiate(FindObjectOfType<UI_Manager>().getElementGameObjectByName("RadialProgress"), FindObjectOfType<UI_Manager>().transform);
            RadialProgress.transform.position = this.transform.position;
        }
        onStart();
    }

    
    public virtual void onStart() { }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable)
        {
            RadialProgress.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(this.transform.position);
            RadialProgress.layer = 1;
            if (interactPlayer == PlayerToInteract.Player1) {
                RadialProgress.transform.Find("Key").GetComponent<TMP_Text>().text = "DOWN";
                float dist = Vector3.Distance(transform.position, players[0].transform.position);
                RadialProgress.transform.localScale = Vector3.zero;
                if (dist <= interactRange)
                {
                    RadialProgress.transform.localScale = new Vector3(Mathf.Min((interactRange - dist) / interactRange, 1f) + 0.3f, Mathf.Min((interactRange - dist) / interactRange, 1f) + 0.3f, 1);
                    isInteracting = false;
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        Interact(players[0]);
                    }
                }
            }
            else
            {
                RadialProgress.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(this.transform.position);
                RadialProgress.transform.Find("Key").GetComponent<TMP_Text>().text = "S";
                float dist = Vector3.Distance(transform.position, players[1].transform.position);
                RadialProgress.transform.localScale = Vector3.zero;
                if (dist <= interactRange)
                {
                    RadialProgress.transform.localScale = new Vector3(Mathf.Min((interactRange - dist) / interactRange, 1f) + 0.3f, Mathf.Min((interactRange - dist) / interactRange, 1f) + 0.3f, 1);
                    isInteracting = false;
                    if (Input.GetKey(KeyCode.S))
                    {
                        Interact(players[1]);
                    }
                }
            }

            RadialProgress.transform.Find("Center").transform.Find("Fill").GetComponent<InverseMask>().fillAmount = currentInteractPercentage;
        }

        if (interactableType == InteractableType.Door && internalFlags[0] != null)
        {
            if ((bool)internalFlags[0])
            {
                if (transform.position.y < (float)internalFlags[1] + 1.4f)
                {
                    transform.position += new Vector3(0f, Time.deltaTime, 0f);
                }
            }
            if (!(bool)internalFlags[0]) {
                if (transform.position.y > (float)internalFlags[1])
                {
                    transform.position -= new Vector3(0f, Time.deltaTime, 0f);
                } 
            }
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, (float)internalFlags[1], (float)internalFlags[1] + 1.4f), transform.position.z);
        }
        isCollidable = isCollidableEditor;
    }
}
