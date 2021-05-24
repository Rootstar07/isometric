using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChecker : MonoBehaviour
{
    public GameManager gameManager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("총알2"))
        {
            Debug.Log("총알 +");

            gameManager.WallHealthCounter();
        }

    }
}
