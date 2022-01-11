using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public static ImageManager instance;
    public Sprite[] image;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(int a, SpriteRenderer mRenderer)
    {
        mRenderer.sprite = image[a];
    }
}
