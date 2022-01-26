using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletManager : MonoBehaviour
{
    public int power = 1;
    
    public GameObject exploParticle;    // 폭발 파티클
    // Start is called before the first frame update
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);   // speed의 속도로 전진
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(transform.position.y > 10)
            return;
        if(other.gameObject.transform.parent.GetComponent<Drop>() != null)
        {
            if(other.gameObject.transform.parent.GetComponent<Drop>().isHit == false)   // 땅에 떨어진 블럭이 아니라면, 즉 떨어지고 있는 블럭이라면
            {
                AudioManager.instance.HitSound();

                if(other.gameObject.transform.parent.childCount <= 2 && Spawner.instance.isHard == true)
                {
                    other.gameObject.GetComponent<HeartPoint>().HeartCalc(0);   // 체력 감소
                }
                else
                {
                    other.gameObject.GetComponent<HeartPoint>().HeartCalc(power);   // 체력 감소
                }
                power = 0;  // 여러 블럭 충돌 방지

                Color mColor = new Color(0,0,0,1);
                ColorSystem.instance.SetColor(other.gameObject.transform.parent.GetComponent<Drop>().myColor,ref mColor);
                GameObject ex = Instantiate(exploParticle,other.transform.position,Quaternion.identity);
                ParticleSystem.MainModule psmain = ex.GetComponent<ParticleSystem>().main;
                psmain.startColor = new ParticleSystem.MinMaxGradient(new Color(1,1,1,1), mColor); 
                Destroy(ex,1f);

                Destroy(this.gameObject);   // 총알과 블럭을 삭제
                //Destroy(other.gameObject);
            }
        }
        else if(other.gameObject.transform.parent.GetComponent<Drop_2>() != null)
        {
            if(other.gameObject.transform.parent.GetComponent<Drop_2>().isHit == false)   // 땅에 떨어진 블럭이 아니라면, 즉 떨어지고 있는 블럭이라면
            {
                AudioManager.instance.HitSound();
                if(other.gameObject.transform.parent.childCount <= 2 && Spawner.instance.isHard == true)
                {
                    other.gameObject.GetComponent<HeartPoint>().HeartCalc(0);   // 체력 감소
                }
                else
                {
                    other.gameObject.GetComponent<HeartPoint>().HeartCalc(power);   // 체력 감소
                }
                power = 0;  // 여러 블럭 충돌 방지

                Color mColor = new Color(0,0,0,1);
                ColorSystem.instance.SetColor(other.gameObject.transform.parent.GetComponent<Drop_2>().myColor,ref mColor);
                GameObject ex = Instantiate(exploParticle,other.transform.position,Quaternion.identity);
                ParticleSystem.MainModule psmain = ex.GetComponent<ParticleSystem>().main;
                psmain.startColor = new ParticleSystem.MinMaxGradient(new Color(1,1,1,1), mColor); 
                Destroy(ex,1f);

                Destroy(this.gameObject);   // 총알과 블럭을 삭제
                //Destroy(other.gameObject);
            }
        }
    }
}
