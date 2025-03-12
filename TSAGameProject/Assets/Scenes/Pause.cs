using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Vector2 endLocation;
    public bool pause = false;
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        endLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if(pause){
            canvasGroup.alpha = 1;
            Time.timeScale = 0;
        } else {
            canvasGroup.alpha = 0;
            Time.timeScale = 1;
        }
        
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
        pause = false;
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #else
        Application.Quit(); 
        #endif
    }


}
