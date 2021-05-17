using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] {"안녕", "이곳에 처음이구나" }); //1000번 id에 대사를 넣음
        talkData.Add(2000, new string[] { "절대로 이곳에서 잠들면 안돼:0", "명심해!:1" });
        talkData.Add(100, new string[] { "작은 탁자가 있다." });

        //표정생성
        portraitData.Add(2000 + 0, portraitArr[0]);
        portraitData.Add(2000 + 1, portraitArr[1]);
        portraitData.Add(2000 + 2, portraitArr[2]);
        portraitData.Add(2000 + 3, portraitArr[3]);

        //퀘스트 대화
        //10번대 퀘스트 진행
        talkData.Add(10 + 2000, new string[] { "다행히 멀쩡한거 같네:0", "여기가 루도의 꿈속이구나:1", "생각이상으로 이상한걸 미호가 멀쩡한지 확인해줄래?:2" });
        talkData.Add(11 + 1000, new string[] { "괜찮아?" });
        talkData.Add(12 + 2000, new string[] { "다시 왔네:0" });

        talkData.Add(20 + 2000, new string[] { "무슨일이야?:0", "그래!:3" });
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

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
