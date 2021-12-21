using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        // var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        var Player_1_position = new Vector3(0, -5);
        PhotonNetwork.Instantiate("Player_1", Player_1_position, Quaternion.identity);
        var Player_2_position = new Vector3(0, 5);
        PhotonNetwork.Instantiate("Player_2", Player_2_position, Quaternion.identity);
        var Ball_position = new Vector3(0, 0);
        PhotonNetwork.Instantiate("Ball", Ball_position, Quaternion.identity);
    }
}