using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class PlayerAttack : MonoBehaviour, IPunObservable
{

    //private static PlayerAttack instance;
    //public static PlayerAttack Instance
    //{
    //get
    //{
    //if (instance == null) instance = new PlayerAttack();
    //return instance;
    //}
    //}
        GameObject gobj;


    public string currentWeaponName;

    public int WeaponType;
    //两只手
    HandOfPlayer[] handOfPlayers;

    Animator ani;

    PhotonView phv;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ani = GetComponent<Animator>();

        phv = GetComponent<PhotonView>();


        currentWeaponName = transform.GetComponentInChildren<WeaponAtrribute>().WeaponName;
        WeaponType = DBA.Instance.GetWeaponTypeByName(currentWeaponName);



    }
/// <summary>
/// 初始化
/// </summary>
public void Init()
    {
        // GameObject temp;

    //    ChangeAttackAnimator(currentWeaponName);



        handOfPlayers = transform.GetComponentsInChildren<HandOfPlayer>();

        for (int i = 0; i < handOfPlayers.Length; i++)
        {
            //返回值为当前武器的gameobject
            GameObject go = handOfPlayers[i].GetCurrentWeapon();

            if (go != null)
            {
                go.name.Replace("(Clone)", "");
                currentWeaponName = go.name;


                
                ChangeAttackAnimator(currentWeaponName);
              //  GetNewWeapon(go.name);
            }
        }

    }
    /// <summary>
    /// 改变动画
    /// </summary>
    public void ChangeAttackAnimator(string curWeapon)
    {
        WeaponType = DBA.Instance.GetWeaponTypeByName(curWeapon);
        ani.SetInteger(ConstLyb.Animator_AttackMode, WeaponType);
        SetPhotonAnimatorView();
    }

    private void FixedUpdate()
    {
        if (phv.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ani.SetTrigger(ConstLyb.Animator_OpenFire);
            }
        }
    }


    private void OnGUI()
    {
        if (!phv.IsMine)
        {
            return;
        }
        //忍者
        if((gameObject.name.ToLower()).Contains("ninja"))
        {
            if (GUILayout.Button("十手"))
            {
                GetNewWeapon("prefab_sai");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("武士刀"))
            {
                GetNewWeapon("prefab_katana");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }

        }
        //武士
        if((gameObject.name.ToLower()).Contains("samurai"))
        {
            if (GUILayout.Button("武士刀"))
            {
                GetNewWeapon("prefab_katana");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("手枪"))
            {
                GetNewWeapon("prefab_musketpistol");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }

        }
        //海盗
        if ((gameObject.name.ToLower()).Contains("pirate") )
        {
            if (GUILayout.Button("飞斧"))
            {
                GetNewWeapon("prefab_axe");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);
            }
            if (GUILayout.Button("手枪"))
            {
                GetNewWeapon("prefab_musketpistol");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("弯刀"))
            {
                GetNewWeapon("prefab_saber");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }

        }
        //维京人
        if ((gameObject.name.ToLower()).Contains("viking"))
        {
            if (GUILayout.Button("锤子"))
            {
                GetNewWeapon("prefab_hammer");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);
            }
            if (GUILayout.Button("飞斧"))
            {
                GetNewWeapon("prefab_axe");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);
            }


        }
        //牛仔
        if ((gameObject.name.ToLower()).Contains("cowboy"))
        {
            if (GUILayout.Button("左轮"))
            {
                GetNewWeapon("prefab_revolver");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("飞斧"))
            {
                GetNewWeapon("prefab_axe");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);
            }


        }
        //兽人
        if ((gameObject.name.ToLower()).Contains("orcish"))
        {
            if (GUILayout.Button("狼牙棒"))
            {
                GetNewWeapon("prefab_club");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("大斧子"))
            {
                GetNewWeapon("prefab_boneaxe");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }

        }
        //巫师
        if ((gameObject.name.ToLower()).Contains("witch"))
        {
            if (GUILayout.Button("法杖"))
            {
                GetNewWeapon("prefab_staff");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("自然法杖"))
            {
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

                GetNewWeapon("prefab_naturestaff");
            }

            if (GUILayout.Button("剑"))
            {
                GetNewWeapon("prefab_sword");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }

        }
        //骑士
        if ((gameObject.name.ToLower()).Contains("knight"))
        {
            if (GUILayout.Button("剑"))
            {
                GetNewWeapon("prefab_sword");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("寒冰剑"))
            {
                GetNewWeapon("prefab_crystalsword");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("水晶剑"))
            {
                GetNewWeapon("prefab_crystalhalberd");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("双刃剑"))
            {
                GetNewWeapon("prefab_crystalsworddouble");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }


        }
        //游侠
        if ((gameObject.name.ToLower()).Contains("archer"))
        {
            if (GUILayout.Button("弓箭"))
            {
                GetNewWeapon("prefab_bow"); GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }
            if (GUILayout.Button("鲁特琴"))
            {
                GetNewWeapon("prefab_lute");
                GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

            }

        }
        //if (GUILayout.Button("弓箭"))
        //{
        //    GetNewWeapon("prefab_bow"); GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("法杖"))
        //{
        //    GetNewWeapon("prefab_staff");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("剑"))
        //{
        //    GetNewWeapon("prefab_sword");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("大斧子"))
        //{
        //    GetNewWeapon("prefab_boneaxe");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("狼牙棒"))
        //{
        //    GetNewWeapon("prefab_club");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("鲁特琴"))
        //{
        //    GetNewWeapon("prefab_lute");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("自然法杖"))
        //{
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //    GetNewWeapon("prefab_naturestaff");
        //}
        //if (GUILayout.Button("左轮"))
        //{
        //    GetNewWeapon("prefab_revolver");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("弯刀"))
        //{
        //    GetNewWeapon("prefab_saber");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("水晶剑"))
        //{
        //    GetNewWeapon("prefab_crystalhalberd");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("寒冰剑"))
        //{
        //    GetNewWeapon("prefab_crystalsword");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
        //if (GUILayout.Button("锤子"))
        //{
        //    GetNewWeapon("prefab_hammer");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);
        //}
        //if (GUILayout.Button("双刃剑"))
        //{
        //    GetNewWeapon("prefab_crystalsworddouble");
        //    GetComponent<RPCScrip>().SwitchMyWeapon(GetComponent<PhotonView>().ViewID, currentWeaponName);

        //}
    }
    /// <summary>
    /// 获取新武器
    /// </summary>
    /// <param name="weaponName"></param>
    public void GetNewWeapon(string weaponName)
    {
        //GameObject gobj = ObjectPool.Instance.GetObj(weaponName);
        //网络实例化
        if (!phv.IsMine)
        {
            return;
        }
         gobj = PhotonNetwork.Instantiate(weaponName, transform.position, Quaternion.identity);
       
        gobj.GetComponent<WeaponAtrribute>().SetFather(GetComponent<PhotonView>().ViewID);
        //去掉克隆
        gobj.name = gobj.name.Replace("(Clone)", "");
        //获取子弹脚本
        BulletCSharp bulletCSharp = gobj.GetComponent<BulletCSharp>();
        //如果有子弹脚本，代表这是武器
        if (bulletCSharp != null)
        {
            //防止脱手
            gobj.GetComponent<Rigidbody>().isKinematic = true;
            //位置归零
            Destroy(bulletCSharp);
            //子弹初始化
            BulletInit(gobj, gameObject.transform);
        }
        //攻击等于数据库查找到的类型
        int attackType = DBA.Instance.GetWeaponTypeByName(weaponName);

        //武器类型等于查找到的攻击类型
        WeaponType = attackType;
        //当前武器等于生成的武器名字
        currentWeaponName = gobj.name;
        //如果没找到手就在找一遍
        if (handOfPlayers == null)
        {
            handOfPlayers = transform.GetComponentsInChildren<HandOfPlayer>();
        }

        for (int i = 0; i < handOfPlayers.Length; i++)
        {
            //有手就清除武器；
            handOfPlayers[i].ClearCurrentWeapon();
        }
        //如果类型1、2
        if (attackType == 1 || attackType == 2)
        {
            for (int i = 0; i < handOfPlayers.Length; i++)
            {

                if (handOfPlayers[i].handType == HandType.Right)
                {
                    handOfPlayers[i].ChangeCurrentWeapon(gobj);
                }
            }
        }
        //类型3、4
        if (attackType == 3 || attackType == 4)
        {
            for (int i = 0; i < handOfPlayers.Length; i++)
            {
                if (handOfPlayers[i].handType == HandType.Left)
                {
                    handOfPlayers[i].ChangeCurrentWeapon(gobj);
                }
            }

        }
        //武器动画
        ChangeAttackAnimator(currentWeaponName);
    }

    /// <summary>
    /// 子弹的初始化
    /// </summary>
    /// <param name="Bullet"></param>
    /// <param name="fireMouse"></param>
    public void BulletInit(GameObject Bullet, Transform fireMouse)
    {
        Bullet.transform.SetParent(fireMouse);
        Debug.Log(Bullet + "子弹初始化");
        Bullet.transform.localPosition = Vector3.zero;
        Bullet.transform.localEulerAngles = Vector3.zero;
    }



    /// <summary>
    /// 集成接口后实现接口的东西
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentWeaponName);
            stream.SendNext(WeaponType);
        }

        else
        {
            this.currentWeaponName = (string)(stream.ReceiveNext());
            this.WeaponType = (int)(stream.ReceiveNext());
        }
    }

    
    //public void GetWeaponHasType()
    //{
    //}



    public void SetPhotonAnimatorView()
    {
        phv.RPC("PhotonAni", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void PhotonAni()
    {
        PhotonAnimatorView pav = GetComponent<PhotonAnimatorView>();
        pav.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
        pav.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
        pav.SetParameterSynchronized("Speed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
        pav.SetParameterSynchronized("AttackMode", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
        pav.SetParameterSynchronized("OpenFire", PhotonAnimatorView.ParameterType.Trigger, PhotonAnimatorView.SynchronizeType.Continuous);

    }



}

