using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleStart : MonoBehaviour
{
    public GameManager gameManager;

    public int stageNum;
    public bool isStart;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "플레이어")
        {
            gameManager.ChangeMap(stageNum, isStart, gameObject);
            gameObject.SetActive(false);
        }
    }

}
