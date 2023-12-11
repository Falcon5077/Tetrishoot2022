// * ---------------------------------------------------------- //
// * 메인 씬에서 게임 씬으로 넘어가거나 게임을 종료할 때 사용하는 스크립트 입니다.
// * 오브젝트 : GameManager
// * ---------------------------------------------------------- //

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneM : MonoBehaviour
{
    public TextMeshProUGUI quitText;
    public static bool isQuit;

    // * ---------------------------------------------------------- //
    // * GameScene을 불러옵니다.
    public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }


    // * ---------------------------------------------------------- //
    // * Really Quit 버튼을 눌렀을 때 호출합니다.
    // * 게임을 종료하는 함수입니다.
    public void QuitApp(){
        // * 이 함수가 호출 되었을 때 isQuit가 false라면 Text를 ReallyQuit으로 바꾸고 isQuit을 True로 바꿉니다.
        if(isQuit == false)
        {
            if(quitText != null)
            {
                quitText.fontSize = 90;
                quitText.text = "Really Quit";
            }

            isQuit = true;
            return;
        }        
    
        // * isQuit가 true라면 게임을 종료합니다.
        Application.Quit();
        Debug.Log("quit");
    }

    // * ---------------------------------------------------------- //
    // * ReallyQuit 버튼말고 다른 곳을 눌렀을 때 호출합니다.
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
