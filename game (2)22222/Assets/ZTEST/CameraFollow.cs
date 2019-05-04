using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;

    bool Init;

    void Update()
    {
        if (player1 != null)
        {
            transform.position = player1.position;
            if (!Init&& player1.GetComponent<PhotonView>().IsMine)
            {
                player1.GetComponent<PlayerAttack>().Init();
                Init = true;
            }
        }
    }
}
