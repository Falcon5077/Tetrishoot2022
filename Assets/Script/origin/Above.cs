using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Above : MonoBehaviour
{
    public Slider mSlider;
    public static Above instance;
    public GameObject planeObject;
    public int count = 0;
    public float UpTime = 60.0f; //바닥이 올라오는 시간
    Coroutine runningCoroutine = null;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        UpTime = Spawner.instance.specialBlockTime[TimeType.FloorSpawn]; // 15
        count = 0;
    }
    public void AboveStart()
    {
        if(runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        runningCoroutine = StartCoroutine("AboveBlock");
    }   
    

    public void DownBlock()
    {
        if(count == 0)
            return;
        runningCoroutine = null;
        BlockCheck.instance.DelayCheck();

        GameObject[] block = GameObject.FindGameObjectsWithTag("drop2");
        for(int i = 0; i < block.Length; i++)
        {
            block[i].transform.position -= new Vector3(0,1.0f,0);
        }

        planeObject.transform.position -= new Vector3(0,1.0f,0);

        count--;
    }

    IEnumerator AboveBlock()
    {
        float t = Mathf.Round(UpTime);
        Debug.Log(t);
        mSlider.maxValue = UpTime;
        mSlider.value = UpTime;
        for(int i = 0; i < t*10; i++)
        {
            mSlider.value -= 0.1f;
            yield return new WaitForSeconds(0.1f);  //시간이 되면 바닥 올리기
        }
        
        if(UpTime > 18)
            UpTime -= 1;

        if(Time.timeScale != 0){
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

            count++;
        }
        

        if(runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        runningCoroutine = StartCoroutine("AboveBlock");
    }
}
