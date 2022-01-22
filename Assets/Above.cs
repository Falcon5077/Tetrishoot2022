using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Above : MonoBehaviour
{
    public GameObject planeObject;
    public float UpTime = 60.0f; //바닥이 올라오는 시간
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine("AboveBlock");
        }
    }

    IEnumerator AboveBlock()
    {
        yield return new WaitForSeconds(UpTime);  //시간이 되면 바닥 올리기

        BlockCheck.instance.DelayCheck();

        GameObject[] block = GameObject.FindGameObjectsWithTag("drop2");
        for(int i = 0; i < block.Length; i++)
        {
            block[i].transform.position += new Vector3(0,1.0f,0);
        }

        GameObject[] block2 = GameObject.FindGameObjectsWithTag("block");
        for(int i = 0; i < block2.Length; i++)
        {
            block2[i].transform.position += new Vector3(0,1.0f,0);
        }

        planeObject.transform.position += new Vector3(0,1.0f,0);
        
        StartCoroutine("AboveBlock");

    }
}
