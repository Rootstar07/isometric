using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogue()
    {
        dialogue.dialogues = DataBaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        return dialogue.dialogues;
    }

}
