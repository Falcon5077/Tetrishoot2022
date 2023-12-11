// * ---------------------------------------------------------- //
// * Canvas에서 UI를 관리하는 스크립트입니다.
// * 오브젝트 : Canvas
// * ---------------------------------------------------------- //

using UnityEngine;
using UnityEngine.UI;
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
    
    // * ---------------------------------------------------------- //
    // * 게임 재시작
    public void reStartGame(){          
        
        // * 시작 신을 다시 가지고 옵니다
        SceneManager.LoadScene("GameScene");  
        pause.SetActive(false);

        Time.timeScale = 1;

        isPause = false;
    }
    // * ---------------------------------------------------------- //
    // * 게임 정지
    public void pauseGame(){   
        // * 인게임 시간을 정지시킵니다      
        Time.timeScale = 0;    

        // * Pause pannel On
        pause.SetActive(true);  

        isPause = true;

        touchCount++;

        if(touchCount >= 10)
        {
            DevOption.SetActive(true);
        }
    }

    // * ---------------------------------------------------------- //
    // * 게임 재개
    public void Resume(){
        // * 패널 안 보이게
        pause.SetActive(false);  

        Time.timeScale = 1;

        isPause = false;

        if(touchCount < 8)
            touchCount = 0;
    }

    // * 메인 화면으로 돌아갑니다.
    public void Lobby()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }

    // * 게임을 종료합니다.
    public void QuitApp(){ 
        Application.Quit();
    }

    void Start()
    {
        // * 정지 해제
        isPause = false;        

        // * 일시정지 시 나오는 패널을 안 보이게 합니다.
        pause.SetActive(false);    
    }

    void Update()
    {
        // * 뒤로가기 또는 ESC 누를 시 일시정지
        if(Input.GetKeyDown(KeyCode.Escape)){  
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
            
            if(!IsInvoking("ClickCancel")){  
                Invoke("ClickCancel", 1.0f);
            }
        }
        // * 더블클릭 시 나가기
        else if (ClickCount == 2){  
            CancelInvoke("ClickCancel");
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    void ClickCancel(){  
        ClickCount = 0;
    }
}
