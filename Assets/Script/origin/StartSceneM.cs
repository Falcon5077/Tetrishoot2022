using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneM : MonoBehaviour
{
    public TextMeshProUGUI quitText;
    public static bool isQuit;
    public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void QuitApp(){
        if(isQuit == false)
        {
            if(quitText != null)
            {
                quitText.text = "Really Quit";
                quitText.fontSize = 90;
            }
            isQuit = true;
            return;
        }        
    
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

    public void SetDefault()
    {
        isQuit = false;
        if(quitText != null)
        {
            quitText.text = "Quit";
            quitText.fontSize = 150;
        }
    }
}
