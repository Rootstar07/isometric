using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class GameManagerDaily : MonoBehaviour
{
    public Animator talkPanel;
    public Text talkText;
    public GameObject scannedObject;
    public GameObject menuPanel;

    public GameObject Player;
    public Image PortraitImg;
    public Animator protraitAni;
    public bool isScan;
    public int talkIndex;
    public QuestManager questManager;
    public Sprite prevProtrait;
    public TalkManager talkmanager;

    private void Start()
    {
        Load();
        Debug.Log(questManager.CheckQuest());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuPanel.activeSelf == true)
                menuPanel.SetActive(false);
            else
                menuPanel.SetActive(true);
        }

    }

    public void Scan(GameObject scanObj)
    {
        scannedObject = scanObj; //player가 스캔한 오브젝트를 받음

        ObjData objdata = scannedObject.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNPC);

        talkPanel.SetBool("isShow", isScan); //판넬 활성화

    }

    void Talk(int id, bool isNpc)
    {
        //대화 데이터 설정
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkmanager.GetTalk(id + questTalkIndex, talkIndex);

        //대화 종료
        if (talkData == null)
        {
            isScan = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id)); //대화가 끝나면 퀘스트인덱스 증가
            return;
        }

        //대화 계속
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            //초상화 보여주기
            PortraitImg.sprite = talkmanager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            PortraitImg.color = new Color(1, 1, 1, 1);

            //초상화가 바뀌면 애니메이션 실행
            if (prevProtrait != PortraitImg.sprite)
            {
                protraitAni.SetTrigger("doEffect");
                prevProtrait = PortraitImg.sprite;
            }

        }
        else
        {
            talkText.text = talkData;
            PortraitImg.color = new Color(1, 1, 1, 0);
        }

        isScan = true;
        talkIndex++;
    }



    public void Save()
    {
        SaveGame.Save<Vector2>("PlayerPosition", Player.transform.position);
        SaveGame.Save<int>("QuestId", questManager.questId);
        SaveGame.Save<int>("QuestActionIndex", questManager.questActionIndex);
    }

    public void Load()
    {


        if (SaveGame.Load<Vector2>("PlayerPosition") == null)
            return;

        Player.transform.position = SaveGame.Load<Vector2>("PlayerPosition");
        questManager.questId = SaveGame.Load<int>("QuestId");
        questManager.questActionIndex = SaveGame.Load<int>("QuestActionIndex");

        menuPanel.SetActive(false);


        Debug.Log("불러오기 완료");
    }

    public void ResetData()
    {
        SaveGame.Save<Vector2>("PlayerPosition", new Vector2(0, 0));
        SaveGame.Save<int>("QuestId", 10);
        SaveGame.Save<int>("QuestActionIndex", 0);

        Load();

        menuPanel.SetActive(false);
        Debug.Log("리셋완료");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
