using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public Animator dialogAnimator;
    public GameObject 조건;

    public void Update()
    {
        if (조건.activeSelf == false)
        {
            dialogAnimator.SetBool("isMenu", true);
        }else
        {
            dialogAnimator.SetBool("isMenu", false);
        }
    }


}
