// * ---------------------------------------------------------- //
// * UI PopUp 애니메이션을 위한 스크립트입니다.
// * 오브젝트 : UI Object
// * ---------------------------------------------------------- //

using System.Collections;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    // * 애니메이션 반복 재생 여부 Boolean
    public bool isLoop = false;

    // * 애니메이션 재생할 UI Object
    public GameObject UIPanel;

    // * ---------------------------------------------------------- //
    // * 씬 로드 시 PopUp 애니메이션 재생
    private void OnEnable() {
        PopUp(UIPanel);
    }
    private void OnSceneLoaded()
    {
        PopUp(UIPanel);
    }
    private void Awake() {
        PopUp(UIPanel);
    }
    // * ---------------------------------------------------------- //
    // * PopUp 애니메이션입니다.
    public void PopUp(GameObject itweenAnimationObject)
    {
        if(itweenAnimationObject == null) return;

        Hashtable hash = new Hashtable();
        Vector3 h_amount = new Vector3(0.3f, 0.3f, 0f);
        float h_time = 0.5f;

        if(isLoop == true)
        {
            h_amount = new Vector3(0.6f,0.6f, 0f);
            h_time = 1.5f;

            // * h_time 만큼 기다렸다가 다시 재생
            StartCoroutine(LoopAnim(h_time));
        }

        hash.Add("amount", h_amount);
        hash.Add("time",h_time);
        hash.Add("ignoretimescale",true);

        iTween.PunchScale(itweenAnimationObject, hash);
    }

    IEnumerator LoopAnim(float interval_sec)
    {
        yield return new WaitForSecondsRealtime(interval_sec);

        PopUp(UIPanel);
    }
}
