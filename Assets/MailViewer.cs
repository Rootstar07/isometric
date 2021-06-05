using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailViewer : MonoBehaviour
{
    public GameObject message;
    public Text title1;
    public Text title2;
    public Text sender;
    public Text content1;
    public Text content2;
    public Text mailtitle;
    public Text mailsender;

    [TextArea]
    public string 내용;

    public void ToggleMessage()
    {

        message.SetActive(true);
        title1.text = title2.text = mailtitle.text;
        sender.text = mailsender.text;
        content1.text = content2.text = 내용;
   
    }
}
