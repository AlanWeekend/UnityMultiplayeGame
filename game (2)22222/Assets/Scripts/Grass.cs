using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grass : MonoBehaviour
{

    Material mat;

    private void Awake() {
        mat = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView photonView = other.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                PlayerBlood playerBlood = other.GetComponent<PlayerBlood>();
                playerBlood.grass += 1;
                mat.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
                other.GetComponent<PlayerBlood>().ShowBlood(playerBlood.grass<=0);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView photonView = other.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                PlayerBlood playerBlood = other.GetComponent<PlayerBlood>();
                playerBlood.grass -= 1;
                if (playerBlood.grass < 0) playerBlood.grass = 0;
                mat.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
                other.GetComponent<PlayerBlood>().ShowBlood(playerBlood.grass<=0);
            }
        }
    }
}
