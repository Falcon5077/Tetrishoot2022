// * ---------------------------------------------------------- //
// * 게임 모드 선택을 위한 스크립트입니다.
// * 오브젝트 : MainButtons
// * ---------------------------------------------------------- //

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    public GameObject Mode;
    public Button Normal;
    public Button Extreme;
    
    // * ---------------------------------------------------------- //
    // * 선택한 모드를 PlayerPrefs에 저장합니다.
    public void SetMode(int value)
    {
        // * 1 노말 2 익스트림
        PlayerPrefs.SetInt("Mode",value); 
    }
    // * ---------------------------------------------------------- //
    // * Normal, Extreme 버튼의 Pop 애니메이션을 실행합니다.
    public void PopPosition()
    {
        Hashtable hash = new Hashtable();
        hash.Add("position", new Vector3(0, 1000, 0f));
        hash.Add("time", 0.85f);
        hash.Add("easetype",iTween.EaseType.easeInOutBack);
        hash.Add("islocal", true);
        
        iTween.MoveTo(Normal.gameObject,hash);

        hash["position"] = new Vector3(0,600,0);
        iTween.MoveTo(Extreme.gameObject,hash);
    }

    // * ---------------------------------------------------------- //
    // * Normal, Extreme 버튼이 다시 원래 자리로 돌아갑니다.
    public void RePosition()
    {
        Hashtable hash = new Hashtable();
        hash.Add("position", new Vector3(0, 200, 0f));
        hash.Add("time", 0.2f);
        hash.Add("easetype",iTween.EaseType.linear);
        hash.Add("islocal", true);

        iTween.MoveTo(Normal.gameObject,hash);
        iTween.MoveTo(Extreme.gameObject,hash);

        Invoke("SetOff",0.3f);

    }

    // * ---------------------------------------------------------- //
    // * Start 버튼을 누르면 Mode Button을 On/Off 합니다.
    public void Turn()
    {
        if(Mode.activeInHierarchy == true){
            RePosition();
        }
        else if(Mode.activeInHierarchy == false){
            PopPosition();
            Mode.SetActive(true);
        }
    }
    public void SetOff()
    {
        Mode.SetActive(false);
    }
}
