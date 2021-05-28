using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teledata : MonoBehaviour
{
    public GameObject toWhere;
    public GameObject camPos;
    public GameObject helper;

    public int camSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        helper.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        helper.SetActive(false);
    }
}
