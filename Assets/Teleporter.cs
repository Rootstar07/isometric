using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject[] ways;
    public GameObject[] camPoint;
    public GameObject player;
    public GameObject camera;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("플레이어"))
        {

        }

    }
}
