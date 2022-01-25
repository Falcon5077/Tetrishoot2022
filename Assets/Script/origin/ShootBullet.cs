using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public Joystick joystick;
    public GameObject bullet;
    public float turnSpeed = 3f;
    public float h = 0.0f;
    public float v = 0.0f;
    public float SpawnDelay = 2f;
    public Vector3 SpawnPosition;
    public bool isShoot;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnBullet");

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartTouch();
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            StopTouch();
        }
        DragMouse();
    }

    public void Shoot()
    {
        if(Time.timeScale == 0)
            return;
        GameObject temp = Instantiate(bullet,transform.position,Quaternion.identity);
        
        AudioManager.instance.ShootSound();
        // if(v < 0){   // 아래로 당겼을 때는 위치를 고정하고 각도조절하여 발사
        //     float angle = Mathf.Atan2(h,-v) * Mathf.Rad2Deg;
        //     temp.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        // }
        Destroy(temp,5f);
    }
    public void StartTouch(){
        isShoot = true;
        DragMouse();
        StartCoroutine("SpawnBullet");
    }
    public void StopTouch(){
        isShoot = false;
    }
    
    public void DragMouse()
    {
        Vector3 MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        h = joystick.Direction.x;
        v = joystick.Direction.y;
        //Debug.Log(h + " / " + v);

        // transform.position = new Vector3(MousePosition.x,-10,0);
        transform.position = new Vector3(MousePosition.x,transform.position.y,0);

        // if(v >= -0.1f){  // 위로 당겼을때만 포지션 이동
        //     transform.position = new Vector3(MousePosition.x,-10,0);
        // }
        
    }

    IEnumerator SpawnBullet()    // 1초 간격으로 랜덤한 블럭 생성
    {
        if(isShoot)
            Shoot();
        else
            yield break;

        yield return new WaitForSeconds(SpawnDelay);

        StartCoroutine("SpawnBullet");
    }
}
