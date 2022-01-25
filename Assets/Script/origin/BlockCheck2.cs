using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck2 : MonoBehaviour
{
    public int hit = 0;
    public GameObject[] block;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        
        if(transform.position.x > 9.5f)
        {
            for(int i = 0; i < block.Length; i++)
            {
                Destroy(block[i].gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "block")
        {
            block[hit++] = other.gameObject;
        }

    }
}
