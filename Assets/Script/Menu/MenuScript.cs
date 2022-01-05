using Photon.Realtime;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

//namespace Photon.Pun.Demo.Asteroids
//{
public class MenuScript : MonoBehaviourPunCallbacks
{
    [Header("SELECT 画面")]
    public GameObject SelectPanel;

    [Header("HOST 画面")]
    public GameObject HostPanel;

    [Header("JOIN 画面")]
    public GameObject JoinPanel;
    public Button Join_Btn;
    public InputField RoomID_Input;


    [Header("その他 GUI")]
    public Text RoomId_Value;
    public GameObject PlayerNamePrefab;

    bool isJoinedRoom = false;


    private void Start()
    {
        Debug.Log("スタート");

        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        // ルームに入室していないならスキップ
        if (!isJoinedRoom)
            return;

        int RanInt = new System.Random().Next(1, 1000);
        // 
        // PlayerNamePrefab.GetComponent<PhotonView>().RPC("CreatePlayerNameLabel", RpcTarget.AllBuffered, PhotonNetwork.AllocateViewID(RanInt));

    }

    #region Photon CallBack

    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続");
        SetActivePanel(SelectPanel.name);

        // 入力項目の6文字　文字数制御
        RoomID_Input.onValueChanged.AddListener(OnPassInputChanged);
    }


    //// マスターサーバーのロビーに入るときに呼び出されます
    //public override void OnJoinedLobby()
    //{
    //}
    //// ロビーを出た後に呼び出されます。
    //public override void OnLeftLobby()
    //{
    //}
    //// サーバーがルームを作成できなかったとき（OpCreateRoomが失敗したとき）に呼び出されます。
    //public override void OnCreateRoomFailed(short returnCode, string message)
    //{
    //}
    //// 前回のOpJoinRoom呼び出しがサーバーで失敗したときに呼び出されます。
    //public override void OnJoinRoomFailed(short returnCode, string message)
    //{
    //}


    // ルームに入ったときに呼び出されます。
    public override void OnJoinedRoom()
    {
        // Host画面に遷移
        SetActivePanel(HostPanel.name);

        // ルームID　設定
        RoomId_Value.text = PhotonNetwork.CurrentRoom.Name;

        // プレイヤーラベル作成
        bool ResCreateLabel = CreatePlayerNamePrefab();
    }

    public override void OnLeftRoom()
    {
        // Select画面に遷移
        SetActivePanel(SelectPanel.name);
    }

    // 他のプレイヤーが入室してきたとき
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("新しいプレイヤーが部屋に入った");
        // プレイヤーラベル作成
        bool ResCreateLabel = CreatePlayerNamePrefab();

    }

    // プレイヤーが部屋を退室したとき
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("プレイヤーが部屋に出た");
        // プレイヤーラベル作成
        bool ResCreateLabel = CreatePlayerNamePrefab();
    }
    #endregion

    #region Button Clicked
    // Select画面にて、Hostボタンを押下時
    public void OnHostButtonClicked()
    {
        // RoomID ランダム生成
        var RoomId = Guid.NewGuid().ToString().Substring(0, 6);

        // ルームオプション　設定
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };

        RoomId = "fffddd";
        // ルーム作成　Callback[OnJoinedRoom　か　 OnCreateRoomFailed]
        PhotonNetwork.CreateRoom(RoomId, options, null);

        Debug.Log("ルーム作成");
    }

    // Select画面にて、Joinボタンを押下時
    public void OnJoinButtonClicked()
    {
        // Join画面に遷移
        SetActivePanel(JoinPanel.name);

    }

    // Join画面にて、Joinボタンを押下時
    public void OnRoomJoinButtonClicked()
    {
        // s
        PhotonNetwork.JoinRoom(RoomID_Input.text);
    }

    // 他画面にて、Backボタンを押下時
    public void OnBackButtonClicked()
    {
        // Hostの場合、一人の場合、ルームの破棄処理
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {

            // 二人の場合、Joinした人をルームマスターにする
        }
        else
        {

        }

        // Joinした場合、ルーム退出する処理
        PhotonNetwork.LeaveRoom();

        // Select画面に遷移
        SetActivePanel(SelectPanel.name);

    }
    #endregion

    // ルーム名の入力制御
    private void OnPassInputChanged(string value)
    {
        // パスワードを6桁入力した時のみ、ルーム参加ボタンを押せるようにする
        Join_Btn.interactable = (value.Length == 6);
    }

    #region MakeFunction
    // ルーム人数とプレイヤーラベルの同期、作成
    private bool CreatePlayerNamePrefab()
    {
        bool res = false;

        var gameObject = GameObject.Find("PlayerNamePrefab(Clone)");

        // "PlayerNameLabel~~" というタグ名のゲームオブジェクトを取得
        try
        {
            // 一旦プレイヤーラベルあれば、削除
            if (GameObject.FindGameObjectWithTag("PlayerNameLabel_1") != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("PlayerNameLabel_1"));
            }
            
            if (GameObject.FindGameObjectWithTag("PlayerNameLabel_2") != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("PlayerNameLabel_2"));
            }
        }
        catch
        {
            res = false;
        }

        // プレイヤーラベル　作成
        foreach (var p in PhotonNetwork.PlayerList)
        {
            var PlayerNameNetwork = PhotonNetwork.Instantiate("PlayerNamePrefab", Vector3.zero, Quaternion.identity);
            PlayerNameNetwork.transform.SetParent(HostPanel.transform);
            PlayerNameNetwork.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            // プレイヤーの順番分岐
            if (p.UserId != "" && p.ActorNumber == 1)
            {
                // プレイヤー１の場合
                Debug.Log("プレイヤー１ 生成");
                PlayerNameNetwork.tag = "PlayerNameLabel_1";
                PlayerNameNetwork.transform.localPosition = new Vector3(0.0f, -45.0f, 0.0f);
            }
            else // if (p.UserId != "" && p.ActorNumber == 2)
            {
                // プレイヤー2の場合
                Debug.Log("プレイヤー2 生成");
                PlayerNameNetwork.tag = "PlayerNameLabel_2";
                PlayerNameNetwork.transform.localPosition = new Vector3(0.0f, -180.0f, 0.0f);
            }
            // ラベル生成
            PlayerNameNetwork.GetComponent<PlayerNameLabel>().Initialize(p.ActorNumber, p.NickName);
            res = true;
        }

        return res;
    }

    #endregion


    #region RPC Connection
    //[PunRPC]
    //private void CreatePlayerNameLabel(int viewID)
    //{
    //    // プレイヤー名　作成　＋　同期処理
    //    foreach (var p in PhotonNetwork.PlayerList)
    //    {
    //        GameObject PlayerName = Instantiate(PlayerNamePrefab);
    //        PlayerName.transform.SetParent(HostPanel.transform);
    //        PlayerName.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //        // プレイヤーの順番分岐
    //        if (p.UserId != "" && p.ActorNumber == 1)
    //        {
    //            // プレイヤー１の場合
    //            PlayerName.transform.localPosition = new Vector3(0.0f, -45.0f, 0.0f);
    //        }
    //        else if (p.UserId != "" && p.ActorNumber == 2)
    //        {
    //            // プレイヤー2の場合
    //            PlayerName.transform.localPosition = new Vector3(0.0f, -180.0f, 0.0f);
    //        }
    //        // ラベル生成
    //        PlayerName.GetComponent<PlayerNameLabel>().Initialize(p.ActorNumber, p.NickName);

    //        // PhotonView 　追加
    //        var _photonView = PlayerName.gameObject.AddComponent<PhotonView>();
    //        // PhotonView に ViewID を設定
    //        _photonView.ViewID = viewID;
    //        //到達保証の設定
    //        _photonView.Synchronization = ViewSynchronization.ReliableDeltaCompressed;

    //    }
    //}
    #endregion
    // アクティブにするパネルを設定
    private void SetActivePanel(string activePanel)
    {
        // Select 画面
        SelectPanel.SetActive(activePanel.Equals(SelectPanel.name));
        // Host 画面
        HostPanel.SetActive(activePanel.Equals(HostPanel.name));
        // Join 画面
        JoinPanel.SetActive(activePanel.Equals(JoinPanel.name));
    }

}
