using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject bombParticle;     // 폭탄 파티클
    public GameObject exploParticle;    // 블럭 폭발 파티클
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Bomb();
        }

        if(GetComponent<Land>() != null)
        {
            Bomb();
        }
    }

    private void OnDestroy() {
        Bomb();
    }

    public void Bomb()
    {
        GameObject particle = Instantiate(bombParticle,transform.position,Quaternion.identity);
        Destroy(particle,1f);
        BlockCheck.instance.EarthQuake();
        if(GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }

        // 콜라이더를 담을 수 있는 배열을 만든다.
        Collider2D[] colls;

        // 반경 10f에 위치한 오브젝트들을 배열에 담는다
        colls = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),3f);
        

        // foreach문을 통해서 colls배열에 존재하는 각각에 폭발효과를 적용해준다.
        foreach (Collider2D coll in colls)
        {
            // 조건문을 사용해서 특정레이어에 속한 오브젝트에만 영향을 줄 수 있다.(ex-플레이어만 날아가도록)
            if (coll.gameObject.tag == "drop" || coll.gameObject.tag == "drop2" || coll.gameObject.tag == "block")
            {
                Debug.Log(coll.name);
                if(coll.GetComponent<Rigidbody2D>() == null)
                {
                    coll.gameObject.AddComponent<Rigidbody2D>();
                }

                if(coll.GetComponent<Rigidbody2D>() != null)
                {
                    coll.GetComponent<BoxCollider2D>().isTrigger = true;
                    coll.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    // 해당 오브젝트의 Rigidbody를 가져와서 AddExplosionForce 함수를 사용해준다.
                    // AddExplosionForce(폭발력, 폭발위치, 반경, 위로 솟구쳐올리는 힘)
                    Vector2 direction = coll.transform.position - transform.position;
                    coll.GetComponent<Rigidbody2D>().AddForce(direction * 5f,ForceMode2D.Impulse);
                }

                Color mColor = new Color(0,0,0,1);
                GameObject ex = Instantiate(exploParticle,coll.transform.position,Quaternion.identity);
                ParticleSystem.MainModule psmain = ex.GetComponent<ParticleSystem>().main;
                psmain.startColor = new ParticleSystem.MinMaxGradient(mColor, new Color32(105,80,40,255)); 
                Destroy(ex,1f);

                coll.gameObject.tag = "Untagged";
                Destroy(coll.gameObject,1f);
                if(coll.GetComponent<Drop_2>() == null)
                    Destroy(coll.GetComponent<Drop>());
                else
                    Destroy(coll.GetComponent<Drop_2>());
            }
        // 코드 정리.
        // 검출된 오브젝트들 중에서 8번 레이어에 속한 오브젝트 각각을,
        // 폭발위치(coll오브젝트위치아님)기준으로 반경 20f까지 100f의 폭발력과 20f의 상향력으로 날려버린다.
        }
        if(GetComponent<HeartPoint>() != null)
            Destroy(GetComponent<HeartPoint>().mHP);

        Destroy(this.gameObject);
        //Destroy(transform.GetChild(0).gameObject);        
    }
}
