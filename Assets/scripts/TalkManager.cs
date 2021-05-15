using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] {"안녕", "이곳에 처음이구나" }); //1000번 id에 대사를 넣음
        talkData.Add(100, new string[] { "작은 탁자가 있다." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }else
        {
            return talkData[id][talkIndex];
        }
       
    }
}
