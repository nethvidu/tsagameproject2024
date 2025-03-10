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
    public bool bruh;
    public Animator transition;
    public UnityEngine.Video.VideoPlayer vid;
    public TextMeshProUGUI text;


    void Start()
    {
        transition = GameObject.Find("UI").GetComponent<Animator>();
        vid = GameObject.Find("UI").GetComponent<UnityEngine.Video.VideoPlayer>();

        drone = FindFirstObjectByType<DroneMove>();
        UIManager = FindObjectOfType<UI_Manager>();
        lvlMgr = FindObjectOfType<LevelManager>(); 
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(level));
        text = transition.transform.GetComponentsInChildren<TextMeshProUGUI>()[2];
        this.startLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(bruh){
            players[0].SpawnPlayer();
            players[1].SpawnPlayer();
            bruh = false;
        }
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
        bruh = true;
        drone.text.text = "";
        levelLoaded = true;
        
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
        lvlMgr.destroyMap();
    }
    public IEnumerator LevelTransition(Levels.Level loadLevel)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        transition.Play("Crossfade_Start", -1, 0.0f);
        vid.frame = 0;
        text.text = "Level " + loadLevel.ToString();
        yield return new WaitForSeconds(0.10f);
        vid.Play();
        drone.locList.Clear();
        yield return new WaitForSeconds(5.76f);
        levelClear();
        loadNewLevel(loadLevel);
        
    }
}
 