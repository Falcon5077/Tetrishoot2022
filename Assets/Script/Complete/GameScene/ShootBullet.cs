// * ---------------------------------------------------------- //
// * Bullet을 발사하는 스크립트입니다.
// * 오브젝트 : Shooter
// * ---------------------------------------------------------- //

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

    // * ---------------------------------------------------------- //
    // * Bullet의 Spawn 관련 필드입니다.
    public float SpawnDelay = 2f;
    public Vector3 SpawnPosition;
    public bool isShoot;
    public static bool canShoot = false;
    // * ---------------------------------------------------------- //

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
        // * 게임 정지 시 발사 중지
        if(Time.timeScale == 0 || canShoot == false)
            return;

        // * 총알을 발사
        GameObject temp = Instantiate(bullet,transform.position,Quaternion.identity);
        
        // * 소리를 재생
        AudioManager.instance.ShootSound();
        
        // * 5초 뒤 삭제
        Destroy(temp,5f);
    }
    public void StartTouch(){
        if(isShoot == true)
            return;

        isShoot = true;
        DragMouse();
        StartCoroutine("SpawnBullet");
    }
    public void StopTouch(){
        isShoot = false;
    }
    
    public void DragMouse()
    {
        // * 터치 좌표를 이동하면 Bullet Shooter의 포지션을 터치 좌표의 x축으로 이동합니다
        
        Vector3 MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        h = joystick.Direction.x;
        v = joystick.Direction.y;

        transform.position = new Vector3(MousePosition.x,transform.position.y,0);
    }

    IEnumerator SpawnBullet()
    {
        if(isShoot)
            Shoot();
        else
            yield break;

        yield return new WaitForSeconds(SpawnDelay);

        StartCoroutine("SpawnBullet");
    }
}
