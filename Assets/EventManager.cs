using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class EventManager : MonoBehaviour
{
    public int nowNode;
    public Flowchart flowchart;

    private void LateUpdate()
    {
        //어디서든 NowProgress값이 변하면 강제이벤트 한번 실행
        if (flowchart.GetIntegerVariable("StoryNum") == 0)
        {
            Fungus.Flowchart.BroadcastFungusMessage("강제이벤트1");
            flowchart.SetIntegerVariable("StoryNum", 1);
        }

    }
}
