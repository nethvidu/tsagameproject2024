using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Levels.Level Level = Levels.Level.id01; //Placeholder
    private LevelManager lvlMgr;
    public PlayerControllerRB2D[] players;
    public UI_Manager UIManager;

    void Start()
    {
        UIManager = FindObjectOfType<UI_Manager>();
        lvlMgr = FindObjectOfType<LevelManager>(); 
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(Level));
        this.startLevel(lvlMgr);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private IEnumerator updateFPS()
    {
        while (true)
        {
            UIManager.getElementByName<TMP_Text>("FPS Counter").text = (1f / Time.deltaTime).ToString("0.0") + " FPS";
            yield return new WaitForSeconds(1);
        }
    }
     
    void startLevel(LevelManager mgr)
    {
        StartCoroutine(transform.parent.GetComponentInChildren<LevelScript>().TickLevel());
        StartCoroutine(updateFPS());
    }
}
 