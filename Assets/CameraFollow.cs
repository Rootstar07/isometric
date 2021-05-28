using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public bool canCameraMove;
    Vector3 campos;

    void Update()
    {
        if (canCameraMove == false)
            return;
        float x = target.transform.position.x;
        float y = target.transform.position.y;
        campos = new Vector3(x, y, -10);

        gameObject.transform.position = campos;
    }

}
