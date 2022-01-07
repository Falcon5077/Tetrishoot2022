using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public GameObject Checker;
    
    public List<GameObject> block = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Shoot",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * 15,15,layerMask);

        if(hit.collider != null){
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && hit.collider.tag == "drop")
            {
                hit.collider.gameObject.layer = 9;
                block.Add(hit.collider.gameObject);
            }
            Debug.DrawLine(transform.position,hit.point);
        }
        
        if(block.Count >= 11)
        {
            for(int i = 0; i < block.Count; i++)
            {
                Destroy(block[i]);
            }
            
            block.Clear();
        }
    }

    public void Shoot()
    {
        GameObject temp = Instantiate(Checker);
        Destroy(temp,10f);
        Invoke("Shoot",0.5f);
    }
}
