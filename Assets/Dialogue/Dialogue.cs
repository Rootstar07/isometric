using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] //DialogueEvent에서 이걸 받아오는데 수정하기 위해 직렬화가 필요
public class Dialogue
{
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용 (배열)")]
    public string[] contexts;

}

[System.Serializable]
public class DialogueEvent
{
    public string name; //이벤트의 이름

    public Vector2 line;
    public Dialogue[] dialogues;
}
