using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleStart : MonoBehaviour
{
    public GameObject nonFightObj;
    public GameObject fightObj;
    public GameManager gameManager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "플레이어")
        {
            gameManager.IsFight(fightObj, nonFightObj);
        }
    }
}
