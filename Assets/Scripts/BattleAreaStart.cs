using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaStart : MonoBehaviour
{

    public Animator battleAreaAni;
    public GameObject collider;

    public void Start()
    {
        battleAreaAni.SetBool("IsBattle", true);
        Invoke("Collider", 1f);
    }

        public void Collider()
    {
        collider.SetActive(true);
    }

    public void Destory()
    {
        collider.SetActive(false);
        Destroy(gameObject, 1f);
        battleAreaAni.SetBool("IsBattle", false);
    }
}
