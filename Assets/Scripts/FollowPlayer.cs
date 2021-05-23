using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public bool canCameraMoving;

    Vector3 cameraPosition;

    void LateUpdate()
    {
        if (canCameraMoving == true)
            CameraMoveing();
    }

    public void CameraMoveing()
    {
        cameraPosition.x = player.transform.position.x;
        cameraPosition.y = player.transform.position.y;
        cameraPosition.z = -10;

        transform.position = cameraPosition;
    }

    public void CameraStop(GameObject x)
    {
        cameraPosition.x = x.transform.position.x;
        cameraPosition.y = x.transform.position.y;
        cameraPosition.z = -10;

        transform.position = cameraPosition;
    }
}
