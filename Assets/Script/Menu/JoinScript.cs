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
        // ���[�������擾���A�ݒ�
        RoomId_Value.text = PhotonNetwork.CurrentRoom.Name;


        // �v���C���[�擾
        var Players = PhotonNetwork.CurrentRoom.Players;

        var i = 0;


        foreach (var Player in Players)
        {
            Debug.Log("�v���C���[�� �F " + Player.Value.NickName + Player.Value.ActorNumber);
            Debug.Log("���[�U�[ID �F " + Player.Value.UserId);

            // �v���C���[�̏��ԕ���
            if (Player.Value.UserId != "" && Player.Value.ActorNumber == 1)
            {
                // �v���C���[�P�̏ꍇ
                P1_Name.text = Player.Value.NickName + Player.Value.ActorNumber;
            }
            else if (Player.Value.UserId != "" && Player.Value.ActorNumber == 2)
            {
                // �v���C���[2�̏ꍇ
                P2_Name.text = Player.Value.NickName + Player.Value.ActorNumber;
            }
            else
            {
                // Join�œ������ꍇ�p
                P1_Name.text = Player.Value.NickName + Player.Value.ActorNumber;
            }

        }
    }
}
