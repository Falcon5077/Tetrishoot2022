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

    // Update is called once per frame
    void Update()
    {
        if(mHP == null)
            return;

        //mHP.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        mHP.transform.position = transform.position;        
    }

    public void HeartCalc(int v)
    {
        if(mHP == null)
            return;

        HP -= v;

        mHP.GetComponent<TextMeshProUGUI>().text = HP.ToString();

        if(HP <= 0) // Check 
        {
            //Above.instance.ScoreUp(10);
            Score.instance.ScoreUp(-10);
            Destroy(mHP);
            Destroy(this.gameObject);
        }
    }
}
