using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drop_2 : MonoBehaviour
{
    public float boxSize = 0.95f;
    public float Gravity; // 중력
    public int BlockType; // 오른쪽으로 몇 칸 튀어 나왔는지 체크하기 위한 블록 타입
    public bool isHit;
    public Vector3 fixedPos;

    public string myColor;
    public int mWay;
    void Start()
    {
        Gravity = Spawner.instance.mGravity; // + Random.Range(0.1f,0.5f);
        if(GetComponent<SpecialBlock>().blockType == 5)
        {
            Gravity += 1.5f;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-Gravity); // 가속도 없는 중력
        for(int i = 0; i < 4; i++)
        {
            // 자식 오브젝트의 box colider를 스크립트로 지정, 변동될 가능성있기때문
            transform.GetChild(i).GetComponent<BoxCollider2D>().size = new Vector2(boxSize,boxSize);
        }
    }

    private void Update() {
        if(transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
        if(transform.childCount < 4)
        {
            if(GetComponent<SpecialBlock>() != null)
                Destroy(GetComponent<SpecialBlock>());
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "bullet")
        {
            return; // 총알과 닿으면 아무 동작하지 않고 return
        }
        if(isHit == false)  // 땅에 처음 닿았다면 
        {
            Gravity = 3f;
            if(GetComponent<SpecialBlock>() != null)
            {
                if(GetComponent<SpecialBlock>().blockType == 0)
                {
                    
                    if(other.gameObject.tag == "drop" || other.gameObject.tag == "drop2")
                    {
                        Destroy(other.gameObject);
                        //transform.GetChild(3).GetComponent<HeartPoint>().HeartCalc(5);

                        // 드릴 이펙트
                        
                        AudioManager.instance.BoomSound();
                        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.05f,0.05f,0),0.5f);

                        isHit = false;
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-Gravity); // 가속도 없는 중력
                        return;
                    }
                    else if(other.gameObject.tag == "Quad")
                    {
                        AudioManager.instance.LandSound();                        
                    }
                }
                if(GetComponent<SpecialBlock>().blockType == 5)
                {
                    BlockCheck.instance.EarthQuake();
                }
            }
            else
            {
                AudioManager.instance.LandSound();
            }

            double x = transform.position.x;    // x,y 좌표 저장
            double y = transform.position.y;
            y = System.Math.Truncate(y*10)/10;  // 소수점 제거 후

            double zr = transform.rotation.eulerAngles.z; // z 각도 저장
            zr = System.Math.Truncate(zr*10)/10;

            // 위치와 각도를 교정 (소수점 한자리 수 까지)
            //transform.position = new Vector3(((float)x),((float)y),0);
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
                    ImageManager.instance.SetImage(0,transform.GetChild(i).GetComponent<SpriteRenderer>());
                    //transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.black;
                    transform.GetChild(i).gameObject.tag = "drop";
                    transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                    transform.GetChild(i).gameObject.AddComponent<Land>();
                    Destroy(transform.GetChild(i).gameObject.GetComponent<HeartPoint>());
                    Destroy(transform.GetChild(i).gameObject.GetComponent<HeartPoint>().mHP);
                }
            }

            if(GetComponent<SpecialBlock>() != null)
            {
                if(GetComponent<SpecialBlock>().blockType == 5)
                {
                    //BlockCheck.instance.EarthQuake();
                }
            }
        }
        else
        {
            // 이후에 발생하는 충돌에는 고정된 좌표로 고정
            //transform.position = fixedPos;
        }

    }

    // 블럭 오브젝트 회전 시 화면 밖으로 나가는 블록 조정하는 함수
    public void SetMyPosition(int a)
    {
        int way = 0;
        int minX = 0;
        int maxX = 0;
        int mColor = 8;

        switch(a)
        {
            case 0:        
                way = 0;
                minX = -4;
                maxX = 4;
                SpecialBlock temp = gameObject.AddComponent<SpecialBlock>();
                temp.blockType = 0;
                break;
            case 5:
                way = 0;
                minX = -4;
                maxX = 3;
                SpecialBlock temp2 = gameObject.AddComponent<SpecialBlock>();
                temp2.blockType = 5;
                break;
        }
        mWay = way;
        myColor = mColor.ToString();
        for(int i = 0; i < 4; i++)
        {
            transform.GetChild(i).rotation = Quaternion.Euler(0,0,0);
            SpriteRenderer mRen = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if(ImageManager.instance != null)
                ImageManager.instance.SetImage(mColor,mRen);
        }

        if(ImageManager.instance != null)
            {
                if(GetComponent<SpecialBlock>().blockType == 0)
                {
                    ImageManager.instance.SetImage(9,transform.GetChild(3).GetComponent<SpriteRenderer>());
                    ImageManager.instance.SetImage(12,transform.GetChild(0).GetComponent<SpriteRenderer>());
                    ImageManager.instance.SetImage(12,transform.GetChild(1).GetComponent<SpriteRenderer>());
                    ImageManager.instance.SetImage(12,transform.GetChild(2).GetComponent<SpriteRenderer>());
                }
            }

        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        transform.position = new Vector2(Random.Range(minX,maxX+1),Camera.main.transform.position.y + 15);
    }
}
