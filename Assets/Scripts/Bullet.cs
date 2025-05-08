using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 이동 속도
    public float speed = 5f;

    /*private PlayerFire playerFire;*/
    [SerializeField] private GameObject PlayerFire;

    void Start()
    {
        // PlayerFire 스크립트 찾아놓기
        PlayerFire = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // 1. 방향을 구한다.
        Vector3 dir = Vector3.up;

        // 2. 이동하고 싶다. 공식 P = P0 + vt
        transform.position += dir * speed * Time.deltaTime;
    }

    // 충돌 처리 (물리 충돌)
    private void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }

    // 충돌 처리 (트리거 충돌)
    private void OnTriggerEnter(Collider other)
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
            if(PlayerFire != null)
            PlayerFire.GetComponent<PlayerFire>().bulletObjectPool.Add(gameObject);
        /*}*/
    }
}