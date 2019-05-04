using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerHeroSet : MonoBehaviour
{
    NetworkTest nwt;

    GameObject player;

    PhotonView phv;
    PhotonAnimatorView pva;


    float mapWidth = 17.2f;
    float mapLong =20f;
    //Ray ray;
    RaycastHit hit;
    /// <summary>
    /// 要比相机找到的早
    /// </summary>
    private void Awake()
    {
        
       //nwt = GameObject.Find("AB").GetComponent<NetworkTest>();
        StartCoroutine(HeroBorn());
    }

    /// <summary>
    /// 检测是否能够生成英雄
    /// </summary>
    /// <returns></returns>
    bool CheckCanUse(Vector3 pos)
    {
        if (Physics.SphereCast(pos, 1f, Vector3.up, out hit))
        {
            if (hit.collider.name == "Plane")
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 随机一个位置
    /// </summary>
    Vector3 RandomPos()
    {
        float mapX = Random.Range(-mapWidth, mapWidth + 1);
        float mapY = Random.Range(-mapLong, mapLong + 1);
        return new Vector3(mapX,0, mapY);
    }
    private void OnGUI()
    {
        GUILayout.TextArea(PhotonNetwork.PlayerList.Length.ToString());
       // GUILayout.TextArea(PhotonNetwork.CurrentRoom.Name);
    }









    IEnumerator HeroBorn()
    {
        Vector3 pos;
        do
        {
            pos = RandomPos();
            yield return 0;
        } while (CheckCanUse(pos));

        //Debug.Log(SelectHero.heroName);
        //player = ObjectPool.Instance.GetObj(SelectHero.heroName);
       //player.GetComponent<RPCScrip>().phv = phv;
        
        //player.AddComponent<PhotonView>();
        //phv = player.AddComponent<PhotonView>();

        //player.GetComponent<PhotonView>().ObservedComponents.Add(GetComponent<Transform>());
        //player.GetComponent<PhotonView>().ObservedComponents.Add(GetComponent<Animator>()); GetComponent<PhotonAnimatorView>();
        //pva.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
        //pva.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
        //pva.SetParameterSynchronized("speed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
        //pva.SetParameterSynchronized("AttackMode", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
        //pva.SetParameterSynchronized("OpenFire", PhotonAnimatorView.ParameterType.Trigger, PhotonAnimatorView.SynchronizeType.Continuous);


        
        //Debug.Log(SelectHero.heroName+123);

        //zcc的networkTest脚本的生成方法
        // nwt.SpawnPlayer(SelectHero.heroName, pos);

        //photon的生成方法
        player = PhotonNetwork.Instantiate(SelectHero.heroName, pos, Quaternion.identity);
        player.tag = "Player";
        player.GetComponent<PlayerAttack>().ChangeAttackAnimator(player.GetComponent<PlayerAttack>().currentWeaponName);
        Debug.Log(SelectHero.heroName+"8888888888888888888888");
        player.GetComponent<PlayerMove>().speed = float.Parse(DBA.Instance.GetCareerAttributeByName(SelectHero.heroName.Remove(SelectHero.heroName.IndexOf('0')))[4]);
        
        if (player.GetComponent<PhotonView>().IsMine)
        {
            PlayerSelfId.playerID = player;

        }


        //player.GetComponent<RPCScrip>().SapwnPlayer(SelectHero.heroName,pos);




        //player.GetComponent<PlayerAttack>().Init();
        Camera.main.transform.root.GetComponent<CameraFollow>().player1 = player.transform ;

        phv = player.GetComponent<PhotonView>();
        pva = player.GetComponent<PhotonAnimatorView>();

        pva.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
        pva.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
        pva.SetParameterSynchronized("Speed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
        pva.SetParameterSynchronized("AttackMode", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
        pva.SetParameterSynchronized("OpenFire", PhotonAnimatorView.ParameterType.Trigger, PhotonAnimatorView.SynchronizeType.Continuous);











        //  player.AddComponent<PlayerMove>();
        //  player.AddComponent<PlayerAttack>();
        //  player.AddComponent<PlayerAnimatorAttackEvent>();
        //   player.AddComponent<Rigidbody>();
        //  Rigidbody r =  player.GetComponent<Rigidbody>();
        //   r.useGravity = false;
        //   r.freezeRotation = true;
        //   player.AddComponent<CapsuleCollider>();
        //CapsuleCollider  p= player.GetComponent<CapsuleCollider>();
        //   p.radius = 1;
        //   p.center = new Vector3(0, 1.1f, 0.25f);
        //   p.height = 2.58f;
    }




}
