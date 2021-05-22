using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public GameObject bulletA;
    public int n초후시작;
    public float 주기;
    public int 총알속도;

    void Start()
    {
            //InvokeRepeating("Fire", n초후시작, 주기);   
    }

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 총알속도, ForceMode2D.Impulse);
    }
}
