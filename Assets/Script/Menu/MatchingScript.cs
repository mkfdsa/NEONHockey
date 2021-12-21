using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingScript : MonoBehaviourPunCallbacks
{
    [SerializeField]// シリアライゼーション化
    private TMP_InputField Pass_Input = default;

    [SerializeField]// シリアライゼーション化
    private Button Join_Btn = default;

    private CanvasGroup canvasGroup;

    private void Start()
    {   
        // [CanvasGroup]のコンポーネントをGetしている
        // （これしたら[CanvasGroom]のパラメータがイジれる）
        canvasGroup = GetComponent<CanvasGroup>();

        // マスターサーバーに接続するまでは、入力できないようにする
        canvasGroup.interactable = false;

        // パスワードを入力する前は、ルーム参加ボタンを押せないようにする
        Join_Btn.interactable = false;

        // [Pass_Input]の中身が変われば　[OnPasswordInputFieldValueChanged()]を発火
        Pass_Input.onValueChanged.AddListener(OnPasswordInputFieldValueChanged);

        // [Join_Btn]をクリック時[OnJoinRoomButtonClick()]を発火
        Join_Btn.onClick.AddListener(OnJoinRoomButtonClick);
    }

    public override void OnConnectedToMaster()
    {
        // マスターサーバーに接続したら、入力できるようにする
        canvasGroup.interactable = true;
    }

    private void OnPasswordInputFieldValueChanged(string value)
    {
        // パスワードを6桁入力した時のみ、ルーム参加ボタンを押せるようにする
        Join_Btn.interactable = (value.Length == 6);
    }

    private void OnJoinRoomButtonClick()
    {
        // ルーム参加処理中は、入力できないようにする
        canvasGroup.interactable = false;

        // ルームを非公開に設定する（新規でルームを作成する場合）
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = false;

        // パスワードと同じ名前のルームに参加する（ルームが存在しなければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom(Pass_Input.text, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // ルームへの参加が成功したら、UIを非表示にする
        gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ルームへの参加が失敗したら、パスワードを再び入力できるようにする
        Pass_Input.text = string.Empty;
        canvasGroup.interactable = true;
    }
}