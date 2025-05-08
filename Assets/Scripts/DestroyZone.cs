using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    void Start()
    {
    }

    // 영역 안에 다른 물체가 감지될 경우
    private void OnTriggerEnter(Collider other)
    {
        // 그 물체를 없애고 싶다.
        // Destroy(other.gameObject);
        // 1. 만약 부딪힌 물체가 Bullet이거나 Enemy라면
        if (other.gameObject.name.Contains("Bullet") ||
            other.gameObject.name.Contains("Enemy")||
            other.gameObject.name.Contains("PowerItem")||
            other.gameObject.name.Contains("EmergencyItem"))
        {
            // 부딪힌 물체를 비활성화
            other.gameObject.SetActive(false);

            // 3. 부딪힌 물체가 총알일 경우 총알 리스트에 삽입
            if (other.gameObject.name.Contains("Bullet"))
            {
                // PlayeFire 클래스 얻어오기
                GameObject player = GameObject.Find("Player");
                if (player != null)
                {
                    PlayerFire playerFire = player.GetComponent<PlayerFire>();
                    playerFire.bulletObjectPool.Add(other.gameObject);
                }
            }
            else if (other.gameObject.name.Contains("Enemy"))
            {
                // EnemyManager 클래스 얻어오기
                GameObject emObject = GameObject.Find("EnemyManager");
                if (emObject != null)
                {
                    EnemyManager manager = emObject.GetComponent<EnemyManager>();
                    // 리스트에 총알 삽입
                    manager.enemyObjectPool.Add(other.gameObject);
                }
            }
        }
    }
}