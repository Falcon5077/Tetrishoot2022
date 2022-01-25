using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartSceneM : MonoBehaviour
{

     public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }

      public void QuitApp(){
        Application.Quit();
        Debug.Log("quit");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
