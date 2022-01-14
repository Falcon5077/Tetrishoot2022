using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Above : MonoBehaviour
{
    public float UpTime = 10.0f;
    public GameObject[] bottom;
    public GameObject[] block;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveUp());
    }

    IEnumerator moveUp(){
        yield return new WaitForSeconds(UpTime);
        bottom = GameObject.FindGameObjectsWithTag("bottom");
        block = GameObject.FindGameObjectsWithTag("Plane");
        for(int i=0; i<= bottom.Length; i++){
            bottom[i].GetComponent<Drop>().fixedPos.y += 1.0f;
          
        }
          for(int i=0; i<= block.Length; i++){
            block[i].transform.position = Vector2.up;
        }
        StartCoroutine(moveUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
