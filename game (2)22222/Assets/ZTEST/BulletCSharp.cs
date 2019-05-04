using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于控制子弹的脚本
/// </summary>
public class BulletCSharp : MonoBehaviour
{
    BulletClassType bulletClassType;



    Transform player;
    Vector3 dir;
    Rigidbody rig;
    float arrowSpeed = 17;
    float bulletSpeed = 30;
    float magicBallSpeed = 13;
    float axeSpeed = 15f;
    float saiSpeed = 15f;
    float clubSpeed = 15f;
    //-----------------
    float arrowTime = 6;
    float bulletTime = 10;
    float magicBallTime = 5;
    float axeTime = .4f;
    float saiTime = .6f;
    float clubTime =.4f;
    bool isFire = false;
    /// <summary>
    /// 外部调用的开始方法
    /// </summary>
    /// <param name="t">传入玩家</param>
    public void BAwake(Transform t)
    {
        player = t;
        dir = new Vector3(player.forward.x, 0, player.forward.z).normalized;
        transform.forward = dir;
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        rig = GetComponent<Rigidbody>();
        rig.useGravity = false;
        transform.position = new Vector3(player.position.x, player.position.y + 0.5f, player.position.z + 0.5f);

        IsFire();
        StartCoroutine(Rec());

        bulletClassType = GetComponent<BulletClassType>();


    }
    /// <summary>
    /// 是否要执行飞行
    /// </summary>
    public void IsFire()
    {
        isFire = true;
    }

    private void OnEnable()
    {
        if (GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>().isTrigger = true;
        }
        if (GetComponent<BoxCollider>().enabled == false)
            GetComponent<BoxCollider>().enabled = true;

    }
    public void FixedUpdate()
    {


        if (!isFire)
        {
            return;
        }

        if (bulletClassType.type == BulletType.Arrow)
        {
           StartCoroutine( Stop(arrowTime));
            Arrow();
        }
        if (bulletClassType.type == BulletType.Axe)
        {
           StartCoroutine( Stop(axeTime));
            Axe();
        }
        if (bulletClassType.type == BulletType.Bullet)
        {
          StartCoroutine(  Stop(bulletTime));
            Bullet();
        }
        if (bulletClassType.type == BulletType.Club)
        {
          StartCoroutine(  Stop(clubTime));
            Club();
        }
        if (bulletClassType.type == BulletType.KnifeLight)
        {
            KnifeLight();

        }
        if (bulletClassType.type == BulletType.MagicBall)
        {
            StartCoroutine(Stop(magicBallTime));
            MagicBall();
        }
        if (bulletClassType.type == BulletType.Sai)
        {
            StartCoroutine(Stop(saiTime));
            Sai();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {

            dir = Vector3.zero;
        }

    }

    /// <summary>
    /// 回收
    /// </summary>
    /// <returns></returns>
    IEnumerator Rec()
    {
        float time= 40f;
        if(gameObject.name== "prefab_knifelight")
        {
            time = 0.3f;
        }

        yield return new WaitForSecondsRealtime(time);
        ObjectPool.Instance.RecyleObj(gameObject);
    }

    /// <summary>
    ///  飞行时间
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Stop(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        dir = Vector3.zero;
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        GetComponent<BoxCollider>().enabled = false;
    }






    /// <summary>
    /// 以下是各种武器的执行方法
    /// </summary>
    public void Arrow()
    {
        rig.velocity = dir * arrowSpeed;
    }
    public void Bullet()
    {
        rig.velocity = dir * bulletSpeed;
    }
    public void MagicBall()
    {
        rig.velocity = dir * magicBallSpeed;
    }
    public void Axe()
    {

        if (rig.velocity != Vector3.zero)
        {
            //transform.Rotate(Vector3.up, -20f);
            transform.localEulerAngles += Vector3.up * -30f;

        }
        rig.velocity = dir * axeSpeed;
    }
    public void Sai()
    {

        if (rig.velocity != Vector3.zero)
        {
            transform.Rotate(Vector3.up, -40f);
        }
        rig.velocity = dir * saiSpeed;
    }
    public void Club()
    {
        if (rig.velocity != Vector3.zero)
        {
            transform.Rotate(Vector3.up, -20f);
        }
        rig.velocity = dir * clubSpeed;
    }
    public void KnifeLight()
    {
        if (player == null)
        {
            return;
        }
        transform.position = player.position;
        transform.localEulerAngles = new Vector3(player.eulerAngles.x, player.eulerAngles.y-90  , player.eulerAngles.z);
    }












}
