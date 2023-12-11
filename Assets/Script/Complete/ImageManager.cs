// * ---------------------------------------------------------- //
// * 오브젝트의 스프라이트를 관리하는 스크립트입니다.
// * 오브젝트 : GameManager
// * ---------------------------------------------------------- //

using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public static ImageManager instance;
    public Sprite[] image;

    void Awake()
    {
        if(instance == null) instance = this;
    }

    public void SetImage(int index, SpriteRenderer mRenderer)
    {
        mRenderer.sprite = image[index];
    }
}
