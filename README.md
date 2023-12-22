# 2022 슈퍼센트 하이퍼캐쥬얼 공모전 참가

블럭을 움직이는 테트리스가 아니라 총알을 발사하는 테트리스입니다. <br>
1️⃣ 테트리스 블럭을 이동과 회전을 못하는 대신 총알을 발사하여 블럭을 파괴합니다. <br>
2️⃣ 블럭이 파괴되어 원래 형태를 벗어난 블럭들은 개별 블럭이 되어 중력의 영향을 받습니다. <br>
3️⃣ 한 줄이 완성되면 허공에 떠 있는 블럭들도 중력의 영향을 받습니다.

### [테트리슛 구글 플레이스토어](https://play.google.com/store/apps/details?id=com.dibs.gaming.Tetrishoot&pli=1)

<img alt="image" src="https://github.com/Falcon5077/TetrishootApp/assets/32628758/e1a5e004-86e1-43d3-a8f3-922ff92b0d31" width="25%" height="25%">

---

<div style="display: flex; justify-content: space-between;">
  <img src="https://github.com/Falcon5077/Tetrishoot2022/assets/32628758/05707d29-98ec-4cd8-924f-ec370fa5ddc3" width="30%" height="30%">
  <img src="https://github.com/Falcon5077/Tetrishoot2022/assets/32628758/11b41737-c925-442a-8a67-a4bb2aa23d91" width="30%" height="30%">
  <img src="https://github.com/Falcon5077/Tetrishoot2022/assets/32628758/d92616c2-9394-4551-a300-8891bb216f0d" width="30%" height="30%">
</div>


<H2> RayCast로 한 줄 완성 감지</H2>
테트리스 블럭들을 좌표에 맞게 배열에 저장하여 연산하는 것이 아니라 블럭이 쌓일 수 있는 라인마다 Raycast를 이용하여 한줄이 완성되었는지 확인합니다. 

```
// Ray(8) 레이어만 닿는 raycast를 발사
int layerMask = 1 << LayerMask.NameToLayer("Ray");
RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * 25,25,layerMask);

if(hit.collider != null){
    // 땅에 떨어진 블럭, 떨어지지 않는 중, Hit하지 않은 블럭 체크
    if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && (hit.collider.tag == "drop2"))
    {
        // layer를 Hit로 변경 한번 체크한 블럭은 점검하지 않기 위해서
        hit.collider.gameObject.layer = 9;            
        block.Add(hit.collider.gameObject);
    }
    
    // 블럭이 9개 미만이고 Wall에 닿았다면 한 줄이 완성되지 않았다는 것
    if(block.Count < 9){
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ray") && hit.collider.tag == "Wall")
        {
            StartCoroutine("ClearBlock");
        }
    }
}

if(block.Count >= 9)
{
    for(int i = 0; i < block.Count; i++)
    {
        // 라인 완성 후 폭발 이펙트
        GameObject ex = Instantiate(exploParticle,block[i].position);
        Destroy(ex,1f);
        Destroy(block[i]);
        
        AudioManager.instance.BoomSound();
        iTween.ShakePosition(Camera.main.gameObject,
        new Vector3(0.1f,0.1f,0),0.5f);
    }
    
    block.Clear();  // List 초기화
    
    // Line Check를 1초간 휴식 -> 라인을 클리어하고 블럭들이 떨어짐을 기다리기 위함
    StartCoroutine("DelayLine");
    Above.instance.DownBlock(); // 떠 있는 블럭의 중력 계산
    Score.instance.ScoreUp(200); // 점수 획득
}
```
