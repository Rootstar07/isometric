using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor; //이거 쓸래

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scannedObject;
    public GameObject menuPanel;
    public GameObject Player;
    public bool isScan;

    public NPCConversation testConversation; //무슨 대화 출력할건지 변수

    private void Start()
    {
        GameLoad();
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
        if (isScan == true)
        {
            isScan = false;
            //talkPanel.SetActive(false);
        }
        else
        {
            isScan = true;

            //player가 스캔한 오브젝트를 받음
            scannedObject = scanObj;

            //판넬 활성화
            //talkPanel.SetActive(true);

            //public을 통해 선택한 text를 위의 오브젝트의 이름 값을 가져와서 출력
            //talkText.text = "이건 " + scanObj.name + "이다.";

            ConversationManager.Instance.StartConversation(testConversation);

        }

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
