using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // ３つのPanelを格納する変数
    // インスペクターウィンドウからゲームオブジェクトを設定する
    [SerializeField] GameObject SelectPanel;
    [SerializeField] GameObject HostPanel;
    [SerializeField] GameObject JoinPanel;


    void Start()
    {
        // [BackToMenu]メソッドを呼び出す
        BackToMenu();
    }


    // [SelectPanel]で[Host_Btn]が押されたときの処理
    // [HostPanel]をアクティブにする
    public void PressHost_Btn()
    {
        SelectPanel.SetActive(false);
        HostPanel.SetActive(true);
    }


    // [SelectPanel]で[Join_Btn]が押されたときの処理
    // [JoinPanel]をアクティブにする
    public void PressJoin_Btn()
    {
        SelectPanel.SetActive(false);
        JoinPanel.SetActive(true);
    }


    // [HostPanel]、[JoinPanel]で[Back_Btn]が押されたときの処理
    // [SelectPanel]をアクティブにする
    public void BackToMenu()
    {
        SelectPanel.SetActive(true);
        HostPanel.SetActive(false);
        JoinPanel.SetActive(false);
    }
}