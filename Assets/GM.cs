using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public Flowchart flowchart;
    public Text _rate;
    public Text finalEarn;
    public MenuButton[] menubutton;
    public GameObject MenuNext;
    public Animator telephoneAnimator;
    public int 현재선택된메뉴코드;

    public void DayEnd()
    {
        float rate = flowchart.GetFloatVariable("Rate");
        Debug.Log(rate);

        _rate.text = rate.ToString();
        finalEarn.text = (rate * 6700).ToString();

        foreach (var i in menubutton)
        {
           i.SetMenu(rate);
        }
        
    }

    public void MenuClick(int code)
    {
        현재선택된메뉴코드 = code;
        MenuNext.GetComponent<Button>().interactable = true;
        MenuNext.GetComponentInChildren<Text>().text = code.ToString();
    }

    public void EndReview()
    {
        telephoneAnimator.SetBool("isTouch", true);
        telephoneAnimator.SetBool("EndOfTheDay", false);
        telephoneAnimator.SetBool("ChooseFood", false);
        telephoneAnimator.SetBool("TodayReview", false);
    }
}
