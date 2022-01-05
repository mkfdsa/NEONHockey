using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TitleScene : MonoBehaviourPunCallbacks
{
    // シリアライゼーション化
    [SerializeField] private Button Host_Btn = default;
    [SerializeField] private Button Join_Btn = default;
    [SerializeField] private Button Back_Btn = default; 
    [SerializeField] private TMP_InputField Pass_Input = default;
    [SerializeField] private Text RoomId_Value = default;
    [SerializeField] private Text P1_Name = default;
    [SerializeField] private Text P2_Name = default;
    [SerializeField] private GameObject MessagePanel = default;
    [SerializeField] private GameObject HostPanel = default;
    [SerializeField] private GameObject JoinPanel = default;

    private CanvasGroup canvasGroup;

    // ルームID　設定
    private string RoomId;

    private void Start()
    {
        Debug.Log("スタート");

        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続");
        // マスターサーバーに接続したら、入力できるようにする
        // canvasGroup.interactable = true;

        // [Host_Btn]をクリック時[OnHostButtonClick()]を発火
        Host_Btn.onClick.AddListener(OnHostButtonClick);

        // [Join_Btn]をクリック時[OnJoinButtonClick()]を発火
        Join_Btn.onClick.AddListener(OnJoinButtonClick);
        Join_Btn.interactable = false;

        Pass_Input.onValueChanged.AddListener(OnPassInputFieldValueChanged);
        
        Back_Btn.onClick.AddListener(OnBackButtonClick);
    }

    private void OnHostButtonClick()
    {
        // ルーム参加処理中は、入力できないようにする
        // canvasGroup.interactable = false;

        // ルームを非公開に設定する（新規でルームを作成する場合）
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = false;

        // RoomID ランダム生成
        var guid = Guid.NewGuid();
        RoomId = guid.ToString().Substring(0, 6);

        Debug.Log(RoomId);

        // パスワードと同じ名前のルームに参加する（ルームが存在しなければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom(RoomId, roomOptions, TypedLobby.Default);
        Debug.Log("ルーム作成");
    }

    private void OnJoinButtonClick()
    {

        PhotonNetwork.JoinRoom(Pass_Input.text);
    }

    private void OnBackButtonClick()
    {
        // ロビー（マスターサーバー）に戻る
        PhotonNetwork.LeaveRoom();

        P1_Name.text = "";
        P2_Name.text = "";
        RoomId_Value.text = "";
    }

    private void OnPassInputFieldValueChanged(string value)
    {
        // パスワードを6桁入力した時のみ、ルーム参加ボタンを押せるようにする
        Join_Btn.interactable = (value.Length == 6);
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("ルーム入った");

    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ルームへの参加が失敗したら、パスワードを再び入力できるようにする
        Pass_Input.text = string.Empty;
        // MessagePanel.SetActive(true);
        StartCoroutine(ShowImageSecond(MessagePanel, 2f));
    }

    // 引数で当たられたオブジェクトを表示し、引数の秒数後に非表示にするコルーチン
    IEnumerator ShowImageSecond(GameObject targetObj, float sec)
    {
        targetObj.SetActive(true); //引数で指定されたオブジェクトを表示する
        yield return new WaitForSeconds(sec); // 引数で指定された秒数待つ
        targetObj.SetActive(false); //引数で指定されたオブジェクトを非表示にする
    }


}