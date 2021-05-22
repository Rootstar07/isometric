using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objMovement : MonoBehaviour
{
    public int 이동시간;
    public string 경로;
    public int n초뒤에이동시작!;
    public float 이동주기;
    public EaseType EY;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemyMovement", n초뒤에이동시작, 이동주기);
    }

    public void EnemyMovement()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(경로), "time", 이동시간, "easetype", iTween.EaseType.easeInOutSine));
    }

}



public enum EaseType
{
    easeInSine,
    easeOutSine,
    easeInOutSine,
    easeInQuad,
    easeOutQuad,
    easeInOutQuad,
    easeInCubic,
    easeOutCubic,
    easeInOutCubic,
    easeInQuart,
    easeOutQuart,
    easeInOutQuart,
}
