using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
