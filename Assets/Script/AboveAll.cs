using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class AboveAll : MonoBehaviour
{

    public TMP_Text scoreText; //스코어 텍스트용 변수
   
    public int totScore = 0; //누적 점수 기록용 변수
    

    public float UpTime = 60.0f; //바닥이 올라오는 시간
    public GameObject[] bottom;
    public GameObject[] block;
    public GameObject[] Bot;


    private int savedScore = 0;
    public TMP_Text highScore;
    public TMP_Text EndText;
    public GameObject EndScene; //최종씬
    private bool isEnd = false; //최종씬이 나왔는지?

    private string KeyString = "HighScore";

     void Awake() {
        savedScore = PlayerPrefs.GetInt(KeyString, 0);   //세이브
        highScore.text = "High Score: " + savedScore.ToString("0"); // 하이스코어
    }

    public void GameEnd(){
        Time.timeScale = 0;
        EndScene.SetActive(true);
        isEnd = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(check());
        StartCoroutine(moveUp());
        EndScene.SetActive(false);
    }

       public void DisplayScore(int score){
        totScore += score;
        scoreText.text = $"<color=#00ff00>SCORE: </color> <color=#ff0000>{totScore:#,##0}</color>";
        
       
    }

    IEnumerator moveUp(){
        yield return new WaitForSeconds(UpTime);  //시간이 되면 바닥 올리기
        
        
        
       
        Debug.Log(bottom.Length);
        
        
        if(bottom != null){
        for(int i=0; i < bottom.Length; i++){  //Drop컴포넌트를 지니는 벽돌들은 따로 가져옵니다.
       for(int j = 0; j < bottom[i].transform.childCount; j++)
        {
            bottom[i].transform.GetChild(j).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }

            Vector3 UpV = new Vector3(0, 1.0f, 0);
            bottom[i].GetComponent<Drop>().fixedPos += UpV; //벽돌은 fixedPos로 위치를 가지기 때문에 fixedPos의 y값을 올려줍니다.
        yield return new WaitForSeconds(1.0f);
            for(int j = 0; j < bottom[i].transform.childCount; j++)
        {
            bottom[i].transform.GetChild(j).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        }
        }

        
        if(Bot != null){
        for(int i=0; i < Bot.Length; i++){  //Drop2컴포넌트를 지니는 벽돌들은 따로 가져옵니다.
            Vector3 UpV = new Vector3(0, 1.0f, 0);
            Bot[i].GetComponent<Drop_2>().fixedPos += UpV; //벽돌은 fixedPos로 위치를 가지기 때문에 fixedPos의 y값을 올려줍니다.
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

    IEnumerator check(){
        while(true) {
         bottom = GameObject.FindGameObjectsWithTag("bottom");  //벽돌
        block = GameObject.FindGameObjectsWithTag("Plane");   //바닥과 레이어의 오브젝트들을 배열로 가져옵니다
        Bot = GameObject.FindGameObjectsWithTag("Bot");  //Drop2를 가지는 벽돌들
        Debug.Log("일반벽돌 : " + bottom.Length + "특수벽돌 : " + Bot.Length);
        yield return new WaitForSeconds(0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore(0);//스코어 표기
        if(totScore > savedScore){       //최고점수 갱신
            PlayerPrefs.SetInt(KeyString, totScore);
            }
        EndText.text = scoreText.text;

        if(isEnd == true){
            if(Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene("GameScene");  //신을 다시 가지고 옵니다
            EndScene.SetActive(false);
            Time.timeScale = 1;
            }
        }
        
    }
}
