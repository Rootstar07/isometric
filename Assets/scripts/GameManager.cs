using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class GameManager : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Text enemyHPText;
    public Text enemyMaxHPText;
    public Slider healthSlider;
    public Animator talkPanel;
    public Text talkText;
    public GameObject scannedObject;
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public Animator gameOverAni;
    public GameObject Player;
    public Image PortraitImg;
    public Animator protraitAni;
    public bool isScan;
    public int talkIndex;
    public QuestManager questManager;
    public Sprite prevProtrait;
    public TalkManager talkmanager;
    public StageManager stagemanager;
    public GameObject battleArea;
    
    public GameObject mainCamera;
    public GameObject battleUI;

    public SpriteRenderer spriteRender;
    public Sprite moveSprite;
    public Sprite attackSprtie;
    public Animator moveAnimator;

    string maxWallHealth;
    int wallHitCount;
    GameObject initiator;
    int stageNum;
    bool isFight;
    GameObject battleAreaClone;

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
        if (health <= 0)
            GameOver();

        UpdateHPSlider((float)health / (float)maxHealth);
    }

    public void UpdateHPSlider(float x)
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, x, Time.deltaTime * 10);
    }

    
    public void PlayerHitted()
    {
        health = health - 1;
    }

    public void Scan(GameObject scanObj)
    {
        scannedObject = scanObj; //player가 스캔한 오브젝트를 받음

        ObjData objdata = scannedObject.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNPC);

        //talkPanel.SetBool("isShow", isScan); //판넬 활성화

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

    public void ChangeMap(int x, bool y, GameObject z) //x는 전투 스테이지 번호, y는 inout 여부
    {
        stageNum = x;
        isFight = y;
        initiator = z;

        if (y == true) //전투 시작
        {
            battleUI.SetActive(true);
            stagemanager.basicStage.SetActive(false); //기존 영역 가리기
            stagemanager.fightStage[x].SetActive(true); //해당 지역 오브젝트 활성화
            maxWallHealth = stagemanager.fightStage[x].GetComponentInChildren<BattleInfo>().maxEnemyHp;
            enemyMaxHPText.text = maxWallHealth;
            WallHealth(stagemanager.fightStage[x].GetComponentInChildren<BattleInfo>().enemyHP);
            Fight(x, y, z);

            mainCamera.GetComponentInChildren<FollowPlayer>().canCameraMoving = false; //카메라 고정
            mainCamera.GetComponentInChildren<FollowPlayer>().CameraStop(z);

            spriteRender.sprite = attackSprtie; //플레이어 스프라이트 변경
            moveAnimator.enabled = false; //플레이어 애니메이터 비활성화
        }
        else //일상으로
        {
            stagemanager.clearStage[x] = true;
            battleUI.SetActive(false);
            stagemanager.basicStage.SetActive(true);
            stagemanager.fightStage[x].SetActive(false);
            mainCamera.GetComponentInChildren<FollowPlayer>().canCameraMoving = true;
            battleAreaClone.GetComponentInChildren<BattleAreaStart>().Destory();

            spriteRender.sprite = moveSprite;
            moveAnimator.enabled = true;
        }
    }


    public void Fight(int x, bool y, GameObject z)
    {
        battleAreaClone = Instantiate(battleArea, z.transform.position, z.transform.rotation); //전투영역 생성
        Debug.Log("스테이지: " + x);
    }

    public void WallHealth(string cur) //현재체력표시
    {
        int currentWallHp = int.Parse(maxWallHealth) - int.Parse(cur);
        enemyHPText.text = currentWallHp.ToString();
        if(currentWallHp < 0)
        {
            initiator.GetComponentInChildren<battleStart>().isStart = false;
            initiator.SetActive(true);
        }
    }

    public void WallHealthCounter() //총알인스턴스에서 데이터를 받아서 WallHealth에 전달
    {
        wallHitCount++;
        Debug.Log(wallHitCount);
        WallHealth(wallHitCount.ToString());
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverAni.SetBool("isGameOver", true);
        //Time.timeScale = 0;
    }

    public void Save()
    {
        SaveGame.Save<Vector2>("PlayerPosition", Player.transform.position);
        SaveGame.Save<int>("QuestId", questManager.questId);
        SaveGame.Save<int>("QuestActionIndex", questManager.questActionIndex);
        SaveGame.Save<int>("HP", health);

        //스테이지 매니저의 Bool 배열값을 문자열로 변환하여 저장
        string strArr = "";
        for (int i = 0; i < stagemanager.clearStage.Length; i++)
        {
            strArr = strArr + stagemanager.clearStage[i];
            if (i < stagemanager.clearStage.Length - 1)
            {
                strArr = strArr + ",";
            }
        }
        SaveGame.Save<string>("clearStageStringList", strArr);

        menuPanel.SetActive(false);
        Debug.Log("저장완료");
    }

    public void Load()
    {
        //문자열을 Split을 이용해서 나눈뒤 Bool 배열로 변환
        string[] clearStageStringListData = SaveGame.Load<string>("clearStageStringList").Split(',');
        bool[] clearStageList = new bool[clearStageStringListData.Length];

        for (int i = 0; i < clearStageList.Length; i++)
        {
            clearStageList[i] = System.Convert.ToBoolean(clearStageStringListData[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
        }

        stagemanager.clearStage = clearStageList; //저장데이터와 스테이지 매니저 동기화

        //Bool 배열의 값에 따라 이니시에이터의 생성여부 결정
        for (int i = 0; i < stagemanager.initiatorList.Length; i++)
        {
            if (clearStageList[i] == false)
                stagemanager.initiatorList[i].SetActive(true);
            else
                stagemanager.initiatorList[i].SetActive(false);
        }

        if (SaveGame.Load<Vector2>("PlayerPosition") == null)
            return;

        Player.transform.position = SaveGame.Load<Vector2>("PlayerPosition");
        questManager.questId = SaveGame.Load<int>("QuestId");
        questManager.questActionIndex = SaveGame.Load<int>("QuestActionIndex");
        health = SaveGame.Load<int>("HP");

        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameOverAni.SetBool("isGameOver", false);
        mainCamera.GetComponentInChildren<FollowPlayer>().canCameraMoving = true;

        //전투 중 불러오기
        if (battleAreaClone != null)
        {
            battleUI.SetActive(false);
            stagemanager.basicStage.SetActive(true);
            battleAreaClone.GetComponentInChildren<BattleAreaStart>().Destory();
            spriteRender.sprite = moveSprite;
            moveAnimator.enabled = true;
        }

        Debug.Log("불러오기 완료");
    }

    public void ResetData()
    {
        SaveGame.Save<Vector2>("PlayerPosition", new Vector2(0, 0));
        SaveGame.Save<int>("QuestId", 10);
        SaveGame.Save<int>("QuestActionIndex", 0);
        SaveGame.Save<int>("HP", 10);

        Load();

        menuPanel.SetActive(false);
        Debug.Log("리셋완료");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
