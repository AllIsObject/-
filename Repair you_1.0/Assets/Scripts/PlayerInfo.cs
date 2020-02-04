using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static PlayerData;

public static class PlayerData
{
    public static string[] P1_MOVE = new string[2] { "p1_Horizontal", "p1_Vertical" };
    public static string[] P2_MOVE = new string[2] { "p2_Horizontal", "p2_Vertical" };

    public enum PlayerNum {p1,p2};

    /// <summary>
    /// 获得移动的控制字符串
    /// </summary>
    /// <param name="playerNum"></param>
    /// <returns></returns>
    public static string[] GetMoveStr(PlayerNum playerNum) {
        return playerNum== PlayerNum.p1? P1_MOVE: P2_MOVE;
    }
    /// <summary>
    /// 获得闪现的控制字符串
    /// </summary>
    /// <param name="playerNum"></param>
    /// <returns></returns>
    public static KeyCode GetShanxianStr(PlayerNum playerNum)
    {
        return playerNum == PlayerNum.p1 ? KeyCode.J : KeyCode.Keypad1;
    }
    /// <summary>
    /// 获得扔锤子的控制字符串
    /// </summary>
    /// <param name="playerNum"></param>
    /// <returns></returns>
    public static KeyCode GetChuiziStr(PlayerNum playerNum)
    {
        return playerNum == PlayerNum.p1 ? KeyCode.K : KeyCode.Keypad2;
    }


}
/// <summary>
/// 玩家信息类
/// </summary>
public  class PlayerInfo: MonoBehaviour
{
    public PlayerNum playerNum;
    public bool isEmo;
    private GameObject weapomed_pos;
    

    private void Start()
    {
        weapomed_pos = gameObject.FindChild("weapomed_pos");
    }
    /// <summary>
    ///帽子数量
    /// </summary>
    public int MaoziNum {
        get {
            return weapomed_pos.transform.childCount;
        }
        set {
            if (value < weapomed_pos.transform.childCount && weapomed_pos.transform.childCount>0) {
                //减帽子
                var tf = weapomed_pos.transform.GetChild(weapomed_pos.transform.childCount - 1);
                Destroy(tf.gameObject);
                if (GetComponent<PlayerSkill>().emoNum <= MaoziNum) {
                    IsEmo = false;
                }
            }
        }
    }
    /// <summary>
    /// 是否为恶魔
    /// </summary>
    public bool IsEmo {
        get => isEmo;
        set {
            if (value!= isEmo && value) {
                //变身
                Debug.Log("切换恶魔");

            }
            isEmo = value;
        }
    }
}

