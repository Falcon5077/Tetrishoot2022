using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    
    public static bool LineClear = false;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<BoxCollider2D>().size = new Vector2(0.95f,0.98f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-3); // 가속도 없는 중력
        Debug.Log(transform.parent.childCount);
        if(transform.parent.childCount == 4)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        else
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            transform.tag = "drop2";
        }

        if(LineClear == true)
        {
            StartCoroutine("Gravity");
        }
    }

    IEnumerator Gravity()
    {
        
        GetComponent<BoxCollider2D>().size = new Vector2(0.95f,0.98f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;    
        yield return new WaitForSeconds(1f);
        LineClear = false;
        while(true)
        {
            if(GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                break;
            }
            else
                LineClear = true;
            yield return null;
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

    }
}
