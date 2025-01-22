using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DronePathNode : MonoBehaviour
{
    public string Text;
    public float TextAnimationSpeed = 10;
    public bool hasTriggered = false;
    [field: SerializeField]
    public bool continueOnCondition = false;
    [ConditionalField("continueOnCondition", false)]
    public string flagTriggeredBy;
    public bool continueOnPass = false;
    [ConditionalField("continueOnPass", false)]
    
    public PlayerControllerRB2D[] players;
    
    void Start() {
        players = FindObjectOfType<Game>().players;
    }
    void Update(){
        if(continueOnPass){
            if (players[0].transform.position.x > transform.position.x && players[1].transform.position.x > transform.position.x){
                FindObjectOfType<LevelScript>().triggerFlag(flagTriggeredBy);
                continueOnPass = false;
            }
        
        }
    }
    public void AnimateText()
    {
        if (hasTriggered) return;
        hasTriggered = true;
        GameObject.Find("Drone").GetComponentInChildren<TMP_Text>().text = "";
        StartCoroutine(animateText());
    }

    private IEnumerator animateText()
    {
        for (int i = 0; i < Text.Length; i++)
        {
           
            GameObject.Find("Drone").GetComponentInChildren<TMP_Text>().text += Text[i];
            yield return new WaitForSeconds(1f / TextAnimationSpeed);
        }
    }
}
