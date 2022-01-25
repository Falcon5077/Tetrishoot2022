using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlock : MonoBehaviour
{
    public int blockType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "bullet")
        {
            return; // 총알과 닿으면 아무 동작하지 않고 return
        }

        if(blockType == 0)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if(blockType == 5)
        {
            BlockCheck.instance.EarthQuake();
        }
    }
}
