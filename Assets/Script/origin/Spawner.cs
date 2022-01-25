using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public float mGravity;
    public int defaultHP;
    public int MaxHP;
    public float SpawnDelay = 2f;
    public GameObject[] Block;
    public int nextBlock;
    public float minX;
    public float maxX;
    public List<GameObject> Check = new List<GameObject>();
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
        mGravity = 3.5f;
        defaultHP = 3;
        Time.timeScale = 1;
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
    void Start()
    {
        
    }

    // 스코어는 바닥 부실때
    // 최대체력 점점 늘어나고 2~5까지 랜덤 5나올 확률 증가
    // 라인 클리어시 바닥 내려감 바닥올라오는거 프젝합치기
    IEnumerator Timer() // 최대체력 점점 늘어나고, 2~5까지 랜덤, 5나올 확률 증가, 바닥올라오는거, 프로젝트 합치기, 스코어는 바닥 부실때, 난이도 조절
    {
        defaultHP = 3;
        MaxHP = 3;

        for(int i = 0; i < 120; i++)
        {
            if((i+1) % 60 == 0)
            {
                MaxHP += 1;
            }
            defaultHP = Random.Range(2,MaxHP+1);
            yield return new WaitForSeconds(0.1f);
        }

        while(true)
        {
            int p = Random.Range(0,100);
            if(p > 60)
                defaultHP = 5;
            else if(p > 40)
                defaultHP = 4;
            else if(p > 20)
                defaultHP = 3;
            else if(p > 0)
                defaultHP = 2;
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetBase()
    {
        StartCoroutine("SpawnBase");
    }
    
    IEnumerator SpawnBase()    // 1초 간격으로 랜덤한 블럭 생성
    {
        float t = mGravity;
        mGravity = 15;
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
            
            yield return new WaitForSeconds(Random.Range(0.25f,0.4f));
        }

        mGravity = t;

        yield return new WaitForSeconds(1f);
        BlockCheck.instance.EarthQuake();
    }

    IEnumerator SpawnBlock()    // 1초 간격으로 랜덤한 블럭 생성
    {
        nextBlock = Random.Range(0,Block.Length);
        
        // 0 1 2 3 4 5 6 7 8 9 ~ 99 -> 100개
        int p = Random.Range(0,100);


        // 7이 나왔는데 p도 20이하라면 폭탄을 스폰
        // 7이 나왔는데 p가 20이상이라면 nextBlock 새로 뽑기
        if(nextBlock == 7)
        {
            if(p > 15)
            {
                while(nextBlock == 7)
                {
                    nextBlock = Random.Range(0,Block.Length);
                }
            }
        }

        GameObject spawn = Instantiate(Block[nextBlock]);

        if(nextBlock == 5 && p < 15)    //p  0 ~ 19 -> 20/100 확률
        {
            // 지진블럭 생성될 확률 로직 추가
            Destroy(spawn.GetComponent<Drop>());
            spawn.AddComponent<Drop_2>();
            spawn.GetComponent<Drop_2>().SetMyPosition(nextBlock);
        }
        else if(nextBlock == 0 && p < 15) //p 0 ~ 49 -> 50/100 확률 -> 세로 블럭이 나왔을때 50%확률로 변경
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

        //Destroy(temp,4f);
        yield return new WaitForSeconds(SpawnDelay);

        StartCoroutine("SpawnBlock");
    }
}
