using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// 左手还是右手
/// </summary>
public enum HandType
{
    Left,
    Right
}

public class HandOfPlayer : MonoBehaviour
{

    public HandType handType;

   public Transform weaponPoint;

    private void Awake()
    {
        
        weaponPoint = transform.Find("WeaponPoint");
     //   weaponPoint = transform.Find("WeaponPoint ");

        
    }
    /// <summary>
    /// 获取当前的武器
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentWeapon()
    {
        if (weaponPoint.childCount == 0)
        { 
            return null;
        }
        return weaponPoint.GetChild(0).gameObject;


    }
    /// <summary>
    /// 改变当前的武器
    /// </summary>
    /// <param name="Weapon"></param>
    public void ChangeCurrentWeapon(GameObject Weapon)
    {
        ClearCurrentWeapon();

        Weapon.transform.SetParent(weaponPoint);
        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// 清除当前的武器
    /// </summary>
    public void ClearCurrentWeapon()
    {
        if (weaponPoint.childCount != 0)
        {

            for (int i = 0; i < weaponPoint.childCount; i++)
            {
                if(weaponPoint.GetComponentInChildren<PhotonView>()!=null)
               PhotonNetwork.Destroy(weaponPoint.GetChild(i).gameObject);
                else
               Destroy(weaponPoint.GetChild(i).gameObject);
            }
        }

    }










}
