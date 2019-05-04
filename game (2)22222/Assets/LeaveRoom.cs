using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveRoom : MonoBehaviourPunCallbacks
{
    Button btn;
    int playerBlood;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    private void Start()
    {
        btn.onClick.AddListener(()=>
        {
            PlayerSelfId.playerID.GetComponent<PhotonView>().RPC("DestroyBloodBar", RpcTarget.AllBufferedViaServer,
                PlayerSelfId.playerID.GetComponent<PhotonView>().ViewID);


            PhotonNetwork.LeaveRoom();
        });
    }

    public override void OnLeftRoom()
    {
        ObjectPool.Instance.ClearObj();

        StartCoroutine(changeRoom());
    }

    IEnumerator changeRoom()
    {
        SceneManager.LoadScene("UIScenc01");
        yield return 0;
    }

    [PunRPC]
    public void DestroyBloodBar(int id)
    {
        //Destroy();
        GameObject[] selfBbar = GameObject.FindGameObjectsWithTag("BloodBar");
        for (int i = 0; i < selfBbar.Length; i++)
        {
            if (selfBbar[i].GetComponent<BloodBar>().selfId == id)
            {
                Debug.Log(id + "--------------");
                Destroy(selfBbar[i]);
                break;
            }

        }
    }

}
