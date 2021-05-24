using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChecker : MonoBehaviour
{
    public GameManager gameManager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("총알")) //총알경계라는 태그에 닿으면 삭제
        {
            Debug.Log("총알 +");

            gameManager.WallHealthCounter();
        }

    }
}
