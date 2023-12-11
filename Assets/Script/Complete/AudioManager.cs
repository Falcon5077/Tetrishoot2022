// * ---------------------------------------------------------- //
// * 재생할 Audio를 관리하는 스크립트입니다.
// * 오브젝트 : AudioManager
// * ---------------------------------------------------------- //

using UnityEngine;

// * ---------------------------------------------------------- //
// * 재생할 AudioSource 의 인덱스를 위한 Enum 입니다.
public enum SoundIndex
{
    Boom,
    Land,
    Shoot,
    Hit,
    Laser,
    Clear,
    Count,
    Fail,
    BackGroundMusic
}
// * ---------------------------------------------------------- //

public class AudioManager : MonoBehaviour
{
    public AudioSource[] mAudio;
    public static AudioManager instance;
    private void Awake() {
        instance = this;
    }

    // * Index로 사운드를 재생합니다.
    public void PlaySoundByIndex(int index)
    {
        mAudio[index].Play();
    }

    // * Index로 사운드를 정지합니다.
    public void StopSound(int index)
    {
        mAudio[index].Stop();
    }

    // * ---------------------------------------------------------- //
    // * 지정된 인덱스 사운드를 재생합니다.
    public void BoomSound()
    {
        mAudio[(int)SoundIndex.Boom].Play();
    }
    public void LandSound()
    {
        mAudio[(int)SoundIndex.Land].Play();
    }
    public void ShootSound()
    {
        mAudio[(int)SoundIndex.Shoot].Play();
    }
    public void HitSound()
    {
        mAudio[(int)SoundIndex.Hit].Play();
    }
    public void LaserSound()
    {
        if(!mAudio[(int)SoundIndex.Laser].isPlaying)
        {
            mAudio[(int)SoundIndex.Laser].Play();
        }
    }

    public void ClearSound()
    {
        mAudio[(int)SoundIndex.Clear].Play();
    }

    public void CountSound()
    {
        mAudio[(int)SoundIndex.Count].Play();
    }
    public void FailSound()
    {
        mAudio[(int)SoundIndex.Fail].Play();
    }
}
