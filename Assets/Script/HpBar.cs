using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBar : MonoBehaviour
{

    public TMP_Text HpB; // HP바에 시간 표시
    private readonly float initHp = 60.0f;  //초기 바
    public float curHp;  // 현재 바
    private Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        curHp = initHp;
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>(); //이미지 컴포넌트 가져오기
        StartCoroutine(makeHp());
        

    }

    IEnumerator makeHp(){
        
        DisplayHealth();
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(makeHp());
    }



    

    // Update is called once per frame
    void Update()
    {
        curHp -= Time.deltaTime;
        
    }

    void DisplayHealth(){
        hpBar.fillAmount = curHp/initHp;
        HpB.text = $" <color=#ff0000>{curHp:#,##0}</color>";
        //scoreText.text = $"<color=#00ff00>SCORE: </color> <color=#ff0000>{totScore:#,##0}</color>";
    }
}
