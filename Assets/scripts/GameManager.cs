using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Slider healthSlider;
    float healthRate;
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

    public bool isFight;

    public SpriteRenderer spriteRender;
    public Sprite moveSprite;
    public Sprite attackSprtie;
    public Animator moveAnimator;

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
                menuPanel.SetActive(false);
            else
                menuPanel.SetActive(true);          
        }

        if (health <= 0)
        {
            GameOver();
        }

        healthRate = (float)health / (float)maxHealth;
        UpdateHPSlider();

    }

    public void UpdateHPSlider()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, healthRate, Time.deltaTime * 10);
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

    public void IsFight(GameObject fightObj, GameObject nonFightObj)
    {

        if (isFight == false) //전투on
        {
            nonFightObj.SetActive(false);
            fightObj.SetActive(true);
            isFight = true;
            spriteRender.sprite = attackSprtie;
            moveAnimator.enabled = false;


        }
        else //전투 off
        {
            nonFightObj.SetActive(true);
            fightObj.SetActive(false);
            isFight = false;
            spriteRender.sprite = moveSprite;
            moveAnimator.enabled = true;
        }
    }

    public void GameOver()
    {
        //Destroy(Player.gameObject);
        GameObject[] battleMaps = GameObject.FindGameObjectsWithTag("전투맵");
        foreach(GameObject x in battleMaps)
        {
            x.SetActive(false);
        }

        gameOverPanel.SetActive(true);
        gameOverAni.SetBool("isGameOver", true);
        //Time.timeScale = 0;

    }

    public void GameSave()
    {
        //playerPrefs로 저장할 변수를 선택하고 새로운 변수명 지정
        PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.SetInt("HP", health);

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

        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");
        int _health = PlayerPrefs.GetInt("HP");

        //데이터를 바탕으로 플레이어의 위치를 지정
        Player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
        health = _health;

        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameOverAni.SetBool("isGameOver", false);

        Debug.Log("불러오기 완료");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void resetsavefile()
    {
        //playerPrefs로 저장할 변수를 선택하고 새로운 변수명 지정
        PlayerPrefs.SetFloat("PlayerX", 0);
        PlayerPrefs.SetFloat("PlayerY", 0);
        PlayerPrefs.SetInt("QuestId", 10);
        PlayerPrefs.SetInt("QuestActionIndex", 0);

        //playerPrefs의 세이브 함수 실행
        PlayerPrefs.Save();
        GameLoad();

        menuPanel.SetActive(false);

        Debug.Log("초기화 성공");
    }
}
