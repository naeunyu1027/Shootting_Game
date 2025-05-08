using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [SerializeField] AudioSource PowerAudio; 
    public GameObject PowerManager;
    
    void Start()
    {
        PowerManager = GameObject.FindWithTag("PowerManager");
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
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
        PowerManager.GetComponent<PowerManager>().powerObjectPool.Add(gameObject);
        
        /*}*/
    }
    
}
