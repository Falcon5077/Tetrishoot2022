using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public GameObject Checker;
    
    public List<GameObject> block = new List<GameObject>();

    public bool isCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Shoot",0.5f);
        
        StartCoroutine("CheckBlock");
    }

    // Update is called once per frame
    void Update()
    {
        if(isCheck == false)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Land.LineClear = true;
        }

        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * 25,25,layerMask);
        
        if(hit.collider != null){
            Debug.DrawLine(transform.position,hit.point);
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
            {
                hit.collider.gameObject.layer = 9;
                block.Add(hit.collider.gameObject);
            }

            if(block.Count < 9){
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && hit.collider.tag == "Wall")
                {
                    StartCoroutine("CheckBlock");
                }
            }
        }

        
        if(block.Count >= 9)
        {
            for(int i = 0; i < block.Count; i++)
            {
                Destroy(block[i]);
            }
            block.Clear();
            Land.LineClear = true;
            StartCoroutine("DrawLine");
            //Camera.main.transform.position += new Vector3(0,1,0);
        }

        
        
        
    }

    IEnumerator DrawLine()
    {
        isCheck = false;
        yield return new WaitForSeconds(1f);
        isCheck = true;
    }

    IEnumerator CheckBlock()
    {
        for(int i = 0; i < block.Count; i++)
        {
            block[i].layer = 8;
        }
        block.Clear();

        yield return new WaitForSeconds(1f);

        //StartCoroutine("CheckBlock");
    }

    public void Shoot()
    {
        GameObject temp = Instantiate(Checker);
        Destroy(temp,10f);
        Invoke("Shoot",0.5f);
    }
}
