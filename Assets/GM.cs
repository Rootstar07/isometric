using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GM : MonoBehaviour
{
    public Flowchart flowchart;

    public void Test()
    {
        float rate = flowchart.GetFloatVariable("Rate");
        Debug.Log(rate);
    }
}
