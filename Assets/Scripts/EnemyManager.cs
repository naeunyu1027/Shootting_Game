using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 현재 시간
    private float currentTime;
    // 일정 시간
    public float createTime = 1f;
    // 적 공장
    public GameObject enemyFactory;
    
    // 최소 시간
    [SerializeField]private float minTime = 1f;
    // 최대 시간
    [SerializeField]private float maxTime = 5f;
    
    // 오브젝트 풀 크기
    public int poolSize = 10;
    // 오브젝트 풀 배열 -> 리스트로 교체
    // private GameObject[] enemyObjectPool;
    public List<GameObject> enemyObjectPool;
    
    // SpawnPoints
    public Transform[] spawnPoints;
    
    public Transform Parent;
    void Start()
    {
        // 태어날 때 적의 생성 시간을 설정하고
        createTime = Random.Range(minTime, maxTime);
        
        // 2. 오브젝트 풀을 에너미들을 담을 수 있는 크기로 만들어준다
        // enemyObjectPool = new GameObject[poolSize];
        enemyObjectPool = new List<GameObject>();
        
        // 3. 오브젝트 풀에 넣을 에너미 개수만큼 반복해
        for (int i = 0; i < poolSize; i++)
        {
            // 4. 에너미 공장에서 에너미를 생성한다.
            GameObject enemy = Instantiate(enemyFactory, Parent);
            // 5. 에너미를 오브젝트 풀에 넣고 싶다.
            // enemyObjectPool[i] = enemy;
            enemyObjectPool.Add(enemy);
            
            enemy.SetActive(false);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // 1. 생성 시간이 됐으니까
        if (currentTime > createTime)
        {
            // 2. 오브젝트 풀에 에너미가 있다면
            GameObject enemy = enemyObjectPool[0];
            if (enemyObjectPool.Count > 0)
            {
                if (enemy != null)
                {
                    // 3. 에너미를 활성화 하고 싶다.
                    enemy.SetActive(true);
                    // 4. 오브젝프 풀에서 에너미 제거
                    enemyObjectPool.Remove(enemy);
                    // 랜덤으로 인덱스 선택
                    int index = Random.Range(0, spawnPoints.Length);
                    // 5. 에너미 위치치키기
                    enemy.transform.position = spawnPoints[index].position;
                }

                /*StartCoroutine(ReturnToPool(enemy));*/
            }
            // 2. 에너미 풀 안에 있는 에너미들 중에서
            // for (int i = 0; i < poolSize; i++)
            // {
            //     // 3. 비활성화된 에너미를
            //     // 만약 에너미가 비활성화됐다면
            //     GameObject enemy = enemyObjectPool[i];
            //     if (enemy.activeSelf == false)
            //     {
            //         // 에너미를 활성화하고 싶다.
            //         enemy.SetActive(true);
            //         // 에너미 위치시키기
            //         // 랜덤으로 인덱스 선택
            //         int index = Random.Range(0, spawnPoints.Length);
            //         enemy.transform.position = spawnPoints[index].position;
            //         // 에너미를 활성화 했기 때문에 검색 중단.
            //         break;
            //     }
            // }

            createTime = Random.Range(minTime, maxTime);
            currentTime = 0;


            // // 3. 적 공장에서 적을 생성해
            // GameObject enemy = Instantiate(enemyFactory);
            // // 내 위치에 갖다 놓고 싶다
            // enemy.transform.position = transform.position;
            // // 현재 시간을 0으로 초기화
            // currentTime = 0;
            // // 적을 생성한 후 적의 생성 시간을 다시 설정하고 싶다.
            // createTime = Random.Range(minTime, maxTime);
        }
    }

}
