using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int questId;
    public int questActionIndex; //대화순서
    public GameObject[] questobject;


    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문", new int[] { 2000, 1000, 2000 })); //퀘스트 번호, 퀘스트를 줄 npc 순서 필수!

        questList.Add(20, new QuestData("대화하기", new int[] { 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        ControlObject();

        if (id == questList[questId].npcId[questActionIndex]) //퀘스트에 맞는 대화를 했을때만 인덱스 증가
        {
            questActionIndex++;
        }

        //대화가 끝나면 다음 퀘스트 실행
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NexQuest();
        }
        return questList[questId].questName;
    }

    void NexQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2) //10번 퀘스트의 2번 대화가 끝나면 오브젝트[0] 활성화
                    questobject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questobject[0].SetActive(false);
                break;
        }
    }
}
