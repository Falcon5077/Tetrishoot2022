using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    public GameObject Mode;
    public Button Normal;
    public Button Extreme;
    // Start is called before the first frame update
    public void SetMode(int a)
    {
        PlayerPrefs.SetInt("Mode",a); // 1 노말 2 익스트림
    }
    public void PopPosition()
    {
        System.Collections.Hashtable hash =
                    new System.Collections.Hashtable();
        hash.Add("position", new Vector3(0, 1000, 0f));
        hash.Add("time", 0.85f);
        hash.Add("easetype",iTween.EaseType.easeInOutBack);
        hash.Add("islocal", true);
        
        iTween.MoveTo(Normal.gameObject,hash);
        hash["position"] = new Vector3(0,600,0);
        iTween.MoveTo(Extreme.gameObject,hash);
    }

    public void RePosition()
    {
        System.Collections.Hashtable hash =
                    new System.Collections.Hashtable();
        hash.Add("position", new Vector3(0, 200, 0f));
        hash.Add("time", 0.2f);
        hash.Add("easetype",iTween.EaseType.linear);
        hash.Add("islocal", true);

        iTween.MoveTo(Normal.gameObject,hash);
        iTween.MoveTo(Extreme.gameObject,hash);

        Invoke("SetOff",0.3f);

    }
    public void SetOff()
    {
        Mode.SetActive(false);
    }
    public void Turn()
    {
        if(Mode.activeInHierarchy == true)
            RePosition();
        else if(Mode.activeInHierarchy == false){
            PopPosition();
            Mode.SetActive(true);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
