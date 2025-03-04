using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Levels.Level level = Levels.Level.id01; //Placeholder
    private LevelManager lvlMgr;
    public PlayerControllerRB2D[] players;
    public UI_Manager UIManager;
    public LevelEnd levelend;
    public bool levelLoaded;
    public DroneMove drone;
    public Animator transition;


    void Start()
    {
        transition = GameObject.Find("UI").GetComponent<Animator>();
        drone = FindFirstObjectByType<DroneMove>();
        UIManager = FindObjectOfType<UI_Manager>();
        lvlMgr = FindObjectOfType<LevelManager>(); 
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(level));
        this.startLevel();
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
     
    void startLevel()
    {
        transform.parent.GetComponentInChildren<LevelScript>().LevelStart();
        StartCoroutine(transform.parent.GetComponentInChildren<LevelScript>().TickLevel());
        StartCoroutine(updateFPS());
        levelLoaded = true;
        players[0].SpawnPlayer();
        players[1].SpawnPlayer();
    }

    public void loadNewLevel(Levels.Level levelToLoad)
    {
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(levelToLoad));
        this.startLevel();
        levelLoaded = false;

    }

    public void levelClear()
    {
        levelLoaded = false;
        Destroy(this.transform.parent.Find("Level").Find("CameraBounds(Clone)").gameObject);
    }
    public IEnumerator LevelTransition(Levels.Level loadLevel)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        transition.Play("Crossfade_Start", -1, 0.0f);
        yield return new WaitForSecondsRealtime((1f / Time.deltaTime)/70f);
        drone.locList.Clear();
        levelClear();
        Debug.Log("Ended Coroutine at timestamp : " + Time.time);
        loadNewLevel(loadLevel);

    }
}
 