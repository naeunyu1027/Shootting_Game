using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
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
    public List<GameObject> powerObjectPool;
    
    // SpawnPoints
    public Transform[] spawnPoints;
    
    public Transform Parent;
    
    void Start()
    {
        createTime = Random.Range(minTime, maxTime);
        
        powerObjectPool = new List<GameObject>();
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject power = Instantiate(enemyFactory, Parent);
            powerObjectPool.Add(power);
            
            power.SetActive(false);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // 1. 생성 시간이 됐으니까
        if (currentTime > createTime)
        {
            
            if (powerObjectPool.Count > 0)
            {
                GameObject power = powerObjectPool[0];
                power.SetActive(true);
                powerObjectPool.Remove(power);

                int index = Random.Range(0, spawnPoints.Length);
                power.transform.position = spawnPoints[index].position;
            }


            createTime = Random.Range(minTime, maxTime);
            currentTime = 0;

        }
    }
}
