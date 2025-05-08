using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알을 생산할 공장
    public GameObject bulletFactory;

    // 총구
    public GameObject[] firePosition;

    // 탄창에 넣을 수 있는 총알의 개수
    public int poolSize = 10;

    // 오브젝트 풀 배열 -> 리스트로 교체
    // private GameObject[] bulletObjectPool;
    public List<GameObject> bulletObjectPool;
    
    public AudioSource PowerAudio;
    public AudioSource EmergencyAudio;

    // 태어날 때 오브젝트 풀(탄창)에 총알을 하나씩 생성해 넣고 싶다.
    // 1. 태어날 때
    void Start()
    {
        count = 1;
        // 2. 탄창을 총알 담을 수 있는 크기로 만들어준다.
        bulletObjectPool = new List<GameObject>();

        // 3. 탄창에 넣을 총알 개수만큼 반복해
        for (int i = 0; i < poolSize; i++)
        {
            // 4. 총알 공장에서 총알을 생성한다.
            GameObject bullet = Instantiate(bulletFactory);
            // 5. 총알을 오브젝트 풀에 넣고 싶다.
            // bulletObjectPool[i] = bullet;
            bulletObjectPool.Add(bullet);
            bullet.SetActive(false);
        }

        // 실행되는 플랫폼이 안드로이드일 경우 조이스틱을 활성화시킨다.
#if UNITY_ANDROID
        GameObject.Find("VirtualJoystick").SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("VirtualJoystick").SetActive(false);
#endif
    }
    public int count = 1;

    void OnTriggerEnter(Collider other)
    {
        if(/*CompareTag("PowerItem")*/other.gameObject.CompareTag("PowerItem"))
        {
            PowerAudio.Play();
            if (count < 6)
            {
                count++;
                Debug.Log(count);
                other.gameObject.SetActive(false);
            }
        }
        if(other.gameObject.CompareTag("EmergencyItem"))EmergencyAudio.Play();
    }

    void Update()       
    {
        // 목표 : 사용자가 발사 버튼을 누르면 총알을 발사하고 싶다.
        // 순서 : 1. 사용자가 발사 버튼을 누르면
        // 만약 사용자가 발사 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            
            // count 만큼 총알 발사 (최대 5개)
            for (int i = 0; i < count; i++)
            {
                if (bulletObjectPool.Count > 0)
                {
                    Fire(i);
                }
            }
        }

    }

    /*public void Fire()
    {
        if (bulletObjectPool.Count > 0)
        {
            GameObject bullet = bulletObjectPool[0];

            // 4. 총알을 발사하고 싶다 (활성화 시킨다)
            bullet.SetActive(true);
            // 오브젝트 풀에서 총알 제거
            bulletObjectPool.Remove(bullet);
            // 총알 위치시키기
            bullet.transform.position = firePosition[0].transform.position;
        }    
    }
*/
    

    public void Fire(int index)
    {
        if (bulletObjectPool.Count > 0 && index < firePosition.Length)
        {
            GameObject bullet = bulletObjectPool[0];
            bullet.SetActive(true);
            bulletObjectPool.RemoveAt(0);
            bullet.transform.position = firePosition[index].transform.position;
        }
    }

}