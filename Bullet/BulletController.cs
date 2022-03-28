using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    Vector3 m_direction = Vector3.left;

    [SerializeField]
    float m_speed = 10f;
    // Start is called before the first frame update


    [SerializeField]
    Rigidbody2D m_rigidbody2D;


    Vector3 m_prevPos;
    public void SetBullet(Vector3 pos, Vector3 dir) 
    {
        transform.position = pos;
        m_direction = dir;

        if (dir == Vector3.right)
            transform.eulerAngles = new Vector3(0f,180f,transform.eulerAngles.z);
        else
            transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z);

        //m_rigidbody2D.AddForce(1,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Wall") || collision.CompareTag("Floor")) 
        {
            Debug.Log("Wall Hit");
            Destroy(gameObject);
        }*/
    }

    void Die() { Destroy(gameObject); }

    void Start()
    {
        Invoke("Die",2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_prevPos = transform.position;
        gameObject.transform.position += m_direction * m_speed * Time.deltaTime;

        var dir = gameObject.transform.position - m_prevPos;

        var hit = Physics2D.Raycast(m_prevPos, dir.normalized, dir.magnitude, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Player"));

        if (hit.collider != null) 
        {
            Debug.Log("Wall Hit");
            transform.position = hit.point;
        }
    }
}
