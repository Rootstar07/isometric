using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDaily : MonoBehaviour
{
    float v;
    float h;
    public float speed;
    public GameManagerDaily manager;
    public ProgressManager progressManager;
    Vector3 dirVec;
    GameObject ScanObject;

    Rigidbody2D rigid;
    Animator anim;
    public GameObject isTalking;

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
        if (manager.stopMove == true)
            return;
           
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //h = manager.stopMove ? 0 : Input.GetAxisRaw("Horizontal");
        //v = manager.stopMove ? 0 : Input.GetAxisRaw("Vertical");

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
            manager.Scan(ScanObject);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v) * speed;

        //Ray 보여주기
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));

        //스캔할 레이어 선택
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("ScanableObject"));

        //스캔해서 ScanObject에 스캔된 오브젝트를 저장
        if (rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else
        {
            ScanObject = null;
        }
    }
}
