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
            gameManager.IsFight(fightObj, nonFightObj); //플레이가 영역에 들어가면 전투맵은 보이고 비전투맵은 비활성화, 반대로 가능
        }
    }
}
