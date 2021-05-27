using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ProgressManager : MonoBehaviour
{
    public void CheckProgess()
    {
        if (ConversationManager.Instance.GetBool("test") == true)
        {
            Debug.Log("진행상황변경");
        }
    }
}
