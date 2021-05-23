using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaStart : MonoBehaviour
{

    public Animator battleAreaAni;

    public void Start()
    {
        battleAreaAni.SetBool("IsBattle", true);
    }
}
