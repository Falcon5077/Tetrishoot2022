using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public GameObject exploParticle;    // 폭발 파티클
    public List<GameObject> block = new List<GameObject>(); // ray에 닿은 블럭들을 저장하는 List

    public static bool isCheck = true; // 현재 ray를 쐈는지
    public bool myCheck = true;
    public static BlockCheck instance;
    public float delay = 0f;
    private void Awake() {
        instance = this;
    }

    public void DelayCheck(){
        StartCoroutine("DelayLine");
    }
    
    public void EarthQuake()
    {
        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
        Land.LineClear = true;
        StartCoroutine("DelayLine");
    }

    // Update is called once per frame
    void Update()
    {
        if(isCheck == false || myCheck == false)
        {
            StartCoroutine("ClearBlock");
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EarthQuake();
        }

        // Ray(8) 레이어만 닿는 raycast를 발사
        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * 25,25,layerMask);
        
        if(hit.collider != null){            
            Debug.DrawLine(transform.position,hit.point);

            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "Untagged"))
            {
                StartCoroutine("ClearBlock");
            }

            // 땅에 떨어진 블럭, 떨어지지 않는 중, Hit하지 않은 블럭 체크
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
            {
                hit.collider.gameObject.layer = 9;  // layer를 Hit로 변경 한번 체크한 블럭은 점검하지 않기 위해서
                block.Add(hit.collider.gameObject);
            }

            // 블럭이 9개 미만이고 Wall에 닿았다면 블럭초기화
            if(block.Count < 9){
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && hit.collider.tag == "Wall")
                {
                    StartCoroutine("ClearBlock");
                }
            }
        }

        if(block.Count >= 9)
        {
            for(int i = 0; i < block.Count; i++)
            {
                Color mColor = new Color(0,0,0,1);
                //SetColor("1",ref mColor);
                GameObject ex = Instantiate(exploParticle,block[i].GetComponent<Renderer>().bounds.center,Quaternion.identity);
                ParticleSystem.MainModule psmain = ex.GetComponent<ParticleSystem>().main;
                psmain.startColor = new ParticleSystem.MinMaxGradient(mColor, new Color32(105,80,40,255)); 
                Destroy(ex,1f);

                Destroy(block[i]);

                AudioManager.instance.BoomSound();
                iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f,0.1f,0),0.5f);
            }

            block.Clear();  // List 초기화
            Land.LineClear = true;  // 땅에 떨어져서 Land.cs를 가지고 있는 오브젝트에게 LineClear를 알림

            // Check를 1초간 휴식 -> 라인을 클리어하고 블럭들이 떨어짐을 기다리기 위함
            StartCoroutine("DelayLine");
            Above.instance.DownBlock();
            //Above.instance.ScoreUp(100);
            Score.instance.ScoreUp(100);
            //Camera.main.transform.position += new Vector3(0,1,0);
        }
    }

    IEnumerator DelayLine()
    {
        isCheck = false;
        myCheck = false;
        
        StartCoroutine("ClearBlock");
        yield return new WaitForSeconds(1f);
        
        isCheck = true;
    }

    IEnumerator ClearBlock()
    {
        for(int i = 0; i < block.Count; i++)
        {
            if(block[i] != null)
                block[i].layer = 8; // ray를 다시 맞추기위해서 레이어를 Hit에서 Ray로 변경
        }
        
        block.Clear(); // List 초기화

        yield return new WaitForSeconds(1.5f);

        myCheck = true;

        //StartCoroutine("CheckBlock");
    }

}
