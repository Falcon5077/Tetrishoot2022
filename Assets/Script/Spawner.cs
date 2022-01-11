using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public float mGravity;
    public float SpawnDelay = 2f;
    public GameObject[] Block;
    public int nextBlock;
    public float minX;
    public float maxX;
    public List<GameObject> Check = new List<GameObject>();
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
        mGravity = 3f;
    }
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Check.Add(transform.GetChild(i).gameObject);
            Check[i].transform.localPosition = new Vector3(-7.5f,0.95f * (i+1), 0);
            Check[i].GetComponent<BlockCheck>().delay = i/10;

        }
        StartCoroutine("SpawnBlock");
    }

    IEnumerator SpawnBlock()    // 1초 간격으로 랜덤한 블럭 생성
    {
        nextBlock = Random.Range(0,Block.Length);
        GameObject spawn = Instantiate(Block[nextBlock]);
        // 0 1 2 3 4 5 6 7 8 9 ~ 99 -> 100개
        int p = Random.Range(0,100);

        if(nextBlock == 5 && p < 80)    // 0 ~ 19 -> 20/100 확률
        {
            // 지진블럭 생성될 확률 로직 추가
            Destroy(spawn.GetComponent<Drop>());
            spawn.AddComponent<Drop_2>();
            spawn.GetComponent<Drop_2>().SetMyPosition(nextBlock);
        }
        else if(nextBlock == 0 && p < 50) // 0 ~ 49 -> 50/100 확률 -> 세로 블럭이 나왔을때 50%확률로 변경
        {
            // 드릴블럭 생성될 확률 로직 추가
            Destroy(spawn.GetComponent<Drop>());
            spawn.AddComponent<Drop_2>();
            spawn.GetComponent<Drop_2>().SetMyPosition(nextBlock);
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
