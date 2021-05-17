using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
        GameLoad();
        Debug.Log(questManager.CheckQuest());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuPanel.activeSelf == true)
            {
                menuPanel.SetActive(false);
            }
            else
            {
                menuPanel.SetActive(true);
            }
            
        }
    }

    // Update is called once per frame
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

    public void GameSave()
    {
        //playerPrefs로 저장할 변수를 선택하고 새로운 변수명 지정
        PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.transform.position.y);

        //playerPrefs의 세이브 함수 실행
        PlayerPrefs.Save();

        menuPanel.SetActive(false);

        Debug.Log("저장완료");
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }

        //저장된 데이터를 불러와서 새로운 변수로 지정
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");

        //데이터를 바탕으로 플레이어의 위치를 지정
        Player.transform.position = new Vector3(x, y, 0);

        menuPanel.SetActive(false);

        Debug.Log("불러오기 완료");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
