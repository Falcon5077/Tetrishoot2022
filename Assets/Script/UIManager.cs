using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public Button pauseBut;
    public Button quitBut;
    public Button resBut;
    public GameObject pause;
    private bool isPause;
    int ClickCount;


    public void pauseGame(){
            Time.timeScale = 0;
            pause.SetActive(true);
            isPause = true;
            Debug.Log("pause");
    }
    public void Resume(){  
        pause.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            Debug.Log("resume");
    }

    public void QuitApp(){
        Application.Quit();
        Debug.Log("quit");
    }

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pause.SetActive(false);
        pauseBut.onClick.AddListener(delegate{this.pauseGame();});
        resBut.onClick.AddListener(delegate{Resume();});
        quitBut.onClick.AddListener(delegate{QuitApp();});
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPause == false){
                Time.timeScale = 0;
            pause.SetActive(true);
            isPause = true;
            }
            else if(isPause == true){
                {
            pause.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            }

            }
            ClickCount++;
            if(!IsInvoking("DoubleClick")){
                Invoke("DoubleClick", 1.0f);

            }
         
        }
        else if (ClickCount == 2){
                CancelInvoke("DoubleClick");
                Application.Quit();
                Debug.Log("quit");
        }
    }

    void DoubleClick(){
        ClickCount = 0;
    }
}
