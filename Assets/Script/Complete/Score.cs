// * ---------------------------------------------------------- //
// * 점수를 관리하는 스크립트입니다.
// * 오브젝트 : GameManager
// * ---------------------------------------------------------- //

using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI mText;
    public static Score instance;
    
    void Start()
    {
        score = 0;
        if(instance == null) instance = this;
    }

    public void ScoreUp(int a)
    {
        score += a;

        string s = "Score : " + score.ToString("#,##0");
        
        if(score < 0)
        {
            score = 0;
            s = "0";
            return;
        }

        mText.text = s;
        System.Collections.Hashtable hash =
                    new System.Collections.Hashtable();
                    
        hash.Add("amount", new Vector3(0.5f, 0.5f, 0f));
        hash.Add("time", 0.5f);
        iTween.PunchScale(mText.gameObject, hash);
    }
}
