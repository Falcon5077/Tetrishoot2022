using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    public bool reStart = false;
    public List<GameObject> block = new List<GameObject>(); // ray에 닿은 블럭들을 저장하는 List
    public bool isOver = false;
    // Start is called before the first frame update

    // Update is called once per frame
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

        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * 25,25,layerMask);
        if(hit.collider != null){            
            Debug.DrawLine(transform.position,hit.point);
            if(hit.collider.tag == "drop2"){

                Time.timeScale = 0;
                AudioManager.instance.StopSound(7);
                AudioManager.instance.FailSound();
                isOver = true;
                StartCoroutine("overEffect");
                //UnityEditor.EditorApplication.isPaused = true;
            }  
        }
    }

    IEnumerator overEffect()
    {
        int c = Above.instance.count;
        int layerMask = (1 << LayerMask.NameToLayer("Ray")) | (1 << LayerMask.NameToLayer("Hit"));
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position,Vector2.right * 25,25,layerMask);
        yield return new WaitForSecondsRealtime(1f);
        for(int i = 0; i < 21-c; i++)
        {
            BlockCheck.instance.DelayCheck();
            hit = Physics2D.RaycastAll(transform.position,Vector2.right * 25,25,layerMask);
            foreach (RaycastHit2D h in hit)  // 관통되서 광선에 닿은 모든 물체들이 hits 배열에 담긴다. for문으로 모든 원소에 접근
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
        for(int i = 0; i < c; i++)
        {
            Above.instance.DownBlock();
            Debug.Log("down");
            yield return new WaitForSecondsRealtime(0.05f);
        }
        
        CountDown.instance.mText.text = "Touch To Restart";
        reStart = true;
    }
}
