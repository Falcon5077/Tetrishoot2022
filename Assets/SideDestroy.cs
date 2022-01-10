using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isLaser = true;
    void Start()
    {
        GetComponent<LineRenderer>().SetPosition(0,transform.position + new Vector3(0,0.5f,0));
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.up*25,25,layerMask);
        
        if(hit.collider != null){
            Debug.DrawLine(transform.position,hit.point);
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
            {
                Destroy(hit.collider.gameObject);
            }

            GetComponent<LineRenderer>().SetPosition(1,hit.point);
            if(isLaser == true)
                StartCoroutine("Laser",hit.collider.gameObject);
        }
        else{
            GetComponent<LineRenderer>().SetPosition(1,transform.position);
        }
    }

    IEnumerator Laser(GameObject hitcollider)
    {
        isLaser = false;

        if(hitcollider == null)
            yield break;

        if(hitcollider.transform.position.y < 9)
            hitcollider.GetComponent<HeartPoint>().HeartCalc(1);
        yield return new WaitForSeconds(0.25f);

        isLaser = true;
    }
}
