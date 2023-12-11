// * ---------------------------------------------------------- //
// * 바닥이 점점 올라오는 기능을 구현하는 스크립트입니다.
// * 오브젝트 : Quad
// * ---------------------------------------------------------- //

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Above : MonoBehaviour
{
    // * 싱글톤 접근을 위한 변수
    public static Above instance;

    // * 게임 진행에 사용될 UI Object
    public TextMeshProUGUI mText;
    public Slider mSlider;

    // * 바닥 오브젝트
    public GameObject planeObject;
    public int count = 0;
    
    // * 바닥이 올라오는 인터벌 시간
    public float UpTime = 60.0f; 

    // * 재생중인 코루틴을 담는 변수
    Coroutine runningCoroutine = null;

    int c = 0;
    
    void Awake()
    {
        if(instance == null) instance = this;
        
        UpTime = Spawner.instance.specialBlockTime[TimeType.FloorSpawn];
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


    // * ---------------------------------------------------------- //
    // * 바닥이 내려가면서 블럭을 한 칸 내립니다.
    public void DownBlock()
    {
        if(count == 0)
            return;

        runningCoroutine = null;
        BlockCheck.instance.DelayCheck();

        // * drop2 Tag가 붙은 블럭은 바닥에 착지한 오브젝트들입니다. 
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

        // * 바닥이 올라오는 타이머를 재생시킵니다.
        for(int i = 0; i < t*10; i++)
        {
            mSlider.value -= 0.1f;
            mSlider.value = Mathf.Round(mSlider.value * 10)/10;
            mText.text = mSlider.value.ToString();
            yield return new WaitForSeconds(0.1f);  
        }

        // * 시간이 되면 바닥 올리기
        
        if(c++ < 4 && UpTime > 3)
            UpTime -= 3;

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
        
        // * ---------------------------------------------------------- //
        // * 코루틴 재시작
        if(runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        runningCoroutine = StartCoroutine("AboveBlock");
    }
}
