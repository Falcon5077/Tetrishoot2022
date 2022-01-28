using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button pauseBut;
    public Button quitBut;
    public Button resBut;
    public GameObject pause;
    public int touchCount = 0;
    private bool isPause;
    int ClickCount;
    public GameObject DevOption;
    public void reStartGame(){          //게임 재시작
        SceneManager.LoadScene("GameScene");  //시작 신을 다시 가지고 옵니다
        pause.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        Debug.Log("resume");
    }
    public void pauseGame(){         //일시정지
        Time.timeScale = 0;    //인게임 시간을 정지시킵니다
        pause.SetActive(true);  //패널 생성
        isPause = true;
        Debug.Log("pause");
        touchCount++;
        if(touchCount >= 10)
        {
            DevOption.SetActive(true);
        }
    }
    public void Resume(){     //일시정지 해제
        pause.SetActive(false);  // 패널 안 보이게
        Time.timeScale = 1;
        isPause = false;
        Debug.Log("resume");
        if(touchCount < 8)
            touchCount = 0;
    }

    public void Lobby()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }

    public void QuitApp(){  //나가기
        Application.Quit();  //나가기
        Debug.Log("quit");
    }

    // // Start is called before the first frame update
    void Start()
    {
        isPause = false;        //일시정지되었는지 확인합니다
        pause.SetActive(false);    //일시정지 시 나오는 패널을 안 보이게 합니다.
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){  //뒤로가기/ESC 누를 시 일시정지
            if(isPause == false){
                Time.timeScale = 0;
                pause.SetActive(true);
                isPause = true;
            }
            else if(isPause == true){
                pause.SetActive(false);
                Time.timeScale = 1;
                isPause = false;
            }

            ClickCount++;
            
            if(!IsInvoking("DoubleClick")){  
                Invoke("DoubleClick", 1.0f);
            }
        }
        else if (ClickCount == 2){           //더블클릭 시 나가기
            CancelInvoke("DoubleClick");
            Application.Quit();
            Debug.Log("quit");
        }
    }

    void DoubleClick(){  
        ClickCount = 0;
    }
}
