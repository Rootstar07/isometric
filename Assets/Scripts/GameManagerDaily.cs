using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using VIDE_Data;

public class GameManagerDaily : MonoBehaviour
{
    public GameObject scannedObject;
    public GameObject menuPanel;
    public GameObject Player;
    public bool isScan;
    public GameObject nodePanel;
    public bool stopMove = false;
    public Template_UIManager template_UIManager;
    public VIDE_Assign vide_Assign;

    public int nowNode;

    private void Start()
    {
        Load();
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
        stopMove = true;
        scannedObject = scanObj; //player가 스캔한 오브젝트를 받음
        int nowNode = scannedObject.GetComponentInChildren<NodeData>().node;


        template_UIManager.Interact(vide_Assign);
        VD.SetNode(nowNode);
        //nodePanel.SetActive(true);
        //노드를 지정하면 uimanager에서 nextnode를 계속 돌림, 끝나면 거기서 nodepanel을 비활성화
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
