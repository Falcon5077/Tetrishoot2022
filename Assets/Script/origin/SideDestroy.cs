using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDestroy : MonoBehaviour
{
    public GameObject LaserParticle;
    // Start is called before the first frame update
    public bool isLaser = true;
    void Start()
    {
        AudioManager.instance.StopSound(4);
        LaserParticle.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0,transform.position + new Vector3(0,1f,0));

        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.up*25,25,layerMask);
        
        if(hit.collider != null){
            Debug.DrawLine(transform.position,hit.point);

            LaserParticle.transform.position = hit.point;
            if(!LaserParticle.GetComponent<ParticleSystem>().isPlaying){
                LaserParticle.GetComponent<ParticleSystem>().Play();
            }

            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
            {
                Destroy(hit.collider.gameObject);
            }
            
            GetComponent<LineRenderer>().SetPosition(1,hit.point);
            if(isLaser == true)
                StartCoroutine("Laser",hit.collider.gameObject);
        }
        else{            
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

        if(hitcollider.transform.position.y < 9 && hitcollider.GetComponent<HeartPoint>() != null)
        {
            hitcollider.GetComponent<HeartPoint>().HeartCalc(1);
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(0.2f);

        isLaser = true;
    }
}
