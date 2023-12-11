// * ---------------------------------------------------------- //
// * 터치 또는 클릭 시 사운드 재생 스크립트 입니다.
// * 오브젝트 : MainCamera
// * ---------------------------------------------------------- //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    void Update()
    {
        // * 터치 또는 클릭 시 사운드 재생
        if(Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
        }
    }
}
