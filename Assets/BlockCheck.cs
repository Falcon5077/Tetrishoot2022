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
        
        StartCoroutine("CheckBlock");
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ray");
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * 15,15,layerMask);

        if(hit.collider != null){
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
            {
                hit.collider.gameObject.layer = 9;
                block.Add(hit.collider.gameObject);
            }
        }

        
        if(block.Count >= 11)
        {
            for(int i = 0; i < block.Count; i++)
            {
                Destroy(block[i]);
            }
            block.Clear();
            Camera.main.transform.position += new Vector3(0,1,0);
        }

        
        
        
    }


    IEnumerator CheckBlock()    // 1초 간격으로 랜덤한 블럭 생성
    {
        for(int i = 0; i < block.Count; i++)
        {
            block[i].layer = 8;
        }
        block.Clear();

        yield return new WaitForSeconds(1f);

        StartCoroutine("CheckBlock");
    }

    public void Shoot()
    {
        GameObject temp = Instantiate(Checker);
        Destroy(temp,10f);
        Invoke("Shoot",0.5f);
    }
}
