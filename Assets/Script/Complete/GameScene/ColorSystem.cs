// * ---------------------------------------------------------- //
// * 오브젝트의 Color를 관리하는 스크립트입니다.
// * 오브젝트 : GameManager
// * ---------------------------------------------------------- //

using UnityEngine;

public class ColorSystem : MonoBehaviour
{
    public static ColorSystem instance;
    void Start()
    {
        if(instance == null) instance = this;
    }

    public void SetColor(string first, ref Color mColor) {
        if(first == "0")
        {
            mColor = new Color32(105,80,40,255);
        }
        else if(first == "1")
        {
            mColor = new Color32(255,0,51,255);
        }
        else if(first == "2")
        {
            mColor = new Color32(255,153,0,255);
        }
        else if(first == "3")
        {
            mColor = new Color32(255,255,0,255);
        }
        else if(first == "4")
        {
            mColor = new Color32(0,255,0,255);
        }
        else if(first == "5")
        {
            mColor = new Color32(0,255,255,255);
        }
        else if(first == "6")
        {
            mColor = new Color32(0,51,204,255);
        }
        else if(first == "7")
        {
            mColor = new Color32(153,51,255,255);
        }
        
    }
}
