using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float v;
    float h;
    public float speed;
    public GameManager manager;
    Vector3 dirVec;
    GameObject ScanObject;
    GameObject ScanItem;

    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        getPlayerDir();

        CheckScan();
        CheckItem();
    }

    private void playerMove()
    {
        //매니저에서 대화 처리를 하고 있을때는 이동불가
        h = manager.isScan ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isScan ? 0 : Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            anim.SetFloat("hAxisRaw", h);
            anim.SetFloat("vAxisRaw", v);
            anim.SetBool("IsWalk", true);
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }

    }

    private void getPlayerDir()
    {
        //보는 방향값 얻기
        if (anim.GetFloat("vAxisRaw") == 1)
        {
            dirVec = Vector3.up;
            //print("0,1");
        }
        else if (anim.GetFloat("vAxisRaw") == -1)
        {
            dirVec = Vector3.down;
            //print("0,-1");
        }
        else if (anim.GetFloat("hAxisRaw") == -1)
        {
            dirVec = Vector3.left;
            //print("-1,0");
        }
        else if (anim.GetFloat("hAxisRaw") == 1)
        {
            dirVec = Vector3.right;
            //print("1,0");
        }
    }

    private void CheckScan()
    {

        //스캔 활성화
        if (Input.GetButtonDown("Jump") && ScanObject != null)
        {
            //매니저의 액션 함수로 스캔한 오브젝트를 전달, 이후 매니저에서 UI로 텍스트 넘겨줌
            manager.Scan(ScanObject);
        }
    }

    private void CheckItem()
    {
        //획득할수있는 아이템이 있으면 획득하겠냐고 묻는 ui가 뜸
        if (Input.GetButtonDown("Jump") && ScanItem != null)
        {
            manager.openItemPanel(ScanItem);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v) * speed;

        //Ray 보여주기
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));

        //스캔할 레이어 선택
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("ScanableObject"));
        RaycastHit2D rayHit_item = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Item"));

        //스캔해서 ScanObject에 스캔된 오브젝트를 저장
        if (rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else
        {
            ScanObject = null;
        }

        //아이템을 스캔하면 ScanItem에 해당 오브젝트를 저장
        if (rayHit_item.collider != null)
        {
            ScanItem = rayHit_item.collider.gameObject;
        }
        else
        {
            ScanItem = null;
        }
    }
}
