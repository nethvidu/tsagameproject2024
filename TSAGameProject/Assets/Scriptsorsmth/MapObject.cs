using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour // ADD THIS COMPONENT TO EACH OBJECT WITHIN THE MAP HIERARCHY
{
    public enum PlayerToInteract
    {
        Player1,
        Player2
    }

    public enum InteractableType {
        Door,
        Button,
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
    public InteractableType interactableType { get; set; }
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
    [field: SerializeField]
    public bool isCollidable
    {   
        set
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
            if (value)
            {
                GetComponent<BoxCollider2D>().size = originalColliderSize;
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
        switch (interactableType) { 
            case InteractableType.None:
                FindObjectOfType<LevelScript>().triggerFlag(triggerFlag);
                break;

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
        originalColliderSize = GetComponent<BoxCollider2D>().size;
        isCollidable = true; 
        if (isInteractable)
        {
            RadialProgress = Instantiate(FindObjectOfType<UI_Manager>().getElementGameObjectByName("RadialProgress"), FindObjectOfType<UI_Manager>().transform);
            RadialProgress.transform.position = this.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable)
        {
            RadialProgress.transform.position = this.transform.position;
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

        isCollidable = isCollidableEditor;
    }
}
