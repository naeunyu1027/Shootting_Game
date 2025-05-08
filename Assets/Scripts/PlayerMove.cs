using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject player;
    void Start()
    {
        
    }

    void Update()
    {   ScreenChk();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Vector3 dir = Vector3.right * h + Vector3.up * v;
        Vector3 dir = new Vector3(h, v, 0);
        
        // P = P0 + vt 공식으로 변경
        // transform.Translate(dir * speed * Time.deltaTime);
        // transform.position = transform.position + dir * speed * Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;
    }


    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            player.SetActive(false);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {

    }

    private void ScreenChk() //카메라 안에 고정
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        if (worlpos.y < 0.05f) worlpos.y = 0.05f;
        if (worlpos.y > 0.95f) worlpos.y = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
