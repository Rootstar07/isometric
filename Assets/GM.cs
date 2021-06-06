using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public Flowchart flowchart;
    public Text _rate;
    public Text finalEarn; //순수입
    public MenuButton[] menubutton;
    public GameObject MenuNext;
    public Animator telephoneAnimator;
    public MenuButton 눌린버튼;
    public GameObject telePhone;

    //정산
    public Text 이자;
    public float 이자율 = 0.12f;
    public Text 세금;
    public float 세율 = 0.1f;
    public Text 저축;
    public float 저축율 = 0.1f;
    public float money; //현재 돈
    public Text 최종값;

    //홈화면
    public Text myMoney;

    //foodchoose 돈 최종
    public Text foodChooseFinalMoney;

    //받은 별점
    public float rate;



    public void DayEnd()
    {
        rate = flowchart.GetFloatVariable("Rate"); //받은 별점 GM에 저장

        //위의 별점을 바탕으로 번 돈을 핸드폰에 표시
        _rate.text = rate.ToString();
        finalEarn.text = (rate * 6700).ToString();

        money = rate * 6700 + money;//돈에 번 돈 추가, 대화가 끝났을때에만 기능함

        if (telePhone.GetComponent<Animator>().GetBool("isTouch") == false) //폰이 꺼져있으면
        {
            telePhone.GetComponent<Animator>().SetBool("isTouch", true); //폰 열어
        }

        telePhone.GetComponent<TelePhoneAnimationController>().ToggleBool("EndOfTheDay"); //정산화면 연다.


        //가능한 메뉴 표시
        foreach (var i in menubutton)
        {
           i.SetMenu(rate, money);
        }
        
    }

    public void MenuClick(MenuButton x)
    {
        MenuNext.GetComponent<Button>().interactable = true;
        눌린버튼 = x;

        MenuNext.GetComponentInChildren<Text>().text = x.foodName + ": " + x.foodPrice;
    }

    public void EndReview()
    {
        telephoneAnimator.SetBool("isTouch", true);
        telephoneAnimator.SetBool("EndOfTheDay", false);
        telephoneAnimator.SetBool("ChooseFood", false);
        telephoneAnimator.SetBool("TodayReview", false);
    }

    public void Calculator()
    {
        money = money - 눌린버튼.foodPrice; //음식값계산

        이자.text = (money * 이자율).ToString();
        money = money - (money * 이자율); //이자빼고

        세금.text = (money * 세율).ToString();
        money = money - (money * 세율);

        저축.text = (money * 저축율).ToString();
        money = money - (money * 저축율);


        myMoney.text = 최종값.text = money.ToString();
    }

    public void GetMoney()
    {
        foodChooseFinalMoney.text = money.ToString();
    }
}
