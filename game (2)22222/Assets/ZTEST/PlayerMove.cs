using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{

    float hor, ver;
    Vector3 moveDir;
    Vector3 mouseTempHitPoint;
    Animator ani;

    RaycastHit hit;
    Ray ray;

    PhotonView phv;

    public float speed;

    private void Awake()
    {
        //speed = int.Parse(DBA.Instance.GetCareerAttributeByName(gameObject.name)[4]);
        ani = GetComponent<Animator>();
        hit = new RaycastHit();
        phv= GetComponent<PhotonView>();
    }


    private void FixedUpdate()
    {
        
        if(!phv.IsMine)
        {
            return;
        }
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        if (hor != 0 || ver != 0)
        {
            ani.SetFloat("Speed", Mathf.Lerp(ani.GetFloat("Speed"), 1, Time.deltaTime * 10));
            if (ani.GetFloat("Speed") >= 0.95f)
            {
                ani.SetFloat("Speed", 1);
            }
            moveDir = new Vector3(hor, 0, ver).normalized;
            transform.position += new Vector3(moveDir.x * speed * Time.deltaTime, 0, moveDir.z * speed * Time.deltaTime);
        }
        else
        {
            ani.SetFloat("Speed", Mathf.Lerp(ani.GetFloat("Speed"), 0, Time.deltaTime * 10));
            if (ani.GetFloat("Speed") <= 0.05f)
            {
                ani.SetFloat("Speed", 0);
            }
        }
        //鼠标检测
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        mouseTempHitPoint = new Vector3(hit.point.x, 0, hit.point.z);
        transform.LookAt(mouseTempHitPoint);
    }










}
