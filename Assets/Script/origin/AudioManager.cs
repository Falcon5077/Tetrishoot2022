using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] mAudio;
    public static AudioManager instance;
    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.instance.BoomSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSound(int a)
    {
        mAudio[a].Stop();
    }

    public void BoomSound()
    {
        mAudio[0].Play();
    }
    public void LandSound()
    {
        mAudio[1].Play();
    }
    public void ShootSound()
    {
        mAudio[2].Play();
    }
    public void HitSound()
    {
        mAudio[3].Play();
    }
    public void LaserSound()
    {
        if(!mAudio[4].isPlaying)
        {
            mAudio[4].Play();
        }
    }

    public void ClearSound()
    {
        mAudio[5].Play();
    }

    public void CountSound()
    {
        mAudio[6].Play();
    }
    public void FailSound()
    {
        mAudio[8].Play();
    }
}
