using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyItem : MonoBehaviour
{
    public Transform Parent;
    public GameObject EmergencyManager;
    void Start()
    {
       Parent = GameObject.FindWithTag("EnemySpawn").transform;
        EmergencyManager = GameObject.FindWithTag("EmergencyManager");
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Parent != null)
            {
                foreach (Transform child in Parent.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            ReturnToPool();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }
    
    private void ReturnToPool()
    {
        // 총알 비활성화 + 다시 오브젝트 풀에 넣기
        gameObject.SetActive(false);
        EmergencyManager.GetComponent<EmergencyManager>().EmergencyObjectPool.Add(gameObject);
    }
}
