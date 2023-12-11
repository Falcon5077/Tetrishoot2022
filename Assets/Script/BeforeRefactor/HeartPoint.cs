using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartPoint : MonoBehaviour
{
    public int HP;
    public GameObject hpText;
    public GameObject mHP;
    public Transform mCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if(HP == 0)
            HP = Spawner.instance.defaultHP;
        mCanvas = GameObject.Find("HPCanvas").transform;
        mHP = Instantiate(hpText,mCanvas);
        mHP.GetComponent<TextMeshProUGUI>().text = HP.ToString();
    }

    void Update()
    {
        if(mHP == null)
            return;

        mHP.transform.position = transform.position;        
    }

    public void HeartCalc(int v)
    {
        if(mHP == null)
            return;


        int c = gameObject.transform.parent.childCount;
        if(c <= 2 && Spawner.instance.isHard == true)
        {
            return;
        }

        HP -= v;

        mHP.GetComponent<TextMeshProUGUI>().text = HP.ToString();

        if(HP <= 0)
        {
            Destroy(mHP);
            Destroy(this.gameObject);

            if(c <= 3 && Spawner.instance.isHard == true)
            {
                for(int i = 0; i < c; i++)
                {
                    if(gameObject.transform.parent.GetChild(i).GetComponent<HeartPoint>() != null)
                        gameObject.transform.parent.GetChild(i).GetComponent<HeartPoint>().mHP.GetComponent<TextMeshProUGUI>().text = "";
                }
                return;
            }
        }
    }
}
