using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class InputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ConversationManager.Instance.SelectNextOption();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                ConversationManager.Instance.SelectPreviousOption();
            if (Input.GetKeyDown(KeyCode.LeftControl))
                ConversationManager.Instance.PressSelectedOption();
        }
    }

}
