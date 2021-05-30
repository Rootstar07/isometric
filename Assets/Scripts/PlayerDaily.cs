using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class PlayerDaily : MonoBehaviour
{

    float v;
    float h;
    public float speed;
    public GameManagerDaily manager;
    Vector3 dirVec;
    GameObject ScanObject;
    GameObject Teleporter;
    public GameObject[] flowCharList;
    public Flowchart forcedFlowChart;

    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }


    void Move()
    {
        if (flowCharList[0].activeSelf == true)
            return;
        if (forcedFlowChart.GetBooleanVariable("PlayerCanMove") == false)
            return;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

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

        //스캔 활성화
        if (Input.GetButtonDown("Jump") && ScanObject != null)
        {
            //매니저의 액션 함수로 스캔한 오브젝트를 전달, 이후 매니저에서 UI로 텍스트 넘겨줌
            manager.Scan(ScanObject);
        }

        if (Input.GetButtonDown("Jump") && Teleporter != null)
        {
            manager.Tele(Teleporter);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v) * speed;

        //Ray 보여주기
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));

        //스캔할 레이어 선택
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("ScanableObject"));
        RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Teleporter"));

        //스캔해서 ScanObject에 스캔된 오브젝트를 저장
        if (rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else
        {
            ScanObject = null;
        }

        if (rayHit2.collider != null)
        {
            Teleporter = rayHit2.collider.gameObject;
        }
        else
        {
            Teleporter = null;
        }
    }
}
