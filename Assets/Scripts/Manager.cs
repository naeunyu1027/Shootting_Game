using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GameOver;
    public bool Cheak;
    
    
    

    void Start()
    {
        
    }

    void Update()
    {

    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameOver.SetActive(true);
            Player.SetActive(false);
        }
    }

}
