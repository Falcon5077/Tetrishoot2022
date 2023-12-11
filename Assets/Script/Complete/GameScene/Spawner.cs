// * ---------------------------------------------------------- //
// * 블럭을 스폰하는 스크립트입니다.
// * 오브젝트 : GameManager
// * ---------------------------------------------------------- //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeType{
    public const int BombSpawn = 0;
    public const int BombSize = 1;
    public const int FloorSpawn = 2;
    public const int GravitySize = 3;
    public const int SpawnSize = 4;
    public const int DrillSpawn = 5;
    public const int QuakeSpawn = 6;
    public const int LimitHP = 7;
}
public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    // * HP 관련 필드
    public int defaultHP;
    public int MaxHP;

    // * 게임 플레이 관련 필드
    public float mGravity;
    public bool isHard = false;
    public TextMeshProUGUI mText;

    // * 블럭 관련 필드
    public int nextBlock;
    public GameObject[] Block;
    public float SpawnDelay = 2f;
    public float[] specialBlockTime = new float[5];
    public List<GameObject> Check = new List<GameObject>();
    
    
    // Start is called before the first frame update
    public void SetMode()
    {
        isHard = !isHard;
    }
    private void Awake() {
        if(instance == null) instance = this;

        defaultHP = 3;
        Time.timeScale = 1;

        // * 1 노말 2 익스트림
        int Mode = PlayerPrefs.GetInt("Mode",1); 

        // * 게임의 난이도를 설정합니다.
        if(Mode == 1)
        {
            specialBlockTime[TimeType.BombSpawn] = 25;
            specialBlockTime[TimeType.BombSize] = 3;
            specialBlockTime[TimeType.FloorSpawn] = 30;
            specialBlockTime[TimeType.GravitySize] = 3.5f;
            specialBlockTime[TimeType.SpawnSize] = 3;
            specialBlockTime[TimeType.DrillSpawn] = 25;
            specialBlockTime[TimeType.QuakeSpawn] = 25;
            specialBlockTime[TimeType.LimitHP] = 5;
            Spawner.instance.isHard = true;
        }   
        else if(Mode == 2)
        {
            specialBlockTime[TimeType.BombSpawn] = 25;
            specialBlockTime[TimeType.BombSize] = 3;
            specialBlockTime[TimeType.FloorSpawn] = 20;
            specialBlockTime[TimeType.GravitySize] = 5f;
            specialBlockTime[TimeType.SpawnSize] = 2;
            specialBlockTime[TimeType.DrillSpawn] = 25;
            specialBlockTime[TimeType.QuakeSpawn] = 25;
            specialBlockTime[TimeType.LimitHP] = 3;
            
            Spawner.instance.isHard = false;
        }

        SpawnDelay = specialBlockTime[TimeType.SpawnSize];
        mGravity = specialBlockTime[TimeType.GravitySize];
    }
    public void GameStart()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Check.Add(transform.GetChild(i).gameObject);
            Check[i].transform.localPosition = new Vector3(-7.5f,0.98f * (i+1), 0);
            Check[i].GetComponent<BlockCheck>().delay = i/10;
        }
        StartCoroutine("SpawnBlock");
        StartCoroutine("Timer");
    }

    // * 스코어는 바닥 부실때 플러스
    // * 최대체력 점점 늘어나고 2~5까지 랜덤, 5 나올 확률 점점 증가
    // * 라인 클리어시 바닥 내려감
    IEnumerator Timer() 
    {
        // * 최대체력 점점 늘어나고, 2~5까지 랜덤, 5나올 확률 증가, 바닥올라오는거, 프로젝트 합치기, 스코어는 바닥 부실때, 난이도 조절
        defaultHP = 3;
        MaxHP = 3;
        
        for(int i = 0; i < 180; i++)
        {
            mText.text = i.ToString();

            if((i+1) % 25 == 0)
            {
                MaxHP += 1;
                SpawnDelay -= 0.2f;
                Debug.Log((i+1) + "초 마다 감소 중" + SpawnDelay);
            }

            if((i+1) % 25 == 0)
            {
                mGravity += 0.2f;
                Debug.Log((i+1) + "초 마다 가속 중" + mGravity);
            }

            if(i == 120)
            {
                specialBlockTime[TimeType.BombSpawn] = 10;
                specialBlockTime[TimeType.DrillSpawn] = 10;
                specialBlockTime[TimeType.QuakeSpawn] = 10;
            }

            defaultHP = Random.Range(2,MaxHP+1);

            if(defaultHP > specialBlockTime[TimeType.LimitHP])
                defaultHP = (int)specialBlockTime[TimeType.LimitHP];

            yield return new WaitForSeconds(1f);
        }

        while(true)
        {
            defaultHP = Random.Range(2,MaxHP+1);

            if(defaultHP > specialBlockTime[TimeType.LimitHP])
                defaultHP = (int)specialBlockTime[TimeType.LimitHP];
                
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetBase()
    {
        StartCoroutine("SpawnBase");
    }
    
    // * 1초 간격으로 랜덤한 블럭 생성
    IEnumerator SpawnBase()    
    {
        float t = mGravity;
        mGravity = 18;
        for(int i = 0; i < 5; i++)
        {
            nextBlock = Random.Range(0,Block.Length);
            if(nextBlock == 7)
            {
                while(nextBlock == 7)
                {
                    nextBlock = Random.Range(0,Block.Length);
                }
            }

            GameObject spawn = Instantiate(Block[nextBlock]);
            spawn.GetComponent<Drop>().SetMyPosition(nextBlock);
            mGravity -= 1;
            yield return new WaitForSeconds(Random.Range(0.35f,0.45f));
        }

        mGravity = t;

        yield return new WaitForSeconds(1f);
        BlockCheck.instance.EarthQuake();
    }

    // * 1초 간격으로 랜덤한 블럭 생성
    IEnumerator SpawnBlock()    
    {
        nextBlock = Random.Range(0,Block.Length);
        
        // * 0 ~ 99 -> 100개
        int p = Random.Range(0,100);

        // * 7이 나왔는데 p도 20이하라면 폭탄을 스폰
        // * 7이 나왔는데 p가 20이상이라면 nextBlock 새로 뽑기
        if(nextBlock == 7)
        {
            if(p > specialBlockTime[TimeType.BombSpawn])
            {
                while(nextBlock == 7)
                {
                    nextBlock = Random.Range(0,Block.Length);
                }
            }
        }

        GameObject spawn = Instantiate(Block[nextBlock]);

        // * p  0 ~ 19 -> 20/100 확률
        if(nextBlock == 5 && p < specialBlockTime[TimeType.QuakeSpawn])    
        {
            // 지진블럭 생성될 확률 로직 추가
            Destroy(spawn.GetComponent<Drop>());
            spawn.AddComponent<Drop_2>();
            spawn.GetComponent<Drop_2>().SetMyPosition(nextBlock);
        }
        // * p 0 ~ 49 -> 50/100 확률 -> 세로 블럭이 나왔을때 50%확률로 변경
        else if(nextBlock == 0 && p < specialBlockTime[TimeType.DrillSpawn]) 
        {
            // 드릴블럭 생성될 확률 로직 추가
            Destroy(spawn.GetComponent<Drop>());
            spawn.AddComponent<Drop_2>();
            spawn.GetComponent<Drop_2>().SetMyPosition(nextBlock);
        }
        else if(nextBlock < 7 && p > 95)
        {
            spawn.GetComponent<Drop>().GlassBlock(nextBlock);
        }
        else
        {
            spawn.GetComponent<Drop>().SetMyPosition(nextBlock);
        }

        yield return new WaitForSeconds(SpawnDelay);

        StartCoroutine("SpawnBlock");
    }
}
