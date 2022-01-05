using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerNameLabel : MonoBehaviourPunCallbacks
{

    [Header("UI References")]
    public Text PlayerName;
    public GameObject PlayerNamePrefab;

    bool isJoinedRoom = false;

    void Start()
    {

        if (PhotonNetwork.PlayerList.Length != 1)
        {
            Debug.Log("ÉvÉåÉCÉÑÅ[Ç™ëµÇ¢Ç‹ÇµÇΩ");
        }
    }

    public void Initialize(int playerId, string playerName)
    {
        PlayerName.text = playerName + " No " + playerId;
    }

    #region RPC Connection

    #endregion
}
