using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//메뉴에서 아이템 생성 및 아이템의 성질을 정해주는 스크립트입니당

//오브젝트에 붙이는게 아니라 메뉴 띄워서 그냥 생성해버림
[CreateAssetMenu(fileName ="New Item", menuName ="아이템 생성하기/새로운 아이템")] 


public class Item : ScriptableObject //오브젝트에 안붙여도 작동함
{

    public string itemName; //아이템 이름
    public Sprite itemImage; //인벤토리에 띄울 아이템의 이미지
    public GameObject itemPrefab; //인벤토리에서 월드에 아이템을 떨굴때 필요한 실체
    public ItemType itemType;

    public string weaponType; //무기유형

    public enum ItemType //아이템의 유형을 열거함
    {
        장비,
        물약,
        재료,
        기타
    }


}
