using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Testing : MonoBehaviour
{
    public TextMeshProUGUI[] mText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAboveTime(float a){
        Above.instance.UpTime += a;
        Above.instance.UpTime = Mathf.Round(Above.instance.UpTime * 10)/10;
        mText[0].text = Above.instance.UpTime.ToString();
    }
    public void SetSpawnTime(float a){
        Spawner.instance.SpawnDelay += a;
        Spawner.instance.SpawnDelay  = Mathf.Round(Spawner.instance.SpawnDelay  * 10)/10;
        mText[1].text = Spawner.instance.SpawnDelay.ToString();
    }
    public void SetDropTime(float a){
        Spawner.instance.mGravity += a;
        Spawner.instance.mGravity = Mathf.Round(Spawner.instance.mGravity * 10)/10;
        mText[2].text = Spawner.instance.mGravity.ToString();
    }
}
