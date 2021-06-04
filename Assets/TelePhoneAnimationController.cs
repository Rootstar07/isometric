using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePhoneAnimationController : MonoBehaviour
{
    public Animator myAnimator;

    public void ToggleBool(string name)
    {
        //myAnimator.SetBool(name, !myAnimator.GetBool(name));

        if (myAnimator.GetBool("isTouch") == true && myAnimator.GetBool("isTextUp") == true)
            //둘다 열린상태에서 닫으려고 하면 텍스트만 닫히게
            myAnimator.SetBool("isTextUp", false);
        else if(myAnimator.GetBool("isTouch") == true && myAnimator.GetBool("isNewsUp") == true)
            myAnimator.SetBool("isNewsUp", false);
        else
            myAnimator.SetBool(name, !myAnimator.GetBool(name));

    }
}
