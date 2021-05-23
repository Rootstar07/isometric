using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public bool canCameraMoving;

    Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canCameraMoving == true)
            CameraMoveing();
        else
            CameraStop();

    }

    public void CameraMoveing()
    {
        cameraPosition.x = player.transform.position.x;
        cameraPosition.y = player.transform.position.y;
        cameraPosition.z = -10;

        transform.position = cameraPosition;
    }

    public void CameraStop()
    {
        transform.position = transform.position;
    }
}
