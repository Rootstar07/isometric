using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using DialogueEditor;

public class GameManagerDaily : MonoBehaviour
{
    public GameObject scannedObject;
    public GameObject menuPanel;
    public GameObject Player;
    public bool isScan;

    public NPCConversation conversation;

    private void Start()
    {
        Load();
        ConversationManager.Instance.StartConversation(conversation);
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

        if (ConversationManager.Instance != null)
             {
             if (ConversationManager.Instance.IsConversationActive)
                 {
                 if (Input.GetKeyDown(KeyCode.UpArrow))
                     ConversationManager.Instance.SelectPreviousOption();
                
                 else if (Input.GetKeyDown(KeyCode.DownArrow))
                     ConversationManager.Instance.SelectNextOption();
                
                 else if (Input.GetKeyDown(KeyCode.Space))
                     ConversationManager.Instance.PressSelectedOption();
                 }
             }

    }

    public void Scan(GameObject scanObj)
    {
        scannedObject = scanObj; //player가 스캔한 오브젝트를 받음

        ObjData objdata = scannedObject.GetComponent<ObjData>();

        ConversationManager.Instance.StartConversation(scanObj.GetComponentInChildren<NPCConversation>());

        //Talk(objdata.id, objdata.isNPC);

        //talkPanel.SetBool("isShow", isScan); //판넬 활성화

    }

    public void Save()
    {
        SaveGame.Save<Vector2>("PlayerPosition", Player.transform.position);

        menuPanel.SetActive(false);

        Debug.Log("저장하기 완료");
    }

    public void Load()
    {
        if (SaveGame.Load<Vector2>("PlayerPosition") == null)
            return;

        Player.transform.position = SaveGame.Load<Vector2>("PlayerPosition");

        menuPanel.SetActive(false);

        Debug.Log("불러오기 완료");
    }

    public void ResetData()
    {
        SaveGame.Save<Vector2>("PlayerPosition", new Vector2(0, 0));

        Load();

        menuPanel.SetActive(false);
        Debug.Log("리셋완료");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
