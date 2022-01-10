using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int power = 1;
    // Start is called before the first frame update
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);   // speed의 속도로 전진
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.transform.parent.GetComponent<Drop>().isHit == false)   // 땅에 떨어진 블럭이 아니라면, 즉 떨어지고 있는 블럭이라면
        {
            other.gameObject.GetComponent<HeartPoint>().HeartCalc(power);
            power = 0;
            Destroy(this.gameObject);   // 총알과 블럭을 삭제
            //Destroy(other.gameObject);
        }
    }
}
