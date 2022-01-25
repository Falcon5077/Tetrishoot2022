using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public bool isLoop = false;
    public GameObject UIPanel;

    private void OnEnable() {
        if(UIPanel !=null)
            PopUp(UIPanel);
    }
    void OnSceneLoaded()
    {
        if(UIPanel !=null)
            PopUp(UIPanel);
    }

    private void Awake() {
        if(UIPanel !=null)
            PopUp(UIPanel);
    }

    public void PopUp(GameObject temp)
    {
        System.Collections.Hashtable hash =
                    new System.Collections.Hashtable();
        
        if(isLoop == true)
        {

            hash.Add("amount", new Vector3(0.6f,0.6f, 0f));
            hash.Add("time",1.5f);
        }
        else
        {
            hash.Add("amount", new Vector3(0.3f, 0.3f, 0f));
            hash.Add("time", 0.5f);
        }
        hash.Add("ignoretimescale",true);

        iTween.PunchScale(temp, hash);

        if(isLoop == true)
        {
            StartCoroutine(LoopAnim(1.5f));
        }
    }

    IEnumerator LoopAnim(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);

        PopUp(UIPanel);
    }
}
