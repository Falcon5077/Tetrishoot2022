using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnDelay = 2f;
    public GameObject[] Block;
    public int nextBlock;
    public float minX;
    public float maxX;
    public List<GameObject> Check = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Check.Add(transform.GetChild(i).gameObject);
            Check[i].transform.localPosition = new Vector3(-7.5f,0.95f * (i+1), 0);

        }
        StartCoroutine("SpawnBlock");
    }

    IEnumerator SpawnBlock()    // 1초 간격으로 랜덤한 블럭 생성
    {
        nextBlock = Random.Range(0,Block.Length);
        GameObject temp = Instantiate(Block[nextBlock]);
        temp.GetComponent<Drop>().SetMyPosition(nextBlock);
        //Destroy(temp,4f);
        yield return new WaitForSeconds(SpawnDelay);

        StartCoroutine("SpawnBlock");
    }
}
