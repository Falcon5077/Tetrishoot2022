using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AboveAll : MonoBehaviour
{

    public TMP_Text scoreText; //스코어 텍스트용 변수
    public int totScore = 0; //누적 점수 기록용 변수
    

    public float UpTime = 60.0f; //바닥이 올라오는 시간
    public GameObject[] bottom;
    public GameObject[] block;
    public GameObject[] Lazer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveUp());
        
    }

       public void DisplayScore(int score){
        totScore += score;
        scoreText.text = $"<color=#00ff00>SCORE: </color> <color=#ff0000>{totScore:#,##0}</color>";
        
       
    }

    IEnumerator moveUp(){
        yield return new WaitForSeconds(UpTime);  //시간이 되면 바닥 올리기
        
        
        
        bottom = GameObject.FindGameObjectsWithTag("bottom");  //벽돌
        block = GameObject.FindGameObjectsWithTag("Plane");   //바닥과 레이어의 오브젝트들을 배열로 가져옵니다
        //Lazer = GameObject.FindGameObjectsWithTag("lazer");  
        
        
        if(bottom != null){
        for(int i=0; i < bottom.Length; i++){  //Drop컴포넌트를 지니는 벽돌들은 따로 가져옵니다.
            Vector3 UpV = new Vector3(0, 1.0f, 0);
            bottom[i].GetComponent<Drop>().fixedPos += UpV; //벽돌은 fixedPos로 위치를 가지기 때문에 fixedPos의 y값을 올려줍니다.
        }
        }
        /*
         for(int i=0; i < Lazer.Length; i++){
            Lazer[i].transform.Translate(new Vector3(0,1.0f,0));
        }*/
          for(int i=0; i < block.Length; i++){  //레이저와 바닥은 별다른 컴포넌트가 없기 때문에 그냥 한 칸씩 올려줍니다.
            block[i].transform.Translate(new Vector3(0,1.0f,0));
        }
        StartCoroutine(moveUp());
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore(0);//스코어 표기
    }
}
