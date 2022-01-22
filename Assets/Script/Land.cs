using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    
    public static bool LineClear = false;
    public bool isGravity = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.95f,0.98f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-3); // 가속도 없는 중력

        // 블럭이 온전하면 고정 아니라면 중력 적용
        if(transform.parent.childCount == 4)    
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        else
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)    // 떨어지는 중이 아니라면 drop2로 tag 변경
        {
            transform.tag = "drop2";
        }
        else if(GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f)
        {
            transform.tag = "drop";
        }

        if(LineClear == true && !isGravity)   // 라인 클리어시 중력 적용
        {
            StartCoroutine("Gravity");
        }
    }

    public void DestroyEachOther(GameObject temp)
    {
        Destroy(this.gameObject);
        Destroy(temp);
    }

    IEnumerator Gravity()
    {
        isGravity = true;
        GetComponent<BoxCollider2D>().size = new Vector2(0.95f,0.98f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;    
        yield return new WaitForSeconds(1f);
        LineClear = false;
        while(true)
        {
            if(GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
            {
                break;
            }
            else{
                LineClear = true;
            }
            
            yield return null;
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        isGravity = false;
        //LineClear = false;
    }

    public void rePosition()
    {
        double x = transform.position.x;    // x,y 좌표 저장
        double y = transform.position.y;
        x = System.Math.Truncate(x*10)/10;  // 소수점 제거 후
        y = System.Math.Truncate(y*10)/10;  // 소수점 제거 후

        // 위치와 각도를 교정 (소수점 한자리 수 까지)
        transform.position = new Vector3(((float)x),((float)y),0);
    }
}
