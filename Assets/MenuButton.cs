using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public float foodPrice;

    public void SetMenu(float rate)
    {
        if (rate * 6700 < foodPrice)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

}
