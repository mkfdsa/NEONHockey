using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;



public class JoinScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text P1_Name = default;
    [SerializeField] private Text P2_Name = default;
    [SerializeField] private Text RoomId_Value = default;

    public override void OnJoinedRoom()
    {
        // ルーム名を取得し、設定
        RoomId_Value.text = PhotonNetwork.CurrentRoom.Name;


        // プレイヤー取得
        var Players = PhotonNetwork.CurrentRoom.Players;

        var i = 0;


        foreach (var Player in Players)
        {
            Debug.Log("プレイヤー名 ： " + Player.Value.NickName + Player.Value.ActorNumber);
            Debug.Log("ユーザーID ： " + Player.Value.UserId);

            // プレイヤーの順番分岐
            if (Player.Value.UserId != "" && Player.Value.ActorNumber == 1)
            {
                // プレイヤー１の場合
                P1_Name.text = Player.Value.NickName + Player.Value.ActorNumber;
            }
            else if (Player.Value.UserId != "" && Player.Value.ActorNumber == 2)
            {
                // プレイヤー2の場合
                P2_Name.text = Player.Value.NickName + Player.Value.ActorNumber;
            }
            else
            {
                // Joinで入った場合用
                P1_Name.text = Player.Value.NickName + Player.Value.ActorNumber;
            }

        }
    }
}
