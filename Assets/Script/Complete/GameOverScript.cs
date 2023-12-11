// * ---------------------------------------------------------- //
// * GaveOver를 감지하는 스크립트입니다.
// * 오브젝트 : GameOver
// * ---------------------------------------------------------- //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // * ray에 닿은 블럭들을 저장하는 List
    public List<GameObject> block = new List<GameObject>(); 

    // * 재시작을 위한 변수
    public bool reStart = false;

    // * 게임오버를 위한 변수
    public bool isOver = false;
    

    void Update()
    {
        if(reStart)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("GameScene");
            }
        }

        if(isOver == true)
            return;

        // * ---------------------------------------------------------- //
        // * Layer Mask를 생성하고 RayCast를 발사합니다.
        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * 25,25,layerMask);
        // * ---------------------------------------------------------- //

        // * RayCast에 오브젝트가 닿았다면 게임종료를 진행합니다.
        if(hit.collider != null){
            Debug.DrawLine(transform.position,hit.point);
            if(hit.collider.tag == "drop2"){

                Time.timeScale = 0;
                AudioManager.instance.StopSound((int)SoundIndex.BackGroundMusic);
                AudioManager.instance.FailSound();

                isOver = true;
                StartCoroutine("overEffect");
            }  
        }
    }


    // * ---------------------------------------------------------- //          
    // * 게임 오버 이펙트를 실행합니다.
    IEnumerator overEffect()
    {
        // * 올라온 바닥의 개수
        int groundBlockCount = Above.instance.count;

        int layerMask = (1 << LayerMask.NameToLayer("Ray")) | (1 << LayerMask.NameToLayer("Hit"));
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position,Vector2.right * 25,25,layerMask);

        yield return new WaitForSecondsRealtime(1f);

        for(int i = 0; i < 21 - groundBlockCount; i++)
        {
            BlockCheck.instance.DelayCheck();
            hit = Physics2D.RaycastAll(transform.position,Vector2.right * 25,25,layerMask);
            
            // * 관통되서 광선에 닿은 모든 물체들이 hits 배열에 담습니다. foreach로 모든 원소에 접근
            foreach (RaycastHit2D h in hit)  
            {
                if(h.collider != null)
                {
                    if(h.collider.tag == "drop2")
                    {
                        h.collider.gameObject.layer = 10;
                        block.Add(h.collider.gameObject);
                        h.collider.gameObject.SetActive(false);
                    }
                }
            }

            yield return new WaitForSecondsRealtime(0.05f);
            transform.position -= new Vector3(0,0.98f,0);
        }

        for(int i = 0; i < groundBlockCount; i++)
        {
            Above.instance.DownBlock();
            Debug.Log("down");
            yield return new WaitForSecondsRealtime(0.05f);
        }
        
        CountDown.instance.mText.text = "Touch To Restart";
        reStart = true;
    }
}
