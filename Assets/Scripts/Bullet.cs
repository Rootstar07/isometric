using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("플레이어"))
        {
            Destroy(gameObject);
        }
    }
}
