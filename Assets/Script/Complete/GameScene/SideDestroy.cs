// * ---------------------------------------------------------- //
// * 게임 좌우에 블럭 파괴를 도와주는 레이저 오브젝트용 스크립트입니다.
// * 오브젝트 : Side Destroy
// * ---------------------------------------------------------- //

using System.Collections;
using UnityEngine;

public class SideDestroy : MonoBehaviour
{
    public GameObject LaserParticle;
    public bool isLaser = true;
    void Start()
    {
        AudioManager.instance.StopSound(4);
        LaserParticle.GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0,transform.position + new Vector3(0,1f,0));

        // *  Raycast를 발사합니다.
        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.up*25,25,layerMask);
        
        if(hit.collider != null){
            Debug.DrawLine(transform.position,hit.point);

            // * HitPoint에서 파티클을 재생합니다.
            LaserParticle.transform.position = hit.point;
            if(!LaserParticle.GetComponent<ParticleSystem>().isPlaying){
                LaserParticle.GetComponent<ParticleSystem>().Play();
            }

            // * 파괴된 오브젝트면 삭제합니다.
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
            {
                Destroy(hit.collider.gameObject);
            }
            
            // * LineRenderer의 끝 지점을 hit.point로 설정합니다.
            GetComponent<LineRenderer>().SetPosition(1,hit.point);

            // * Hit Object를 파괴합니다.
            if(isLaser == true)
                StartCoroutine("Laser",hit.collider.gameObject);
        }
        else{            
            // * Hit Object가 없다면 레이저를 대기시킵니다.
            if(LaserParticle.GetComponent<ParticleSystem>().isPlaying){
                LaserParticle.GetComponent<ParticleSystem>().Stop();
            }
            GetComponent<LineRenderer>().SetPosition(1,transform.position + new Vector3(0,1f,0));

        }
    }

    IEnumerator Laser(GameObject hitcollider)
    {
        isLaser = false;

        if(hitcollider == null)
            yield break;

        // * Hit Collider의 체력을 감소시킵니다.
        if(hitcollider.transform.position.y < 9 && hitcollider.GetComponent<HeartPoint>() != null)
        {
            hitcollider.GetComponent<HeartPoint>().HeartCalc(1);
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(0.2f);

        isLaser = true;
    }
}
