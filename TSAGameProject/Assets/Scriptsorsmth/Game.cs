using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Levels.Level Level = Levels.Level.id03; //Placeholder
    private LevelManager lvlMgr;
    public PlayerControllerRB2D[] players;
    public UI_Manager UIManager;

    void Start()
    {
        UIManager = FindObjectOfType<UI_Manager>();
        lvlMgr = FindObjectOfType<LevelManager>(); 

    }

    public async void StartCour(Map map, GameObject[] loadAfter = null)
    {
        print("Trying to load level " + Level.ToString() + "...");
        lvlMgr.loadMap(map);

        print("Loading successful");
        if (loadAfter != null)
        {
            loadAfter.ForEach(a => a.SetActive(true));
        }
        this.startLevel(lvlMgr);
    }
    // Update is called once per frame
    void Update()
    {
        

    }

    public Map GetMap()
    {
        return lvlMgr.getReferenceToLevel(Level);
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
        FindObjectsByType<PlayerControllerRB2D>(default).ForEach(player => player.ResetPos());
        transform.parent.GetComponentInChildren<LevelScript>().LevelStart();
        FindObjectOfType<DroneMove>().GetComponent<SplineAnimate>().Container = GameObject.Find("DronePath(Clone)").GetComponent<SplineContainer>();
        FindObjectOfType<DroneMove>().GetComponent<SplineAnimate>().Play();
        StartCoroutine(transform.parent.GetComponentInChildren<LevelScript>().TickLevel());
        StartCoroutine(updateFPS());
    }

    void ResetLevel()
    {
        loadNewLevel(Level);
    }
    public void loadNewLevel(Levels.Level levelToLoad)
    {
        lvlMgr.loadMap(lvlMgr.getReferenceToLevel(levelToLoad));
        this.startLevel(lvlMgr);
    }

    void levelClear()
    {

    }
}
 