using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI mText;
    public static Score instance;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            ScoreUp(-100);
        if(Input.GetKeyDown(KeyCode.X))
            ScoreUp(100000);

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
