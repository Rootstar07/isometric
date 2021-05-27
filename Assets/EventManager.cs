using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject[] gameobject;

    public void Setactive(int x)
    {
        gameobject[x].SetActive(true);
    }
}
