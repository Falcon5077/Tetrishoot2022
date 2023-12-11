using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI mText;
    public static CountDown instance;

    private void Awake() {
        instance = this;
        ShootBullet.canShoot = false;
        SetMode();
        StartCoroutine("GameStart");
    }

    public void SetMode()
    {
        int a = PlayerPrefs.GetInt("Mode",1); // 1 노말 2 익스트림
        if(a == 1)
        {
            Spawner.instance.isHard = true;
        }   
        else if(a == 2)
        {
            Spawner.instance.isHard = false;
        }
    }

    IEnumerator CountStart()
    {
        System.Collections.Hashtable hash =
                    new System.Collections.Hashtable();
        hash.Add("amount", new Vector3(0.5f, 0.5f, 0f));
        hash.Add("time", 0.85f);

        yield return new WaitForSeconds(1f);
        for(int i = 3; i > 0; i--)
        {
            iTween.PunchScale(gameObject, hash);
            
            mText.text = i.ToString();
            AudioManager.instance.CountSound();
            yield return new WaitForSeconds(1f);
        }

        iTween.PunchScale(gameObject, hash);
        mText.fontSize = 230;
        mText.text = "GameStart!";

        AudioManager.instance.ClearSound();
        yield return new WaitForSeconds(1f);

        ShootBullet.canShoot = true;

        Spawner.instance.GameStart();
        Above.instance.AboveStart();
        Score.instance.ScoreUp(0);
        mText.text = "";

        
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.5f);
        Score.instance.ScoreUp(0);
        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
        yield return new WaitForSeconds(0.35f);
        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
        
        Spawner.instance.SetBase();
        yield return new WaitForSeconds(0.85f);

        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
        yield return new WaitForSeconds(0.35f);
        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
        yield return new WaitForSeconds(0.35f);

        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("CountStart");
    }

    // private int Timer = 0;
    // [SerializeField]
    // private int Count = 30;

    // public GameObject Num_A;   //1번
    // public GameObject Num_B;   //2번
    // public GameObject Num_C;   //3번
    // public GameObject Num_GO;
    // void Start ()
    // {
    //   //시작시 카운트 다운 초기화
    //   Timer = 0;

    //   Num_A.SetActive(false);
    //   Num_B.SetActive(false);
    //   Num_C.SetActive(false);
    //   Num_GO.SetActive(false);

    // }



    // void Update ()
    // {

    // //게임 시작시 정지
    // if(Timer == 0){
    // Time.timeScale = 0.0f;  
    // }


    // //Timer 가 90보다 작거나 같을경우 Timer 계속증가

    // if(Timer <= Count*3){
    // Timer++;

    //       // Timer가 30보다 작을경우 3번켜기
    //     if(Timer < Count){
    //       Num_C.SetActive(true);
    //   }

    //     // Timer가 30보다 클경우 3번끄고 2번켜기
    //     if(Timer > Count){
    //       Num_C.SetActive(false);
    //       Num_B.SetActive(true);
    //   }

    //     // Timer가 60보다 작을경우 2번끄고 1번켜기
    //     if(Timer > Count*2){
    //       Num_B.SetActive(false);
    //       Num_A.SetActive(true);
    //   }

    //     //Timer 가 90보다 크거나 같을경우 1번끄고 GO 켜기 LoadingEnd () 코루틴호출
    //     if(Timer >= Count*3){
    //       Num_A.SetActive(false);
    //       Num_GO.SetActive(true);
    //       StartCoroutine(this.LoadingEnd());
    //       Time.timeScale = 1.0f; //게임시작
    //     }
    //   }

    // }



    // IEnumerator LoadingEnd(){
    //   yield return new WaitForSeconds(1.0f);
    // Num_GO.SetActive(false);
    // }
}