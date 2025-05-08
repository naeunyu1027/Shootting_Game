using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표 : 적이 다른 물체와 충돌했을 때 폭발 효과를 발생시키고 싶다.
// 순서 : 
// 1. 적이 다른 물체와 충돌 했으니까
// 2. 폭발 효과 공장에서 폭발 효과를 하나 만들어야 한다.
// 3. 폭발 효과를 발생(위치) 시키고 싶다.
// 필요 속성 : 폭발 공장 주소(외부에서 값을 넣어준다)

public class Enemy : MonoBehaviour
{
    // 필요 속성 : 이동 속도
    public float speed = 5f;
    Vector3 dir;
    
    // 폭발 공장 주소(외부에서 값을 넣어준다)
    public GameObject explosionFactory;
    public GameObject emObject;

    void Start()
    {
        emObject = GameObject.FindWithTag("EnemyManager");
        /*EnemyManager manager = emObject.GetComponent<EnemyManager>();*/
    }
    void OnEnable()
    {
        // 0부터 9까지 10개의 값 중에 하나를 랜덤으로 가져온다
        int randValue = Random.Range(0, 10);
        // 만약 3보다 작으면 플레이어 방향
        if (randValue < 3)
        {
            // 플레이어를 찾아 target으로 하고 싶다.
            GameObject target = GameObject.Find("Player");
            // 방향을 구하고 싶다.
            if (target != null)
            {
                dir = target.transform.position - transform.position;
            }

            // 방향의 크기를 1로 하고 싶다.
            dir.Normalize();
        }
        else // 그렇지 않으면 아래 방향으로 정하고 싶다.
        {
            dir = Vector3.down;
        }
    }

    void Update()
    {
        // 1. 방향을 구한다.
        // Vector3 dir = Vector3.down;
        // 2. 이동하고 싶다.
        transform.position += dir * speed * Time.deltaTime;
    }
    
    // 충돌 시작
    // 1. 적이 다른 물체와 충돌 했으니까
    private void OnCollisionEnter(Collision other)
    {
        // 에너미를 잡을 때마다 현재 점수를 표시하고 싶다.
        ScoreManager.Instance.Score++;
        // ScoreManager.Instance.SetScore(ScoreManager.Instance.GetScore() + 1);
        
        // // 1. 씬에서 ScoreManager 객체를 찾아오자
        // GameObject smObject = GameObject.Find("ScoreManager");
        // // 2. ScoreManager 게임 오브젝트를 얻어온다.
        // ScoreManager sm = smObject.GetComponent<ScoreManager>();
        //
        // // ScoreManager로 관련 권한 이관 및 대체
        // // 3. ScoreManager의 Get/Set 메서드로 수정
        // sm.SetScore(sm.GetScore() + 1);
        
        // 2. 폭발 효과 공장에서 폭발 효과를 하나 만들어야 한다.
        GameObject explosion = Instantiate(explosionFactory);
        // 3. 폭발 효과를 발생(위치시키고 싶다)
        explosion.transform.position = transform.position;
        
        // 만약 부딪힌 객체가 Bullet인 경우에는 비활성화시켜 탄창에 다시 넣어준다.
        // 1. 만약 부딪힌 물체가 Bullet 이라면
        if (other.gameObject.name.Contains("MyBullet"))
        {
            // 2. 부딪힌 물체를 비활성화
            other.gameObject.SetActive(false);
        }
        else
        {
            Destroy(other.gameObject);    
        }
        // Destroy(gameObject);
        gameObject.SetActive(false);
        // EnemyManager 클래스 얻어오기`
       
        // 리스트에 총알 삽입
        /*emObject.GetComponent<EnemyManager>().enemyObjectPool.Add(gameObject);*/
        ReturnToPool();
    }

    void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }
    
    private void ReturnToPool()
    {
        // 총알 비활성화 + 다시 오브젝트 풀에 넣기
        /*if (gameObject.activeSelf)
        {*/
        gameObject.SetActive(false);
        /*playerFire.bulletObjectPool.Add(gameObject);*/
        emObject.GetComponent<EnemyManager>().enemyObjectPool.Add(gameObject);
        
        /*}*/
    }
}
