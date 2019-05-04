using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用来标记每个武器是什么类型
/// </summary>
public enum BulletType
{
    KnifeLight,
    Bullet,
    MagicBall,
    Axe,
    Sai,
    Club,
    Arrow
}



public class BulletClassType : MonoBehaviour {

    public BulletType type;
    public int ViewID;

}
