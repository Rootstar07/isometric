using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int bulletCounter;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "총알경계") //총알경계라는 태그에 닿으면 삭제
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "플레이어")
        {
            Debug.Log("맞았습니다");
            Destroy(gameObject);
        }
    }
}
