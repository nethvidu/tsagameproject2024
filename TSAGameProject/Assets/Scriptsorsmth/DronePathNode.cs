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

    public void AnimateText()
    {
        if (hasTriggered) return;
        hasTriggered = true;
        GameObject.Find("DroneText").GetComponentInChildren<TMP_Text>().text = "";
       StartCoroutine(animateText());
    }

    private IEnumerator animateText()
    {
        for (int i = 0; i < Text.Length; i++)
        {
           
            GameObject.Find("DroneText").GetComponentInChildren<TMP_Text>().text += Text[i];
            yield return new WaitForSeconds(1f / TextAnimationSpeed);
        }
    }
}
