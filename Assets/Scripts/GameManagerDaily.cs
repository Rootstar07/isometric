using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using Fungus;

public class GameManagerDaily : MonoBehaviour
{
    public GameObject scannedObject;
    public GameObject menuPanel;
    public GameObject Player;
    public GameObject camera;
    public bool isScan;

    public GameObject flowchartObject;
    public Flowchart flowchart;
    public Flowchart forcedflowchart;
    public bool playerCanMove = true;

    private void Start()
    {
        Load();
        //ConversationManager.Instance.StartConversation(conversation);
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
        flowchartObject.SetActive(true);

        flowchart.SetFloatVariable("ID", scanObj.GetComponentInChildren<FungusData>().ID);


    }

    public void Tele(GameObject teleport)
    {
        float camX = teleport.GetComponentInChildren<Teledata>().camPos.transform.position.x;
        float camY = teleport.GetComponentInChildren<Teledata>().camPos.transform.position.y;

        Player.transform.position = teleport.GetComponentInChildren<Teledata>().toWhere.transform.position;
        camera.transform.position = new Vector3(camX, camY, -10);

        camera.GetComponentInChildren<Camera>().orthographicSize = teleport.GetComponentInChildren<Teledata>().camSize;

    }

    public void Save()
    {
        SaveGame.Save<Vector2>("PlayerPosition", Player.transform.position);

        //대화관련 저장, 퀘스트int는 생길때마다 추가할것
        SaveGame.Save<int>("침대 퀘스트 int", flowchart.GetIntegerVariable("침대퀘스트int"));
        SaveGame.Save<bool>("플레이어이동가능여부", forcedflowchart.GetBooleanVariable("PlayerCanMove"));
        SaveGame.Save<int>("스토리진행번호", forcedflowchart.GetIntegerVariable("StoryNum"));

        menuPanel.SetActive(false);

        Debug.Log("저장하기 완료");
    }

    public void Load()
    {
        if (SaveGame.Load<Vector2>("PlayerPosition") == null)
            return;

        Player.transform.position = SaveGame.Load<Vector2>("PlayerPosition");
        forcedflowchart.SetIntegerVariable("StoryNum", SaveGame.Load<int>("스토리진행번호")); //스토리번호 로드
        forcedflowchart.SetBooleanVariable("PlayerCanMove", SaveGame.Load<bool>("플레이어이동가능여부"));
        flowchart.SetIntegerVariable("침대퀘스트int", SaveGame.Load<int>("침대 퀘스트 int"));

        menuPanel.SetActive(false);

        Debug.Log("불러오기 완료");
    }

    public void ResetData()
    {
        SaveGame.Save<Vector2>("PlayerPosition", new Vector2(0, 0));

        Load();
        forcedflowchart.SetIntegerVariable("StoryNum", 0);
        forcedflowchart.SetBooleanVariable("PlayerCanMove", false);
        flowchart.SetIntegerVariable("침대퀘스트int", 0);

        menuPanel.SetActive(false);
        Debug.Log("리셋완료");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
