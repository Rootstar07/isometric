using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //습득가능한 최대 거리

    private bool pickupActive = false; //습득가능 여부

    private RaycastHit2D hitinfo; //레이저 충돌체 정보


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
