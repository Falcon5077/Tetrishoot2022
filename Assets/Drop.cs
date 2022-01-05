using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject[] child;
    public float boxSize = 0.95f;
    public float Gravity; // 중력
    public int BlockType; // 오른쪽으로 몇 칸 튀어 나왔는지 체크하기 위한 블록 타입
    public bool isHit;
    public Vector3 fixedPos;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-Gravity); // 가속도 없는 중력
        for(int i = 0; i < 4; i++)
        {
            // 자식 오브젝트의 box colider를 스크립트로 지정, 변동될 가능성있기때문
            transform.GetChild(i).GetComponent<BoxCollider2D>().size = new Vector2(boxSize,boxSize);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "bullet")
        {
            return; // 총알과 닿으면 아무 동작하지 않고 return
        }
        if(isHit == false)  // 땅에 처음 닿았다면 
        {
            double x = transform.position.x;    // x,y 좌표 저장
            double y = transform.position.y;
            y = System.Math.Truncate(y*10)/10;  // 소수점 제거 후

            double zr = transform.rotation.eulerAngles.z; // z 각도 저장
            zr = System.Math.Truncate(zr*10)/10;

            // 위치와 각도를 교정 (소수점 한자리 수 까지)
            transform.position = new Vector3(((float)x),((float)y),0);
            transform.rotation = Quaternion.Euler(new Vector3(0,0,((float)zr)));

            // 중력 제거, 위치 고정
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            // 교정한 현재 좌표를 fixedPos에 저장
            fixedPos = transform.position;

            // 최초 충돌 동작 완료
            isHit = true;
                
            // 자식 오브젝트의 색깔을 white -> black
            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i) != null){
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.black;
                    //transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                }
            }
        }
        else
        {
            // 이후에 발생하는 충돌에는 고정된 좌표로 고정
            transform.position = fixedPos;
        }
    }

    // 블럭 오브젝트 회전 시 화면 밖으로 나가는 블록 조정하는 함수
    public void SetMyPosition(int a)
    {
        
        int way = 0;
        int minX = 0;
        int maxX = 0;
        switch(a)
        {
            case 0:
                way = Random.Range(0,2); 
                if(way == 0)
                {
                    minX = -5;
                    maxX = 5;
                }else if(way == 1)
                {
                    minX = -5;
                    maxX = 2;
                }
                break;
            case 1:
                way = Random.Range(0,4);
                if(way == 0)
                {
                    minX = -5;
                    maxX = 4;
                }else if(way == 1)
                {
                    minX = -5;
                    maxX = 3;
                }else if(way == 2)
                {
                    minX = -4;
                    maxX = 5;
                }else if(way == 3)
                {
                    minX = -3;
                    maxX = 5;
                }
                break;
            case 2:
                way = Random.Range(0,4);
                if(way == 0)
                {
                    minX = -5;
                    maxX = 4;
                }else if(way == 1)
                {
                    minX = -5;
                    maxX = 3;
                }else if(way == 2)
                {
                    minX = -4;
                    maxX = 5;
                }else if(way == 3)
                {
                    minX = -3;
                    maxX = 5;
                }
                break;
            case 3:
                way = Random.Range(0,4);
                if(way == 0)
                {
                    minX = -5;
                    maxX = 3;
                }else if(way == 1)
                {
                    minX = -5;
                    maxX = 4;
                }else if(way == 2)
                {
                    minX = -3;
                    maxX = 5;
                }else if(way == 3)
                {
                    minX = -4;
                    maxX = 5;
                }
                break;
            case 4:
                way = Random.Range(0,4);
                if(way == 0)
                {
                    minX = -5;
                    maxX = 3;
                }else if(way == 1)
                {
                    minX = -5;
                    maxX = 4;
                }else if(way == 2)
                {
                    minX = -3;
                    maxX = 5;
                }else if(way == 3)
                {
                    minX = -4;
                    maxX = 5;
                }
                break;
            case 5:
                way = 0;
                minX = -5;
                maxX = 4;
                break;
            case 6:
                way = Random.Range(0,4);
                if(way == 0)
                {
                    minX = -5;
                    maxX = 3;
                }else if(way == 1)
                {
                    minX = -5;
                    maxX = 4;
                }else if(way == 2)
                {
                    minX = -3;
                    maxX = 5;
                }else if(way == 3)
                {
                    minX = -4;
                    maxX = 5;
                }
                break;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0,0,way * 90));
        transform.position = new Vector2(Random.Range(minX,maxX+1),10);
    }
}
